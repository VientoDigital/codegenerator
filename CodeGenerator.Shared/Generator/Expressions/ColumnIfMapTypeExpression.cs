using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CodeGenerator.Data;
using CodeGenerator.Data.Structure;
using Extenso;
using Extenso.Data;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace CodeGenerator.Generator
{
    public class ColumnIfMapTypeExpression : Expression
    {
        private static string InputPattern
        {
            get
            {
                return @"\s*" +
                    @"IF MAP COLUMN.TYPE\s+(?<equality>(NE|EQ))\s+('|‘)(?<types>[ a-zA-Z0-9_)(|]+)('|’)".DelimeterWrap() +
                    //Content between IF tags
                    "(?<content>.+?)" +
                    "/IF".DelimeterWrap() +
                    @"(?<end>\s*)";
            }
        }

        public override void Interpret(Context context)
        {
            var column = (Column)Parameter;
            var regex = new Regex(InputPattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            string result = context.Input;
            var matches = regex.Matches(result);

            foreach (Match match in matches)
            {
                if (match.Length == 0)
                {
                    continue;
                }

                bool hasEQ = (match.Groups["equality"].Value.IndexOf("EQ") != -1);
                bool hasNE = (match.Groups["equality"].Value.IndexOf("NE") != -1);
                string content = match.Groups["content"].Value;
                string[] specifiedTypes = match.Groups["typeValue"].Value.Split('|');
                string end = match.Groups["end"].Value;
                string replacement = content + end;

                var mappedTypes = GetMappedTypes(column);
                bool isMatch = false;
                for (int i = 0; i < specifiedTypes.Length; i++)
                {
                    string type = specifiedTypes[i].ToLowerInvariant().Trim();

                    if (hasEQ && (mappedTypes.Any(x => x == type))) // if EQ && any of the column's mapped types == that specified type
                    {
                        ReplaceContent(match.Value, replacement, ref result);
                        isMatch = true;
                        break;
                    }
                    else if (hasNE && (!mappedTypes.Any(x => x == type))) // if NE && NONE of the column's mapped types == that specified type
                    {
                        ReplaceContent(match.Value, replacement, ref result);
                        isMatch = true;
                        break;
                    }
                }
                if (!isMatch)
                {
                    ReplaceContent(match.Value, string.Empty, ref result);
                }
            }
            context.Output = result;
            context.Input = context.Output;
        }

        private static IEnumerable<string> GetMappedTypes(Column column)
        {
            var results = new List<string> { column.DbType.ToString().ToLowerInvariant() };

            if (Server.ProviderType == DataSource.Oracle)
            {
                results.Add(GetTypeFromMappings(column));
            }
            else
            {
                var systemType = DataTypeConvertor.GetSystemType(column.DbType);

                if (ConfigFile.Instance.SelectedLanguage.Name.In(".NET", "C#", "VB"))
                {
                    if (systemType.Name.Contains('.'))
                    {
                        results.Add(systemType.Name.RightOfLastIndexOf('.'));
                    }
                    else
                    {
                        results.Add(systemType.Name);
                    }
                }

                switch (ConfigFile.Instance.SelectedLanguage.Name)
                {
                    case "C#":
                        {
                            using var codeProvider = new CSharpCodeProvider();
                            var typeRef = new CodeTypeReference(systemType);

                            string typeName = codeProvider.GetTypeOutput(typeRef);
                            if (typeName.Contains('.'))
                            {
                                typeName = typeName.RightOfLastIndexOf('.');
                            }
                            results.Add(typeName);
                        }
                        break;

                    case "VB":
                        {
                            using var codeProvider = new VBCodeProvider();
                            var typeRef = new CodeTypeReference(systemType);
                            string typeName = codeProvider.GetTypeOutput(typeRef);
                            if (typeName.Contains('.'))
                            {
                                typeName = typeName.RightOfLastIndexOf('.');
                            }
                            results.Add(typeName);
                        }
                        break;

                    default: break;
                }
            }

            return results;
        }

        private static string GetTypeFromMappings(Column column)
        {
            var selectedLanguage = ConfigFile.Instance.SelectedLanguage;
            string nativeType = column.NativeType.ToLower();
            if (selectedLanguage.Mappings.ContainsKey(nativeType))
            {
                string mapping = selectedLanguage.Mappings[nativeType];
                if (string.IsNullOrEmpty(mapping))
                {
                    return "object";
                }
                return mapping;
            }
            else
            {
                return "object";
            }
        }

        private static void ReplaceContent(string matchString, string replacementString, ref string inputString)
        {
            inputString = Regex.Replace(inputString, Regex.Escape(matchString), replacementString);
        }
    }
}
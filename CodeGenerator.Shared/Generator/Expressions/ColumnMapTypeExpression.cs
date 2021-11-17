using System.CodeDom;
using System.Data;
using System.Text.RegularExpressions;
using CodeGenerator.Data;
using CodeGenerator.Data.Structure;
using Extenso;
using Extenso.Data;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace CodeGenerator.Generator
{
    public class ColumnMapTypeExpression : Expression
    {
        public override void Interpret(Context context)
        {
            var column = (Column)Parameter;
            string typeName;

            if (Server.ProviderType != DataSource.Oracle)
            {
                var systemType = DataTypeConvertor.GetSystemType(column.DbType);

                if (ConfigFile.Instance.SelectedLanguage.Name == ".NET")
                {
                    typeName = systemType.Name;
                }
                else if (ConfigFile.Instance.SelectedLanguage.Name == "C#")
                {
                    using var codeProvider = new CSharpCodeProvider();
                    var typeRef = new CodeTypeReference(systemType);
                    typeName = codeProvider.GetTypeOutput(typeRef);
                }
                else if (ConfigFile.Instance.SelectedLanguage.Name == "VB")
                {
                    using var codeProvider = new VBCodeProvider();
                    var typeRef = new CodeTypeReference(systemType);
                    typeName = codeProvider.GetTypeOutput(typeRef);
                }
                else
                {
                    typeName = InterpretFromMappings(column);
                }
            }
            else
            {
                typeName = InterpretFromMappings(column);
            }

            if (column.Nullable &&
                column.DbType != DbType.String &&
                ConfigFile.Instance.SelectedLanguage.Name.In(".NET", "C#", "VB")) // Not sure about other nullable types in languages..
            {
                typeName += "?";
            }

            context.Output = Regex.Replace(context.Input, "MAP COLUMN.TYPE".DelimeterWrap(), typeName);
            context.Input = context.Output;
        }

        private static string InterpretFromMappings(Column column)
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
    }
}
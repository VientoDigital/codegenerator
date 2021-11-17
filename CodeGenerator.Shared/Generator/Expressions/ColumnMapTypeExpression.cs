using System.CodeDom;
using System.Data;
using System.Text.RegularExpressions;
using CodeGenerator.Data;
using CodeGenerator.Data.Structure;
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
            string value;

            if (Server.ProviderType != ProviderType.Oracle)
            {
                var systemType = DataTypeConvertor.GetSystemType(column.DbType);

                if (ConfigFile.Instance.SelectedLanguage.Name == ".NET")
                {
                    value = systemType.Name;
                }
                else if (ConfigFile.Instance.SelectedLanguage.Name == "C#")
                {
                    using var provider = new CSharpCodeProvider();
                    var typeRef = new CodeTypeReference(systemType);
                    value = provider.GetTypeOutput(typeRef);
                }
                else if (ConfigFile.Instance.SelectedLanguage.Name == "VB")
                {
                    using var provider = new VBCodeProvider();
                    var typeRef = new CodeTypeReference(systemType);
                    value = provider.GetTypeOutput(typeRef);
                }
                else
                {
                    value = InterpretFromMappings(column);
                }
            }
            else
            {
                value = InterpretFromMappings(column);
            }

            if (column.Nullable && column.DbType != DbType.String)
            {
                value += "?";
            }

            context.Output = Regex.Replace(context.Input, "MAP COLUMN.TYPE".DelimeterWrap(), value);
            context.Input = context.Output;
        }

        private string InterpretFromMappings(Column column)
        {
            string value;
            if (ConfigFile.Instance.SelectedLanguage.Mappings.ContainsKey(column.NativeType.ToLower()))
            {
                value = ConfigFile.Instance.SelectedLanguage.Mappings[column.NativeType.ToLower()].ToString();
            }
            else
            {
                value = "object";
            }

            return value;
        }
    }
}
﻿using System.Text.RegularExpressions;
using CodeGenerator.Data.Structure;
using CodeGenerator.Shared.Extensions;

namespace CodeGenerator.Generator
{
    public class TableNameReplaceExpression : Expression
    {
        private static string InputPattern => @"TABLE.NAME.REPLACE\((?<oldValue>(.*)),(?<newValue>(.*))\)".DelimeterWrap();

        public override void Interpret(Context context)
        {
            var table = (Table)Parameter;
            var regex = new Regex(InputPattern, RegexOptions.Multiline);
            string inputString = context.Input;
            var matches = regex.Matches(inputString);

            foreach (Match match in matches)
            {
                string matchValue = match.Value;
                string tableName = table.Name;

                string oldValue = match.Groups["oldValue"].ToString();
                string newValue = match.Groups["newValue"].ToString();

                tableName = tableName.Replace(oldValue, newValue);
                inputString = Regex.Replace(inputString, Regex.Escape(matchValue), tableName);
            }

            context.Output = inputString;
            context.Input = context.Output;
        }
    }
}
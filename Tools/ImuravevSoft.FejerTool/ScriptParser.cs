using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImuravevSoft.FejerTool
{
    /// <summary>
    /// Описание синтаксиса
    /// </summary>
    internal class Syntax
    {
        /// <summary>
        /// Разделитель экспериментов
        /// </summary>
        public static string ExpSeparator = @"@";
        /// <summary>
        /// Разделитель параметров
        /// </summary>
        public static string ParamSeparator = @";";
        /// <summary>
        /// Разделитель параметр - значение
        /// </summary>
        public static string ValueSeparator = @"=";

    }
    internal static class ScriptParser
    {
        private static string NonEmptySumbols(string input)
        {
            return input.Trim().Replace(" ", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\n", String.Empty);
        }

        private static string[] GetExp(string clearInput)
        {
            return clearInput.Split(new[] { Syntax.ExpSeparator }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static FejerParams GetParams(string exp)
        {
            var pm = exp.Split(new[] { Syntax.ParamSeparator }, StringSplitOptions.RemoveEmptyEntries);
            var result = new FejerParams();
            foreach (var p in pm)
            {
                var value = p.Split(new[] { Syntax.ValueSeparator }, StringSplitOptions.RemoveEmptyEntries);
                switch (value[0].ToUpper())
                {
                    case "RELAX":
                        {
                            result.Relax = Convert.ToDouble(value[1]);
                            break;
                        }
                    case "AUTORELAX":
                        {
                            result.Autorelax = Convert.ToBoolean(value[1]);
                            break;
                        }
                    case "EPS":
                        {
                            result.Eps = Convert.ToDouble(value[1]);
                            break;
                        }
                    case "BORDER":
                        {
                            result.EdgeBorder = Convert.ToDouble(value[1]);
                            break;
                        }
                    case "MOD":
                        {
                            result.IsNewMethod = Convert.ToBoolean(value[1]);
                            break;
                        }
                    case "LOG":
                        {
                            result.Log = Convert.ToBoolean(value[1]);
                            break;
                        }
                    case "LOGFILE":
                        {
                            result.Logfile = value[1];
                            break;
                        }
                }
            }
            return result;

        }

        public static FejerParams[] Parse(string input)
        {
            var clear = NonEmptySumbols(input).Trim();
            var Exps = GetExp(clear);
            var result = new FejerParams[Exps.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = GetParams(Exps[i]);

            return result;
        }

    }
}

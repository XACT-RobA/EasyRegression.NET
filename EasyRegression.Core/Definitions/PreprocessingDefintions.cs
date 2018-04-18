using System;
using System.Collections.Generic;

namespace EasyRegression.Core.Definitions
{
    public class PreprocessingDefinitions
    {
        public static Dictionary<string, Func<double, double>> DataFunctions =
            new Dictionary<string, Func<double, double>>()
        {
            { "sin", x => Math.Sin(x) },
            { "cos", x => Math.Cos(x) },
            { "tan", x => Math.Tan(x) },
            { "log", x => Math.Log(x) },
            { "log2", x => Math.Log(x, 2) },
            { "log10", x => Math.Log10(x) },
            { "sqrt", x => Math.Sqrt(x) },
            { "inv", x => 1.0 / x },
        };
    }
}
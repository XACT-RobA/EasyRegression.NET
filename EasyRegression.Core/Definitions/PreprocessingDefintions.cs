using System;
using System.Collections.Generic;

namespace EasyRegression.Core.Definitions
{
    public static class PreprocessingDefinitions
    {
        private static Dictionary<string, Func<double, double>> _dataFunctions =
            new Dictionary<string, Func<double, double>>
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

        public static void AddDataFunction(string name, Func<double, double> function)
        {
            _dataFunctions.Add(name, function);
        }

        public static Func<double, double> GetDataFunction(string name)
        {
            if (!_dataFunctions.ContainsKey(name))
            {
                throw new ArgumentException("Expander function doesn't exist");
            }

            return _dataFunctions[name];
        }

        public static IEnumerable<string> GetDataFunctionNames()
        {
            return _dataFunctions.Keys;
        }
    }
}
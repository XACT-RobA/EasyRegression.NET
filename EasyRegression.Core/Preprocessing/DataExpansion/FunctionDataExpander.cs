using System;
using System.Collections.Generic;
using System.Linq;
using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Definitions;
using Newtonsoft.Json;

namespace EasyRegression.Core.Preprocessing.DataExpansion
{
    public class FunctionDataExpander : BaseDataExpander
    {
        private readonly string[] _functions;

        private double Evaluate(string function, double value)
        {
            if (PreprocessingDefinitions.DataFunctions.ContainsKey(function))
            {
                return PreprocessingDefinitions.DataFunctions[function](value);
            }

            throw new ArgumentException("Expander function doesn't exist");
        }

        public FunctionDataExpander(string[] functions)
        {
            if (functions == null || functions.Length < 1)
            {
                throw new ArgumentException("Must provide at least one function to FunctionDataExpander");
            }

            _functions = functions;
        }

        public override Matrix<double> Expand(Matrix<double> input)
        {
            var expandedData = new double[input.Length][];

            for (int il = 0; il < input.Length; il++)
            {
                var expandedRow = new List<double>(((_functions.Length + 1) * input.Width) + 1);
                expandedRow.Add(1.0);

                for (int iw = 0; iw < input.Width; iw++)
                {
                    var value = input[il][iw];
                    expandedRow.Add(value);

                    for (int fi = 0; fi < _functions.Length; fi++)
                    {
                        expandedRow.Add(Evaluate(_functions[fi], value));
                    }
                }

                expandedData[il] = expandedRow.ToArray();
            }

            return new Matrix<double>(expandedData);
        }

        public override double[] ReExpand(double[] input)
        {
            var expandedRow = new List<double>(((_functions.Length + 1) * input.Length) + 1);
            expandedRow.Add(1.0);

            for (int iw = 0; iw < input.Length; iw++)
            {
                var value = input[iw];
                expandedRow.Add(value);

                for (int fi = 0; fi < _functions.Length; fi++)
                {
                    expandedRow.Add(Evaluate(_functions[fi], value));
                }
            }

            return expandedRow.ToArray();
        }

        public override bool HasIntercept()
        {
            return true;
        }

        public override string Serialise()
        {
            var data = new
            {
                expanderType = GetPluginType(),
                functions = _functions,
            };
            return JsonConvert.SerializeObject(data);
        }
    }
}
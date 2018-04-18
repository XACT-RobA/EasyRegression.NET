using System;
using System.Collections.Generic;
using EasyRegression.Core.Common.Models;
using Newtonsoft.Json;

namespace EasyRegression.Core.Preprocessing.DataExpansion
{
    public class PolynomialDataExpander : BaseDataExpander
    {
        private readonly int _order;

        public PolynomialDataExpander(int order)
        {
            if (order < 1)
            {
                throw new ArgumentOutOfRangeException("Polynomial order must be greater than 0");
            }

            _order = order;
        }

        public override Matrix<double> Expand(Matrix<double> input)
        {
            var expandedData = new double[input.Length][];

            for (int il = 0; il < input.Length; il++)
            {
                var expandedRow = new List<double>((input.Width * _order) + 1);
                expandedRow.Add(1.0);

                for (int iw = 0; iw < input.Width; iw++)
                {
                    var value = input[il][iw];

                    for (int io = 1; io <= _order; io++)
                    {
                        expandedRow.Add(Math.Pow(value, io));
                    }
                }

                expandedData[il] = expandedRow.ToArray();
            }

            return new Matrix<double>(expandedData);
        }

        public override double[] ReExpand(double[] input)
        {
            var expandedRow = new List<double>((input.Length * _order) + 1);
            expandedRow.Add(1.0);

            for (int iw = 0; iw < input.Length; iw++)
            {
                var value = input[iw];

                for (int io = 1; io <= _order; io++)
                {
                    expandedRow.Add(Math.Pow(value, io));
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
                order = _order,
            };
            return JsonConvert.SerializeObject(data);
        }
    }
}
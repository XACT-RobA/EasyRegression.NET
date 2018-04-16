using System;
using EasyRegression.Core.Common.Models;
using Newtonsoft.Json;

namespace EasyRegression.Core.Preprocessing.DataExpansion
{
    public class PolynomialProductDataExpander : BaseDataExpander
    {
        private readonly int _order;

        public PolynomialProductDataExpander(int order)
        {
            _order = order;
        }

        public override Matrix<double> Expand(Matrix<double> input)
        {
            int length = input.Length;
            var expandedData = new double[length][];

            for (int il = 0; il < length; il++)
            {
                var tree = new PolynomialTree(_order, input[il]);
                expandedData[il] = tree.GetExpandedData();
            }

            return new Matrix<double>(expandedData);
        }

        public override double[] ReExpand(double[] input)
        {
            var tree = new PolynomialTree(_order, input);
            return tree.GetExpandedData();
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

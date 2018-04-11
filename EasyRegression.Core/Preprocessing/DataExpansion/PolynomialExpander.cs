using System;
using EasyRegression.Core.Common;

namespace EasyRegression.Core.Preprocessing.DataExpansion
{
    public class PolynomialDataExpander : BaseDataExpander
    {
        private int _order;

        public PolynomialDataExpander(int order)
        {
            _order = order;
        }

        public override Matrix<double> Expand(Matrix<double> input)
        {
            int length = input.Length;
            var expandedData = new double[length][];

            for (int il = 0; il < length; il++)
            {
                var tree = new PolynomialTree(_order, input.Data[il]);
                expandedData[il] = tree.GetExpandedData();
            }

            return new Matrix<double>(expandedData);;
        }

        public override double[] ReExpand(double[] input)
        {
            var tree = new PolynomialTree(_order, input);
            return tree.GetExpandedData();
        }
    }
}
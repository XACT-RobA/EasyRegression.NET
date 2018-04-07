using System;

namespace EasyRegression.Core.Preprocessing.DataExpansion
{
    public class PolynomialDataExpander : BaseDataExpander
    {
        private int _order;

        public PolynomialDataExpander(int order)
        {
            _order = order;
        }

        public override double[][] Expand(double[][] input)
        {
            int length = input.Length;
            var expandedData = new double[length][];

            int oldWidth = input[0].Length;
            int newWidth = (int)(Math.Pow(_order + 1, oldWidth) + 0.5);

            for (int il = 0; il < length; il++)
            {
                expandedData[il] = new double[newWidth];
            }

            

            throw new NotImplementedException();
        }

        public override double[] ReExpand(double[] input)
        {
            throw new System.NotImplementedException();
        }
    }
}
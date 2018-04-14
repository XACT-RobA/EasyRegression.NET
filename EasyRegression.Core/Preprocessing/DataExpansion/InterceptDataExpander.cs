using System.Linq;
using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Preprocessing.DataExpansion
{
    public class InterceptDataExpander : BaseDataExpander
    {
        public override Matrix<double> Expand(Matrix<double> input)
        {
            var output = new double[input.Length][];

            for (int i = 0; i < input.Length; i++)
            {
                var newRow = new double[input.Width + 1];
                newRow[0] = 1.0;
                for (int j = 0; j < input.Width; j++)
                {
                    newRow[j + 1] = input[i][j];
                }
                output[i] = newRow;
            }

            return new Matrix<double>(output);
        }

        public override double[] ReExpand(double[] data)
        {
            var output = new double[data.Length + 1];
            
            output[0] = 1.0;
            for (int i = 0; i < data.Length; i++)
            {
                output[i + 1] = data[i];
            }

            return output;
        }

        public override bool HasIntercept()
        {
            return true;
        }
    }
}
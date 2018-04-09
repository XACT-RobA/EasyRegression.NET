using EasyRegression.Core.Common;
using EasyRegression.Core.Common.Maths;

namespace EasyRegression.Core.Preprocessing.DataSmoothing
{
    public class DataNormaliser : BaseDataSmoother
    {
        protected override void CalculateParameters(double[][] data)
        {
            int width = data[0].Length;

            _subtractors = new double[width];
            _divisors = new double[width];

            var mins = data.ColumnMinimums();
            var maxes = data.ColumnMaximums();

            for (int iw = 0; iw < width; iw++)
            {
                _subtractors[iw] = mins[iw];
                _divisors[iw] = maxes[iw] - mins[iw];
            }
        }
    }
}
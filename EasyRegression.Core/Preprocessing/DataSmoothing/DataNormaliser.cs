using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Common.Maths;

namespace EasyRegression.Core.Preprocessing.DataSmoothing
{
    public class DataNormaliser : BaseDataSmoother
    {
        protected override void CalculateParameters(Matrix<double> input)
        {
            int width = input.Width;

            _subtractors = new double[width];
            _divisors = new double[width];

            var mins = input.Data.ColumnMinimums();
            var maxes = input.Data.ColumnMaximums();

            for (int iw = 0; iw < width; iw++)
            {
                _subtractors[iw] = mins[iw];
                _divisors[iw] = maxes[iw] - mins[iw];
            }
        }
    }
}
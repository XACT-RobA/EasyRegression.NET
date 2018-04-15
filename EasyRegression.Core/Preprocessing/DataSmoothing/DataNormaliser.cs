using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Common.Maths;

namespace EasyRegression.Core.Preprocessing.DataSmoothing
{
    public class DataNormaliser : BaseDataSmoother
    {
        public DataNormaliser() { }

        internal DataNormaliser(double[] subtractors, double[] divisors)
        {
            _subtractors = subtractors;
            _divisors = divisors;
        }

        protected override void CalculateParameters(Matrix<double> input)
        {
            int width = input.Width;

            _subtractors = new double[width];
            _divisors = new double[width];

            var mins = input.Data.ColumnMinimums();
            var maxes = input.Data.ColumnMaximums();

            int start = 0;

            if (_hasIntercept)
            {
                _subtractors[0] = 0.0;
                _divisors[0] = 1.0;
                start++;
            }

            for (int iw = start; iw < width; iw++)
            {
                _subtractors[iw] = mins[iw];
                _divisors[iw] = maxes[iw] - mins[iw];
            }
        }
    }
}
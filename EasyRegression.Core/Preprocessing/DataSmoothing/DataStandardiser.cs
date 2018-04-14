using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Common.Maths;

namespace EasyRegression.Core.Preprocessing.DataSmoothing
{
    public class DataStandardiser : BaseDataSmoother
    {
        protected override void CalculateParameters(Matrix<double> input)
        {
            _subtractors = input.Data.ColumnMeans();
            _divisors = input.Data.ColumnStandardDeviations();

            if (_hasIntercept)
            {
                _subtractors[0] = 0.0;
                _divisors[0] = 1.0;
            }
        }
    }
}
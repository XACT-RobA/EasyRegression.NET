using EasyRegression.Core.Common;
using EasyRegression.Core.Common.Maths;

namespace EasyRegression.Core.Preprocessing.DataSmoothing
{
    public class DataStandardiser : BaseDataSmoother
    {
        protected override void CalculateParameters(Matrix<double> input)
        {
            _subtractors = input.Data.ColumnMeans();
            _divisors = input.Data.ColumnStandardDeviations();
        }
    }
}
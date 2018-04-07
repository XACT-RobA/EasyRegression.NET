using EasyRegression.Core.Common;

namespace EasyRegression.Core.Preprocessing.DataSmoothing
{
    public class DataStandardiser : BaseDataSmoother
    {
        protected override void CalculateParameters(double[][] data)
        {
            _subtractors = data.ColumnMeans();
            _divisors = data.ColumnStandardDeviations();
        }
    }
}
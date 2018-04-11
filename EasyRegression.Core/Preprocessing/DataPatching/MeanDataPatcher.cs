using EasyRegression.Core.Common;
using EasyRegression.Core.Common.Maths;

namespace EasyRegression.Core.Preprocessing.DataPatching
{
    public class MeanDataPatcher : BaseDataPatcher
    {
        protected override void CalculateParameters(Matrix<double?> input)
        {
            _parameters = input.Data.ColumnMeans();
        }

        protected override void CalculateParameters(Matrix<double> input)
        {
            _parameters = input.Data.ColumnMeans();
        }
    }
}
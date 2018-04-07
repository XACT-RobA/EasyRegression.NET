using EasyRegression.Core.Common;

namespace EasyRegression.Core.Preprocessing.DataPatching
{
    public class MeanDataPatcher : BaseDataPatcher
    {
        protected override void CalculateParameters(double?[][] data)
        {
            _parameters = data.ColumnMeans();
        }

        protected override void CalculateParameters(double[][] data)
        {
            _parameters = data.ColumnMeans();
        }
    }
}
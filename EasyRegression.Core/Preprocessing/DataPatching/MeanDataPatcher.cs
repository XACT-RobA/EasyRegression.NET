using EasyRegression.Core.Common;

namespace EasyRegression.Core.Preprocessing.DataPatching
{
    public class MeanDatapatcher : BaseDataPatcher
    {
        protected override void CalculateParameters(double?[][] data)
        {
            _parameters = data.ColumnMeans();
        }
    }
}
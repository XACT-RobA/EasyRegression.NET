using EasyRegression.Core.Common;

namespace EasyRegression.Core.Preprocessing.DataPatching
{
    public class MedianDatapatcher : BaseDataPatcher
    {
        protected override void CalculateParameters(double?[][] data)
        {
            _parameters = data.ColumnMedians();
        }

        protected override void CalculateParameters(double[][] data)
        {
            _parameters = data.ColumnMedians();
        }
    }
}
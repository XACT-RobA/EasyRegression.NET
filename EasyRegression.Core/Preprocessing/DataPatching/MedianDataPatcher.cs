using EasyRegression.Core.Common;
using EasyRegression.Core.Common.Maths;

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
using EasyRegression.Core.Common;
using EasyRegression.Core.Common.Maths;

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
using EasyRegression.Core.Common;

namespace EasyRegression.Core.Preprocessing.DataPatching
{
    public class MeanDatapatcher : BaseDataPatcher
    {
        protected override void CalculateParameters(double?[][] data)
        {
            int width = data[0].Length;
            _parameters = new double[width];

            for (int i = 0; i < width; i++)
            {
                _parameters[i] = data.ColumnMean(i);
            }
        }
    }
}
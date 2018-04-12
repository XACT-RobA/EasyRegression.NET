using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Common.Maths;

namespace EasyRegression.Core.Preprocessing.DataPatching
{
    public class MedianDatapatcher : BaseDataPatcher
    {
        protected override void CalculateParameters(Matrix<double?> input)
        {
            _parameters = input.Data.ColumnMedians();
        }

        protected override void CalculateParameters(Matrix<double> input)
        {
            _parameters = input.Data.ColumnMedians();
        }
    }
}
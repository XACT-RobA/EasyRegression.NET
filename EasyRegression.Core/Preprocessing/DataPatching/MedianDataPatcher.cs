using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Common.Maths;

namespace EasyRegression.Core.Preprocessing.DataPatching
{
    public class MedianDataPatcher : BaseDataPatcher
    {
        public MedianDataPatcher() { }

        internal MedianDataPatcher(double[] parameters)
        {
            _parameters = parameters;
        }

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
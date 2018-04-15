using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Common.Maths;

namespace EasyRegression.Core.Preprocessing.DataPatching
{
    public class MeanDataPatcher : BaseDataPatcher
    {
        public MeanDataPatcher() { }

        internal MeanDataPatcher(double[] parameters)
        {
            _parameters = parameters;
        }

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
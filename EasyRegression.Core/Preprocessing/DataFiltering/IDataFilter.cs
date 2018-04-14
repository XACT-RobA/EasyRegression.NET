using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Preprocessing.DataFiltering
{
    public interface IDataFilter : IPreprocessingPlugin
    {
        Matrix<double> Filter(Matrix<double> input);
    }
}
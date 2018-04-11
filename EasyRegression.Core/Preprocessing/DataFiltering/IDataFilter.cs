using EasyRegression.Core.Common;

namespace EasyRegression.Core.Preprocessing.DataFiltering
{
    public interface IDataFilter : IPreprocessingPlugin
    {
        void CalculateOutliers(Matrix<double> input);

        Matrix<double> Filter();
    }
}
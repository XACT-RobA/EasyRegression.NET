using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Preprocessing.DataExpansion
{
    public interface IDataExpander : IPreprocessingPlugin
    {
        // Expand data for training
        Matrix<double> Expand(Matrix<double> input);

        // Expand testing/prediction data
        double[] ReExpand(double[] data);
    }
}
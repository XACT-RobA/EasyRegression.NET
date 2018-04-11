using EasyRegression.Core.Common;
using EasyRegression.Core.Preprocessing.DataExpansion;
using EasyRegression.Core.Preprocessing.DataPatching;
using EasyRegression.Core.Preprocessing.DataSmoothing;

namespace EasyRegression.Core.Preprocessing
{
    public interface IPreprocessor
    {
        // Set plugins
        void SetDataPatcher(IDataPatcher dataPatcher);
        void SetDataExpander(IDataExpander dataExpander);
        void SetDataSmoother(IDataSmoother dataSmoother);


        // Preprocess training data
        Matrix<double> Preprocess(Matrix<double> input);
        Matrix<double> Preprocess(Matrix<double?> input);

        // Process testing/predictino data using same parameters
        double[] Reprocess(double[] input);
        double[] Reprocess(double?[] input);
    }
}
using EasyRegression.Core.Common.Models;
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
        TrainingModel<double> Preprocess(TrainingModel<double> input);
        TrainingModel<double> Preprocess(TrainingModel<double?> input);

        // Process testing/predictino data using same parameters
        double[] Reprocess(double[] input);
        double[] Reprocess(double?[] input);

        string Serialise();
    }
}
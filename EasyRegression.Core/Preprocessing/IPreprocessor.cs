namespace EasyRegression.Core.Preprocessing
{
    public interface IPreprocessor
    {
        // Preprocess training data
        double[][] Preprocess(double[][] input);
        double[][] Preprocess(double?[][] input);

        // Process testing/predictino data using same parameters
        double[] Reprocess(double[] input);
        double[] Reprocess(double?[] input);
    }
}
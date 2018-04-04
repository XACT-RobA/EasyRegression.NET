namespace EasyRegression.Core.Preprocessing
{
    public interface IPreprocessingPlugin
    {
        // Perform preprocessing of training data
        double[][] Process();
        
        // Perform preprocessing on data to predict
        // (using values from training data)
        double[] Reprocess(double[] input);

        // Stores preprocessing parameters to json file
        void StoreParameters(string filepath);

        // Loads preprocessing parameters from json file
        void LoadParameters(string filepath);
    }
}
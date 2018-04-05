namespace EasyRegression.Core.Preprocessing
{
    public interface IPreprocessingPlugin
    {
        // Stores preprocessing parameters to json file
        void StoreParameters(string filepath);

        // Loads preprocessing parameters from json file
        void LoadParameters(string filepath);
    }
}
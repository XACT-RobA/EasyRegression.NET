namespace EasyRegression.Core.Preprocessing
{
    public interface IPreprocessingPlugin
    {
        // Stores preprocessing parameters to json
        string Serialise();
    }
}
namespace EasyRegression.Core.Definitions
{
    public class PreprocessingDefinitions
    {
        public enum DataPatchMethod
        {
            none,
            zero,
            mean,
            median,
        }

        public enum DataSmoothingMethod
        {
            none,
            normalise,
            standardise,
        }

        public enum DataExpandingMethod
        {
            none,
            polynomial,
        }
    }
}
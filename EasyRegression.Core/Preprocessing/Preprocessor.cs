namespace EasyRegression.Core.Preprocessing
{
    public class Preprocessor
    {
        private IPreprocessingPlugin _dataPatcher;

        public Preprocessor(double?[][] inputdata)
        {
            _dataPatcher = new MissingDataPatcher(inputdata);
        }
    }
}
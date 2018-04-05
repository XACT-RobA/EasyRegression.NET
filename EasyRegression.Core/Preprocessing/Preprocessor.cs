using EasyRegression.Core.Preprocessing.DataPatching;

namespace EasyRegression.Core.Preprocessing
{
    public class Preprocessor
    {
        private IDataPatcher _dataPatcher;

        public Preprocessor(double?[][] inputdata)
        {
            _dataPatcher = new MeanDatapatcher();
            
        }
    }
}
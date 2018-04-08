using EasyRegression.Core.Definitions;
using EasyRegression.Core.Preprocessing.DataExpansion;
using EasyRegression.Core.Preprocessing.DataPatching;
using EasyRegression.Core.Preprocessing.DataSmoothing;

namespace EasyRegression.Core.Preprocessing
{
    public class Preprocessor : IPreprocessor
    {
        private IDataPatcher _dataPatcher;
        private IDataSmoother _dataSmoother;
        private IDataExpander _dataExpander;

        public Preprocessor(IDataPatcher dataPatcher = null,
            IDataSmoother dataSmoother = null, 
            IDataExpander dataExpander = null)
        {
            _dataPatcher = dataPatcher ?? new MeanDataPatcher();
            _dataSmoother = dataSmoother ?? new DataStandardiser();
            _dataExpander = dataExpander ?? new BlankDataExpander();
        }

        public double[][] Preprocess(double[][] input)
        {
            var output = input;
            output = _dataPatcher.Patch(output);
            output = _dataSmoother.Smooth(output);
            output = _dataExpander.Expand(output);
            return output;
        }

        public double[][] Preprocess(double?[][] input)
        {
            var output = _dataPatcher.Patch(input);
            output = _dataSmoother.Smooth(output);
            output = _dataExpander.Expand(output);
            return output;
        }

        public double[] Reprocess(double[] input)
        {
            throw new System.NotImplementedException();
        }

        public double[] Reprocess(double?[] input)
        {
            throw new System.NotImplementedException();
        }
    }
}
using EasyRegression.Core.Definitions;
using EasyRegression.Core.Preprocessing.DataExpansion;
using EasyRegression.Core.Preprocessing.DataPatching;
using EasyRegression.Core.Preprocessing.DataSmoothing;

namespace EasyRegression.Core.Preprocessing
{
    public class Preprocessor : IPreprocessor
    {
        private IDataPatcher _dataPatcher;
        private IDataExpander _dataExpander;
        private IDataSmoother _dataSmoother;

        public Preprocessor(IDataPatcher dataPatcher = null, 
            IDataExpander dataExpander = null,
            IDataSmoother dataSmoother = null)
        {
            _dataPatcher = dataPatcher ?? new MeanDataPatcher();
            _dataExpander = dataExpander ?? new BlankDataExpander();
            _dataSmoother = dataSmoother ?? new DataStandardiser();
        }

        public double[][] Preprocess(double[][] input)
        {
            var output = input;
            output = _dataPatcher.Patch(output);
            output = _dataExpander.Expand(output);
            output = _dataSmoother.Smooth(output);
            return output;
        }

        public double[][] Preprocess(double?[][] input)
        {
            var output = _dataPatcher.Patch(input);
            output = _dataExpander.Expand(output);
            output = _dataSmoother.Smooth(output);
            return output;
        }

        public double[] Reprocess(double[] input)
        {
            var output = input;
            output = _dataPatcher.RePatch(output);
            output = _dataExpander.ReExpand(output);
            output = _dataSmoother.ReSmooth(output);
            return output;
        }

        public double[] Reprocess(double?[] input)
        {
            var output = _dataPatcher.RePatch(input);
            output = _dataExpander.ReExpand(output);
            output = _dataSmoother.ReSmooth(output);
            return output;
        }
    }
}
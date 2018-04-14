using System;
using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Definitions;
using EasyRegression.Core.Preprocessing.DataExpansion;
using EasyRegression.Core.Preprocessing.DataPatching;
using EasyRegression.Core.Preprocessing.DataSmoothing;
using Newtonsoft.Json;

namespace EasyRegression.Core.Preprocessing
{
    public class Preprocessor : IPreprocessor
    {
        private IDataPatcher _dataPatcher;
        private IDataExpander _dataExpander;
        private IDataSmoother _dataSmoother;

        public Preprocessor()
        {
            _dataPatcher =  new MeanDataPatcher();
            _dataExpander =  new InterceptDataExpander();
            _dataSmoother =  new DataStandardiser();
        }

        public void SetDataPatcher(IDataPatcher dataPatcher)
        {
            _dataPatcher = dataPatcher ?? _dataPatcher;
        }

        public void SetDataExpander(IDataExpander dataExpander)
        {
            _dataExpander = dataExpander ?? _dataExpander;
        }

        public void SetDataSmoother(IDataSmoother dataSmoother)
        {
            _dataSmoother = dataSmoother ?? _dataSmoother;
        }

        public Matrix<double> Preprocess(Matrix<double> input)
        {
            _dataSmoother.SetHasIntercept(_dataExpander.HasIntercept());

            var output = input;
            output = _dataPatcher.Patch(output);
            output = _dataExpander.Expand(output);
            output = _dataSmoother.Smooth(output);
            return output;
        }

        public Matrix<double> Preprocess(Matrix<double?> input)
        {
            _dataSmoother.SetHasIntercept(_dataExpander.HasIntercept());

            var output = _dataPatcher.Patch(input);
            output = _dataExpander.Expand(output);
            output = _dataSmoother.Smooth(output);
            return output;
        }

        public double[] Reprocess(double[] input)
        {
            _dataSmoother.SetHasIntercept(_dataExpander.HasIntercept());

            var output = input;
            output = _dataPatcher.RePatch(output);
            output = _dataExpander.ReExpand(output);
            output = _dataSmoother.ReSmooth(output);
            return output;
        }

        public double[] Reprocess(double?[] input)
        {
            _dataSmoother.SetHasIntercept(_dataExpander.HasIntercept());

            var output = _dataPatcher.RePatch(input);
            output = _dataExpander.ReExpand(output);
            output = _dataSmoother.ReSmooth(output);
            return output;
        }

        public string Serialise()
        {
            var patcherData = _dataPatcher.Serialise();
            var expanderData = _dataExpander.Serialise();
            var smootherData = _dataSmoother.Serialise();

            var data = new { preprocessingData = new[] { patcherData, expanderData, smootherData } };
            return JsonConvert.SerializeObject(data);
        }

        public static IPreprocessor Deserialise(string data)
        {
            throw new NotImplementedException();
        }
    }
}
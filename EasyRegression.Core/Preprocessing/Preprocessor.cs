using System;
using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Definitions;
using EasyRegression.Core.Preprocessing.DataExpansion;
using EasyRegression.Core.Preprocessing.DataFiltering;
using EasyRegression.Core.Preprocessing.DataPatching;
using EasyRegression.Core.Preprocessing.DataSmoothing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyRegression.Core.Preprocessing
{
    public class Preprocessor : IPreprocessor
    {
        private IDataPatcher _dataPatcher;
        private IDataFilter _dataFilter;
        private IDataExpander _dataExpander;
        private IDataSmoother _dataSmoother;

        public Preprocessor()
        {
            _dataPatcher =  new MeanDataPatcher();
            _dataFilter = new BlankDataFilter();
            _dataExpander =  new InterceptDataExpander();
            _dataSmoother =  new DataStandardiser();
        }

        public void SetDataPatcher(IDataPatcher dataPatcher)
        {
            _dataPatcher = dataPatcher;
        }

        public void SetDataFilter(IDataFilter dataFilter)
        {
            _dataFilter = dataFilter;
        }

        public void SetDataExpander(IDataExpander dataExpander)
        {
            _dataExpander = dataExpander;
        }

        public void SetDataSmoother(IDataSmoother dataSmoother)
        {
            _dataSmoother = dataSmoother;
        }

        public Matrix<double> Preprocess(Matrix<double> input)
        {
            _dataSmoother.SetHasIntercept(_dataExpander.HasIntercept());

            var output = input;
            output = _dataPatcher.Patch(output);
            output = _dataFilter.Filter(output);
            output = _dataExpander.Expand(output);
            output = _dataSmoother.Smooth(output);
            return output;
        }

        public Matrix<double> Preprocess(Matrix<double?> input)
        {
            _dataSmoother.SetHasIntercept(_dataExpander.HasIntercept());

            var output = _dataPatcher.Patch(input);
            output = _dataFilter.Filter(output);
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
            var data = new
            {
                patcher = _dataPatcher.Serialise(),
                filter = _dataFilter.Serialise(),
                expander = _dataExpander.Serialise(),
                smoother = _dataSmoother.Serialise(),
            };
            return JsonConvert.SerializeObject(data);
        }

        public static IPreprocessor Deserialise(string data)
        {
            var json = JObject.Parse(data);

            var patcherData = json["patcher"].ToString();
            var filterData = json["filter"].ToString();
            var expanderData = json["expander"].ToString();
            var smootherData = json["smoother"].ToString();

            var patcher = BaseDataPatcher.Deserialise(patcherData);
            var filter = BaseDataFilter.Deserialise(filterData);
            var expander = BaseDataExpander.Deserialise(expanderData);
            var smoother = BaseDataSmoother.Deserialise(smootherData);

            var preprocessor = new Preprocessor();
            preprocessor.SetDataPatcher(patcher);
            preprocessor.SetDataFilter(filter);
            preprocessor.SetDataExpander(expander);
            preprocessor.SetDataSmoother(smoother);

            return preprocessor;
        }
    }
}
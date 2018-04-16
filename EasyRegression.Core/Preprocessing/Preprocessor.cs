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
            SetDataPatcher(new MeanDataPatcher());
            SetDataFilter(new BlankDataFilter());
            SetDataExpander(new InterceptDataExpander());
            SetDataSmoother(new DataStandardiser());
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
            SetHasIntercept();
        }

        public void SetDataSmoother(IDataSmoother dataSmoother)
        {
            _dataSmoother = dataSmoother;
            SetHasIntercept();
        }

        private void SetHasIntercept()
        {
            if (_dataSmoother != null &&
                _dataExpander != null)
            {
                _dataSmoother.SetHasIntercept(_dataExpander.HasIntercept());
            }
        }

        public Matrix<double> Preprocess(Matrix<double> input)
        {
            var output = input;
            output = _dataPatcher.Patch(output);
            output = _dataFilter.Filter(output);
            output = _dataExpander.Expand(output);
            output = _dataSmoother.Smooth(output);
            return output;
        }

        public Matrix<double> Preprocess(Matrix<double?> input)
        {
            var output = _dataPatcher.Patch(input);
            output = _dataFilter.Filter(output);
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
            if (patcher != null) { preprocessor.SetDataPatcher(patcher); }
            if (filter != null) { preprocessor.SetDataFilter(filter); }
            if (expander != null) { preprocessor.SetDataExpander(expander); }
            if (smoother != null) { preprocessor.SetDataSmoother(smoother); }

            return preprocessor;
        }
    }
}
using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Common.Maths;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyRegression.Core.Preprocessing.DataPatching
{
    public abstract class BaseDataPatcher : BasePreprocessingPlugin, IDataPatcher
    {
        protected double[] _parameters;

        protected virtual void CalculateParameters(Matrix<double?> input)
        {
            _parameters = new double[input.Width];
        }

        protected virtual void CalculateParameters(Matrix<double> input)
        {
            _parameters = new double[input.Width];
        }

        public void SetParameters(double[] parameters)
        {
            _parameters = parameters;
        }

        public Matrix<double> Patch(Matrix<double?> input)
        {
            CalculateParameters(input);

            int length = input.Length;
            int width = input.Width;

            double[][] patchedData = new double[length][];

            for (int il = 0; il < length; il++)
            {
                patchedData[il] = new double[width];

                for (int iw = 0; iw < width; iw++)
                {
                    var value = input[il][iw];

                    patchedData[il][iw] = value.IsValidDouble() ?
                        value.Value : _parameters[iw];
                }
            }

            return new Matrix<double>(patchedData);
        }

        public Matrix<double> Patch(Matrix<double> input)
        {
            CalculateParameters(input);

            int length = input.Length;
            int width = input.Width;

            double[][] patchedData = new double[length][];

            for (int il = 0; il < length; il++)
            {
                patchedData[il] = new double[width];

                for (int iw = 0; iw < width; iw++)
                {
                    var value = input[il][iw];

                    patchedData[il][iw] = value.IsValidDouble() ?
                        value : _parameters[iw];
                }
            }

            return new Matrix<double>(patchedData);
        }

        public double[] RePatch(double?[] data)
        {
            int width = data.Length;

            double[] patchedData = new double[width];

            for (int i = 0; i < width; i++)
            {
                var value = data[i];

                patchedData[i] = value.IsValidDouble() ?
                    value.Value : _parameters[i];
            }

            return patchedData;
        }

        public double[] RePatch(double[] data)
        {
            int width = data.Length;

            double[] patchedData = new double[width];

            for (int i = 0; i < width; i++)
            {
                var value = data[i];

                patchedData[i] = value.IsValidDouble() ?
                    value : _parameters[i];
            }

            return patchedData;
        }

        public override string Serialise()
        {
            var type = GetPluginType();
            var data = new
            {
                pluginType = GetPluginType(),
                parameters = _parameters,
            };
            return JsonConvert.SerializeObject(data);
        }

        public static IDataPatcher Deserialise(string json)
        {
            var jObj = JObject.Parse(json);

            var type = (string)jObj["pluginType"];
            var parameters = ((JArray)jObj["parameters"]).ToObject<double[]>();

            switch (type)
            {
                case nameof(MeanDataPatcher):
                    return new MeanDataPatcher(parameters);
                case nameof(MedianDataPatcher):
                    return new MedianDataPatcher(parameters);
                case nameof(ZeroDataPatcher):
                    return new ZeroDataPatcher(parameters);
                default:
                    throw new System.ArgumentException();
            }
        }
    }
}
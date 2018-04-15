using EasyRegression.Core.Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyRegression.Core.Preprocessing.DataSmoothing
{
    public abstract class BaseDataSmoother : BasePreprocessingPlugin, IDataSmoother
    {
        protected double[] _subtractors;
        protected double[] _divisors;
        protected bool _hasIntercept;

        public virtual void SetHasIntercept(bool hasIntercept)
        {
            _hasIntercept = hasIntercept;
        }

        protected virtual void CalculateParameters(Matrix<double> data)
        {
            throw new System.NotImplementedException();
        }

        public virtual Matrix<double> Smooth(Matrix<double> input)
        {
            CalculateParameters(input);

            int length = input.Length;
            int width = input.Width;

            double[][] smoothedData = new double[length][];

            for (int il = 0; il < length; il++)
            {
                smoothedData[il] = new double[width];

                for (int iw = 0; iw < width; iw++)
                {
                    smoothedData[il][iw] = (input[il][iw] - _subtractors[iw])
                        / _divisors[iw];
                }
            }

            return new Matrix<double>(smoothedData);
        }

        public virtual double[] ReSmooth(double[] data)
        {
            int width = data.Length;

            double[] smoothedData = new double[width];

            for (int i = 0; i < width; i++)
            {
                smoothedData[i] = (data[i] - _subtractors[i]) / _divisors[i];
            }
            
            return smoothedData;
        }

        public override string Serialise()
        {
            var data = new
            {
                smootherType = GetPluginType(),
                subtractors = _subtractors,
                divisors = _divisors,
            };
            return JsonConvert.SerializeObject(data);
        }

        public static IDataSmoother Deserialise(string data)
        {
            var json = JObject.Parse(data);

            var type = json["smootherType"].ToObject<string>();
            var subtractors = ((JArray)json["subtractors"]).ToObject<double[]>();
            var divisors = ((JArray)json["divisors"]).ToObject<double[]>();

            switch (type)
            {
                case nameof(BlankDataSmoother):
                    return new BlankDataSmoother();
                case nameof(DataNormaliser):
                    return new DataNormaliser(subtractors, divisors);
                case nameof(DataStandardiser):
                    return new DataStandardiser(subtractors, divisors);
                default:
                    return null;
            }
        }
    }
}
using System.Linq;
using EasyRegression.Core.Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyRegression.Core.Preprocessing.DataFiltering
{
    public abstract class BaseDataFilter : BasePreprocessingPlugin, IDataFilter
    {
        protected virtual Range<double>[] CalculateLimits(Matrix<double> input)
        {
            throw new System.NotImplementedException();
        }

        protected virtual bool IsWithinLimits(double[] row, Range<double>[] limits)
        {
            for (int i = 0; i < row.Length; i++)
            {
                if (row[i] > limits[i].Upper ||
                    row[i] < limits[i].Lower)
                {
                    return false;
                }
            }
            return true;
        }

        public virtual Matrix<double> Filter(Matrix<double> input)
        {
            var limits = CalculateLimits(input);
            var data = input.Data.Where(row => IsWithinLimits(row, limits))
                                 .ToArray();
            return new Matrix<double>(data);
        }

        public override string Serialise()
        {
            throw new System.NotImplementedException();
        }

        public static IDataFilter Deserialise(string data)
        {
            var json = JObject.Parse(data);

            var type = json["filterType"].ToObject<string>();

            switch (type)
            {
                case nameof(BlankDataFilter):
                    return new BlankDataFilter();
                case nameof(StandardDeviationFilter):
                    return new StandardDeviationFilter(json["multiple"].ToObject<double>());
                case nameof(InterQuartileRangeFilter):
                    return new InterQuartileRangeFilter(json["multiple"].ToObject<double>());
                default:
                    return null;
            }
        }
    }
}
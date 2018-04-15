using System.Linq;
using EasyRegression.Core.Common.Maths;
using EasyRegression.Core.Common.Models;
using Newtonsoft.Json;

namespace EasyRegression.Core.Preprocessing.DataFiltering
{
    public class StandardDeviationFilter : BaseDataFilter
    {
        private double _multiple;

        public StandardDeviationFilter()
        {
            _multiple = 3.0;
        }

        internal StandardDeviationFilter(double multiple)
        {
            SetStandardDeviationMultiple(multiple);
        }

        public void SetStandardDeviationMultiple(double multiple)
        {
            if (multiple > 0)
            {
                _multiple = multiple;
            }
        }

        protected override Range<double>[] CalculateLimits(Matrix<double> input)
        {
            var means = input.Data.ColumnMeans();
            var stdevs = input.Data.ColumnStandardDeviations();

            return means.Select((mean, i) =>
            {
                return new Range<double>(mean + (_multiple * stdevs[i]),
                    mean - (_multiple * stdevs[i]));
            }).ToArray();
        }

        public override string Serialise()
        {
            var data = new
            {
                filterType = GetPluginType(),
                multiple = _multiple,
            };
            return JsonConvert.SerializeObject(data);
        }
    }
}
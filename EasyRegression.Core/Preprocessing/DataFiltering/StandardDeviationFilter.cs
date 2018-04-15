using System.Linq;
using EasyRegression.Core.Common.Maths;
using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Preprocessing.DataFiltering
{
    public class StandardDeviationFilter : BaseDataFilter
    {
        private double _multiple;

        public StandardDeviationFilter()
        {
            _multiple = 3.0;
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
    }
}
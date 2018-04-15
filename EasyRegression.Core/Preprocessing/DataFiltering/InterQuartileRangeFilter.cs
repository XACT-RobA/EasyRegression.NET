using System.Linq;
using EasyRegression.Core.Common.Maths;
using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Preprocessing.DataFiltering
{
    public class InterQuartileRangeFilter : BaseDataFilter
    {
        private double _multiple;

        public InterQuartileRangeFilter()
        {
            _multiple = 1.5;
        }

        public void SetInterQuartileRangeMultiple(double multiple)
        {
            if (multiple > 0)
            {
                _multiple = multiple;
            }
        }

        protected override Range<double>[] CalculateLimits(Matrix<double> input)
        {
            var quartiles = input.Data.ColumnQuartiles();

            return quartiles.Select(x =>
            {
                var range = (x.Upper - x.Lower) * _multiple;
                return new Range<double>(x.Upper + range, x.Lower - range);
            }).ToArray();
        }
    }
}
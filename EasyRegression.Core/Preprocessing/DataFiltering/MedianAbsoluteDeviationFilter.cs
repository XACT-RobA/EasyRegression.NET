using System;
using System.Linq;
using EasyRegression.Core.Common.Maths;
using EasyRegression.Core.Common.Models;
using Newtonsoft.Json;

namespace EasyRegression.Core.Preprocessing.DataFiltering
{
    public class MedianAbsoluteDeviationFilter : BaseDataFilter
    {
        private double _multiple;

        public MedianAbsoluteDeviationFilter()
        {
            _multiple = 3.0;
        }

        internal MedianAbsoluteDeviationFilter(double multiple)
        {
            SetMedianDeviationMultiple(multiple);
        }

        public void SetMedianDeviationMultiple(double multiple)
        {
            if (multiple > 0)
            {
                _multiple = multiple;
            }
        }

        // MAD = median( | Xi - median(X) | )
        protected override Range<double>[] CalculateLimits(Matrix<double> input)
        {
            var medians = input.Data.ColumnMedians();

            var medianDifferences = input.Data.Select(arr =>
            {
                return arr.Select((x, i) => Math.Abs(x - medians[i]))
                          .ToArray();
            }).ToArray();

            return medianDifferences.ColumnMedians().Select((x, i) =>
            {
                var range = x * _multiple;
                return new Range<double>(medians[i] + range, medians[i] - range);
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
using System;
using System.Linq;
using Xunit;
using EasyRegression.Core.Preprocessing.DataFiltering;
using EasyRegression.Core.Common.Models;

namespace EasyRegression.Test.Preprocessing.DataFiltering
{
    public static class InterQuartileRangeFilterTests
    {
        [Fact]
        public static void TestFilter()
        {
            var arr = new[]
            {
                1.0, 2.0, 2.0, 2.25, 2.5, 2.5,
                2.75, 3.0, 3.5, 4.0, 5.0, 1000.0,
            };
            
            var data = arr.Select(x => new[] {x})
                          .ToArray();

            var expected = data.Where(x => x[0] < 10.0)
                               .ToArray();

            var filter = new InterQuartileRangeFilter();
            var filtered = filter.Filter(new Matrix<double>(data));

            Assert.Equal(expected.Length, filtered.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i].Length, filtered[i].Length);
                Assert.Equal(expected[i][0], filtered[i][0]);
            }
        }
    }
}

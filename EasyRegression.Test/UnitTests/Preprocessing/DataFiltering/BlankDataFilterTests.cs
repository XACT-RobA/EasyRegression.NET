using System;
using System.Linq;
using Xunit;
using EasyRegression.Core;
using EasyRegression.Core.Preprocessing.DataFiltering;
using EasyRegression.Core.Common.Models;

namespace EasyRegression.Test.Preprocessing.DataFiltering
{
    public static class BlankDataFilterTests
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

            var y = arr.Select((x, i) => (double)i)
                       .ToArray();

            var filter = new BlankDataFilter();
            var filtered = filter.Filter(new TrainingModel<double>(data, y));

            Assert.Equal(data.Length, filtered.Length);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.Equal(data[i][0], filtered.X[i][0]);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;
using EasyRegression.Core.Common.Maths;

namespace EasyRegression.Test.Common.Maths
{
    public class StatisticsExtensionsTests
    {
        private readonly int _places = 6;

        // Numeric validation

        [Fact]
        public void TestValidDouble()
        {
            Assert.Equal(true, 1.0.IsValidDouble());
            Assert.Equal(true, (-1.0).IsValidDouble());
            Assert.Equal(true, 0.0.IsValidDouble());
            Assert.Equal(true, (-0.0).IsValidDouble());
            Assert.Equal(true, double.MaxValue.IsValidDouble());
            Assert.Equal(true, double.MinValue.IsValidDouble());
            Assert.Equal(false, double.NaN.IsValidDouble());
            Assert.Equal(false, double.PositiveInfinity.IsValidDouble());
            Assert.Equal(false, double.NegativeInfinity.IsValidDouble());
        }

        [Fact]
        public void TestValidNullableDouble()
        {
            Assert.Equal(((double?)null).IsValidDouble(), false);
        }

        // Middle

        [Fact]
        public void TestMiddle()
        {
            Assert.Equal(2.0, (new[] { 1.0, 2.0, 3.0 }).Middle(), _places);
            Assert.Equal(3.0, (new[] { 1.0, 3.0, 2.0 }).Middle(), _places);
            Assert.Equal(2.0, (new[] { 1.0, 3.0 }).Middle(), _places);
            Assert.Equal(2.0, (new[] { 2.0 }).Middle(), _places);
            Assert.Equal(0.0, (new double[0]).Middle(), _places);
        }

        // Statistics
        // Mean

        [Fact]
        public void TestMean()
        {
            Assert.Equal(2.0, (new[] { 1.0, 2.0, 3.0 }).Mean(), _places);
            Assert.Equal(2.0, (new[] { 2.0 }).Mean(), _places);
            Assert.Equal(2.0, (new[] { 1.0, 3.0, double.PositiveInfinity }).Mean(), _places);
            Assert.Equal(0.0, (new[] { double.NaN }).Mean(), _places);
            Assert.Equal(0.0, (new double[0]).Mean(), _places);
        }

        [Fact]
        public void TestNullableMean()
        {
            Assert.Equal(2.0, (new double?[] { 2.0, null }).Mean(), _places);
            Assert.Equal(0.0, (new double?[] { null }).Mean(), _places);
        }

        [Fact]
        public void TestColumnMean()
        {
            var data = new double[][]
            { 
                new[] { 1.0, 2.0 },
                new[] { 3.0, 4.0 },
            };
            Assert.Equal(2.0, data.ColumnMean(0), _places);
            Assert.Equal(3.0, data.ColumnMean(1), _places);
        }

        [Fact]
        public void TestNullableColumnMean()
        {
            var data = new double?[][]
            {
                new double?[] { 1.0, null },
                new double?[] { null, 2.0 },
                new double?[] { 3.0, 4.0 },
            };
            Assert.Equal(2.0, data.ColumnMean(0), _places);
            Assert.Equal(3.0, data.ColumnMean(1), _places);
        }

        [Fact]
        public void TestColumnMeans()
        {
            var data = new double[][]
            { 
                new[] { 1.0, 2.0 },
                new[] { 3.0, 4.0 },
            };
            var expected = new[] { 2.0, 3.0 };
            var actual = data.ColumnMeans();

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }

        [Fact]
        public void TestNullableColumnMeans()
        {
            var data = new double?[][]
            {
                new double?[] { 1.0, null },
                new double?[] { null, 2.0 },
                new double?[] { 3.0, 4.0 },
            };
            var expected = new[] { 2.0, 3.0 };
            var actual = data.ColumnMeans();

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }

        // Median

        [Fact]
        public void TestMedian()
        {
            Assert.Equal(2.0, (new[] { 1.0, 3.0, 2.0 }).Median(), _places);
            Assert.Equal(2.0, (new[] { 2.0 }).Median(), _places);
            Assert.Equal(2.0, (new[] { 2.0, double.PositiveInfinity }).Median(), _places);
            Assert.Equal(2.0, (new[] { 1.0, 3.0, double.NegativeInfinity }).Median(), _places);
            Assert.Equal(0.0, (new[] { double.NaN }).Median(), _places);
            Assert.Equal(0.0, (new double[0]).Median(), _places);
        }

        [Fact]
        public void TestNullableMedian()
        {
            Assert.Equal(2.0, (new double?[] { 2.0, null }).Median(), _places);
            Assert.Equal(0.0, (new double?[] { null }).Median(), _places);
        }

        [Fact]
        public void TestColumnMedian()
        {
            var data = new double[][]
            { 
                new[] { 1.0, 2.0 },
                new[] { 3.0, 4.0 },
                new[] { 2.0, double.NaN },
            };
            Assert.Equal(2.0, data.ColumnMedian(0), _places);
            Assert.Equal(3.0, data.ColumnMedian(1), _places);
        }

        [Fact]
        public void TestNullableColumnMedian()
        {
            var data = new double?[][]
            {
                new double?[] { 1.0, null },
                new double?[] { null, 2.0 },
                new double?[] { 3.0, 4.0 },
                new double?[] { 2.0, double.NaN },
            };
            Assert.Equal(2.0, data.ColumnMedian(0), _places);
            Assert.Equal(3.0, data.ColumnMedian(1), _places);
        }

        [Fact]
        public void TestColumnMedians()
        {
            var data = new double[][]
            { 
                new[] { 1.0, 2.0 },
                new[] { 3.0, 4.0 },
                new[] { 2.0, double.NaN },
            };
            var expected = new[] { 2.0, 3.0 };
            var actual = data.ColumnMedians();

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }

        [Fact]
        public void TestNullableColumnMedians()
        {
            var data = new double?[][]
            {
                new double?[] { 1.0, null },
                new double?[] { null, 2.0 },
                new double?[] { 3.0, 4.0 },
                new double?[] { 2.0, double.NaN },
            };
            var expected = new[] { 2.0, 3.0 };
            var actual = data.ColumnMedians();

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }
    }
}
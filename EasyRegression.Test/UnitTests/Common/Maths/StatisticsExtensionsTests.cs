using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;
using EasyRegression.Core.Common.Maths;
using System.Linq;

namespace EasyRegression.Test.Common.Maths
{
    public class StatisticsExtensionsTests
    {
        private const int _places = 6;
        private const double _max = double.MaxValue;
        private const double _min = double.MinValue;
        private const double _nan = double.NaN;
        private const double _pInf = double.PositiveInfinity;
        private const double _nInf = double.NegativeInfinity;
        private static readonly double? _null = null;

        // Numeric validation

        [Fact]
        public static void TestValidDouble()
        {
            Assert.Equal(true, 1.0.IsValidDouble());
            Assert.Equal(true, (-1.0).IsValidDouble());
            Assert.Equal(true, 0.0.IsValidDouble());
            Assert.Equal(true, (-0.0).IsValidDouble());
            Assert.Equal(true, _max.IsValidDouble());
            Assert.Equal(true, _min.IsValidDouble());
            Assert.Equal(false, _nan.IsValidDouble());
            Assert.Equal(false, _pInf.IsValidDouble());
            Assert.Equal(false, _nInf.IsValidDouble());
        }

        [Fact]
        public static void TestValidNullableDouble()
        {
            Assert.Equal(_null.IsValidDouble(), false);
        }

        // Middle

        [Fact]
        public static void TestMiddle()
        {
            Assert.Equal(2.0, (new[] { 1.0, 2.0, 3.0 }).Middle(), _places);
            Assert.Equal(3.0, (new[] { 1.0, 3.0, 2.0 }).Middle(), _places);
            Assert.Equal(2.0, (new[] { 1.0, 3.0 }).Middle(), _places);
            Assert.Equal(2.0, (new[] { 2.0 }).Middle(), _places);
            Assert.Equal(_nan, (new double[0]).Middle(), _places);
        }

        // Statistics
        // Mean

        [Fact]
        public static void TestMean()
        {
            Assert.Equal(2.0, (new[] { 1.0, 2.0, 3.0 }).Mean(), _places);
            Assert.Equal(2.0, (new[] { 2.0 }).Mean(), _places);
            Assert.Equal(2.0, (new[] { 1.0, 3.0, _pInf }).Mean(), _places);
            Assert.Equal(_nan, (new[] { _nan }).Mean(), _places);
            Assert.Equal(_nan, (new double[0]).Mean(), _places);
        }

        [Fact]
        public static void TestNullableMean()
        {
            Assert.Equal(2.0, (new double?[] { 2.0, null }).Mean(), _places);
            Assert.Equal(_nan, (new double?[] { null }).Mean(), _places);
        }

        [Fact]
        public static void TestColumnMean()
        {
            var data = new[]
            { 
                new[] { 1.0, 2.0 },
                new[] { 3.0, 4.0 },
            };

            Assert.Equal(2.0, data.ColumnMean(0), _places);
            Assert.Equal(3.0, data.ColumnMean(1), _places);
        }

        [Fact]
        public static void TestNullableColumnMean()
        {
            var data = new[]
            {
                new double?[] { 1.0,  null },
                new double?[] { null, 2.0  },
                new double?[] { 3.0,  4.0  },
            };

            Assert.Equal(2.0, data.ColumnMean(0), _places);
            Assert.Equal(3.0, data.ColumnMean(1), _places);
        }

        [Fact]
        public static void TestColumnMeans()
        {
            var data = new[]
            { 
                new[] { 1.0, 2.0 },
                new[] { 3.0, 4.0 },
            };
            var expected = new[] { 2.0, 3.0 };
            var actual = data.ColumnMeans();

            Assert.Equal(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }

        [Fact]
        public static void TestNullableColumnMeans()
        {
            var data = new[]
            {
                new double?[] { 1.0,  null },
                new double?[] { null, 2.0  },
                new double?[] { 3.0,  4.0  },
            };
            var expected = new[] { 2.0, 3.0 };
            var actual = data.ColumnMeans();

            Assert.Equal(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }

        // Median

        [Fact]
        public static void TestMedian()
        {
            Assert.Equal(2.0, (new[] { 1.0, 3.0, 2.0 }).Median(), _places);
            Assert.Equal(2.0, (new[] { 2.0 }).Median(), _places);
            Assert.Equal(2.0, (new[] { 2.0, _pInf }).Median(), _places);
            Assert.Equal(2.0, (new[] { 1.0, 3.0, _nInf }).Median(), _places);
            Assert.Equal(_nan, (new[] { _nan }).Median(), _places);
            Assert.Equal(_nan, (new double[0]).Median(), _places);
        }

        [Fact]
        public static void TestNullableMedian()
        {
            Assert.Equal(2.0, (new double?[] { 2.0, null }).Median(), _places);
            Assert.Equal(_nan, (new double?[] { null }).Median(), _places);
        }

        [Fact]
        public static void TestColumnMedian()
        {
            var data = new[]
            { 
                new[] { 1.0, 2.0  },
                new[] { 3.0, 4.0  },
                new[] { 2.0, _nan },
            };

            Assert.Equal(2.0, data.ColumnMedian(0), _places);
            Assert.Equal(3.0, data.ColumnMedian(1), _places);
        }

        [Fact]
        public static void TestNullableColumnMedian()
        {
            var data = new[]
            {
                new double?[] { 1.0,  null },
                new double?[] { null, 2.0  },
                new double?[] { 3.0,  4.0  },
                new double?[] { 2.0,  _nan },
            };

            Assert.Equal(2.0, data.ColumnMedian(0), _places);
            Assert.Equal(3.0, data.ColumnMedian(1), _places);
        }

        [Fact]
        public static void TestColumnMedians()
        {
            var data = new[]
            { 
                new[] { 1.0, 2.0  },
                new[] { 3.0, 4.0  },
                new[] { 2.0, _nan },
            };
            var expected = new[] { 2.0, 3.0 };
            var actual = data.ColumnMedians();

            Assert.Equal(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }

        [Fact]
        public static void TestNullableColumnMedians()
        {
            var data = new[]
            {
                new double?[] { 1.0,  null },
                new double?[] { null, 2.0  },
                new double?[] { 3.0,  4.0  },
                new double?[] { 2.0,  _nan },
            };
            var expected = new[] { 2.0, 3.0 };
            var actual = data.ColumnMedians();

            Assert.Equal(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }

        // Minimum

        [Fact]
        public static void TestColumnMinimum()
        {
            var data = new[]
            { 
                new[] { 1.0, 2.0,  3.0  },
                new[] { 3.0, 4.0,  -2.0 },
                new[] { 2.0, _nan, 1.0  },
            };

            Assert.Equal(1.0, data.ColumnMinimum(0), _places);
            Assert.Equal(2.0, data.ColumnMinimum(1), _places);
            Assert.Equal(-2.0, data.ColumnMinimum(2), _places);
        }

        [Fact]
        public static void TestColumnMinimums()
        {
            var data = new[]
            { 
                new[] { 1.0, 2.0,  3.0  },
                new[] { 3.0, 4.0,  -2.0 },
                new[] { 2.0, _nan, 1.0  },
            };
            var expected = new[] { 1.0, 2.0, -2.0 };
            var actual = data.ColumnMinimums();

            Assert.Equal(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }

        // Maximum

        [Fact]
        public static void TestColumnMaximum()
        {
            var data = new[]
            { 
                new[] { 1.0, 2.0,  3.0  },
                new[] { 3.0, 4.0,  -2.0 },
                new[] { 2.0, _nan, 1.0  },
            };
            
            Assert.Equal(3.0, data.ColumnMaximum(0), _places);
            Assert.Equal(4.0, data.ColumnMaximum(1), _places);
            Assert.Equal(3.0, data.ColumnMaximum(2), _places);
        }

        [Fact]
        public static void TestColumnMaximums()
        {
            var data = new[]
            { 
                new[] { 1.0, 2.0,  3.0  },
                new[] { 3.0, 4.0,  -2.0 },
                new[] { 2.0, _nan, 1.0  },
            };
            var expected = new[] { 3.0, 4.0, 3.0 };
            var actual = data.ColumnMaximums();

            Assert.Equal(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }

        // Variance

        [Fact]
        public static void TestVariance()
        {
            Assert.Equal(2.0 / 3.0, (new[] { 1.0, 2.0, 3.0 }).Variance(), _places);
            Assert.Equal(0.0, (new[] { 2.0 }).Variance(), _places);
            Assert.Equal(1.0, (new[] { 1.0, 3.0, _pInf }).Variance(), _places);
            Assert.Equal(_nan, (new[] { _nan }).Variance(), _places);
            Assert.Equal(_nan, (new double[0]).Variance(), _places);
        }

        [Fact]
        public static void TestColumnVariance()
        {
            var data = new[]
            { 
                new[] { 1.0, 2.0  },
                new[] { 2.0, 4.0  },
                new[] { 3.0, _nan },
            };

            Assert.Equal(2.0 / 3.0, data.ColumnVariance(0), _places);
            Assert.Equal(1.0, data.ColumnVariance(1), _places);
        }

        [Fact]
        public static void TestColumnVariances()
        {
            var data = new[]
            { 
                new[] { 1.0, 2.0  },
                new[] { 2.0, 4.0  },
                new[] { 3.0, _nan },
            };
            var expected = new[] { 2.0 / 3.0, 1.0 };
            var actual = data.ColumnVariances();

            Assert.Equal(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }

        // Standard deviation

        [Fact]
        public static void TestStandardDeviation()
        {
            Assert.Equal(0.81649658, (new[] { 1.0, 2.0, 3.0 }).StandardDeviation(), _places);
            Assert.Equal(0.0, (new[] { 2.0 }).StandardDeviation(), _places);
            Assert.Equal(1.0, (new[] { 1.0, 3.0, _pInf }).StandardDeviation(), _places);
            Assert.Equal(_nan, (new[] { _nan }).StandardDeviation(), _places);
            Assert.Equal(_nan, (new double[0]).StandardDeviation(), _places);
        }

        [Fact]
        public static void TestColumnStandardDeviation()
        {
            var data = new[]
            { 
                new[] { 1.0, 2.0  },
                new[] { 2.0, 4.0  },
                new[] { 3.0, _nan },
            };

            Assert.Equal(0.81649658, data.ColumnStandardDeviation(0), _places);
            Assert.Equal(1.0, data.ColumnStandardDeviation(1), _places);
        }

        [Fact]
        public static void TestColumnStandardDeviations()
        {
            var data = new[]
            { 
                new[] { 1.0, 2.0  },
                new[] { 2.0, 4.0  },
                new[] { 3.0, _nan },
            };
            var expected = new[] { 0.81649658, 1.0 };
            var actual = data.ColumnStandardDeviations();

            Assert.Equal(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }
    }
}
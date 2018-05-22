using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;
using EasyRegression.Core.Common.Maths;
using System.Linq;
using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Common.Exceptions;

namespace EasyRegression.Test.Common.Maths
{
    public static class StatisticsExtensionsTests
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
            Assert.Throws<NoValidDataException>(() => (new double[0]).Middle());
        }

        // Statistics
        // Mean

        [Fact]
        public static void TestMean()
        {
            Assert.Equal(2.0, (new[] { 1.0, 2.0, 3.0 }).Mean(), _places);
            Assert.Equal(2.0, (new[] { 2.0 }).Mean(), _places);
            Assert.Equal(2.0, (new[] { 1.0, 3.0, _pInf }).Mean(), _places);
            Assert.Throws<NoValidDataException>(() => (new[] { _nan }).Mean());
            Assert.Throws<NoValidDataException>(() => (new double[0]).Mean());
        }

        [Fact]
        public static void TestNullableMean()
        {
            Assert.Equal(2.0, (new double?[] { 2.0, null }).Mean(), _places);
            Assert.Throws<NoValidDataException>(() => (new double?[] { null }.Mean()));
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
            Assert.Throws<NoValidDataException>(() => (new[] { _nan }).Median());
            Assert.Throws<NoValidDataException>(() => (new double[0]).Median());
        }

        [Fact]
        public static void TestNullableMedian()
        {
            Assert.Equal(2.0, (new double?[] { 2.0, null }).Median(), _places);
            Assert.Throws<NoValidDataException>(() => (new double?[] { null }.Median()));
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
            Assert.Throws<NoValidDataException>(() => (new[] { _nan }).Variance());
            Assert.Throws<NoValidDataException>(() => (new double[0]).Variance());
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
            Assert.Throws<NoValidDataException>(() => (new[] { _nan }).StandardDeviation());
            Assert.Throws<NoValidDataException>(() => (new double[0]).StandardDeviation());
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

        // Quartiles

        [Fact]
        public static void TestQuartile()
        {
            var data = new[] { 3.0, 1.0, 4.0, 2.0, 6.0, 5.0, _nan };
            var expected = new Range<double>(5.0, 2.0);
            Assert.Equal(expected, data.Quartile());

            data = new[] { 3.0, 4.0, 1.0, 6.0, _pInf };
            Assert.Equal(expected, data.Quartile());

            data = new[] { 3.0, 1.0, 2.0, 6.0, 5.0, _nInf };
            Assert.Equal(expected, data.Quartile());

            data = new[] { 3.0, 7.0, 4.0, 4.0, 1.0, 6.0, 1.0, _nInf };
            Assert.Equal(expected, data.Quartile());
        }

        [Fact]
        public static void TestColumnQuartile()
        {
            var data = new[]
            {
                new[] { 3.0,  3.0,   3.0,   3.0 },
                new[] { 1.0,  4.0,   1.0,   7.0 },
                new[] { 4.0,  1.0,   2.0,   4.0 },
                new[] { 2.0,  6.0,   6.0,   4.0 },
                new[] { 6.0,  _nan,  5.0,   1.0 },
                new[] { 5.0,  _pInf, _nan,  6.0 },
                new[] { _nan, _nInf, _pInf, 1.0 },
            };

            var expected = new Range<double>(5.0, 2.0);

            Assert.Equal(expected, data.ColumnQuartile(0));
            Assert.Equal(expected, data.ColumnQuartile(1));
            Assert.Equal(expected, data.ColumnQuartile(2));
            Assert.Equal(expected, data.ColumnQuartile(3));
        }

        [Fact]
        public static void TestColumnQuartiles()
        {
            var data = new[]
            {
                new[] { 3.0,  3.0,   3.0,   3.0 },
                new[] { 1.0,  4.0,   1.0,   7.0 },
                new[] { 4.0,  1.0,   2.0,   4.0 },
                new[] { 2.0,  6.0,   6.0,   4.0 },
                new[] { 6.0,  _nan,  5.0,   1.0 },
                new[] { 5.0,  _pInf, _nan,  6.0 },
                new[] { _nan, _nInf, _pInf, 1.0 },
            };

            var expected = new Range<double>(5.0, 2.0);
            var actual = data.ColumnQuartiles();

            Assert.Equal(4, actual.Length);

            for (int i = 0; i < actual.Length; i++)
            {
                Assert.Equal(expected, actual[i]);
            }
        }

        // Shuffle

        [Fact]
        public static void TestShuffleWithSeed()
        {
            var arr = new[] { 1, 2, 3, 4, 5 };

            var shuffled0 = arr.Shuffle(new Random(0))
                               .ToArray();

            var shuffled1 = arr.Shuffle(new Random(1))
                               .Take(3)
                               .ToArray();

            var expected0 = new[] { 4, 5, 3, 2, 1 };

            var expected1 = new[] { 2, 1, 4 };

            Assert.Equal(expected0.Length, shuffled0.Length);

            for (int i = 0; i < expected0.Length; i++)
            {
                Assert.Equal(expected0[i], shuffled0[i]);
            }

            Assert.Equal(expected1.Length, shuffled1.Length);

            for (int i = 0; i < expected1.Length; i++)
            {
                Assert.Equal(expected1[i], shuffled1[i]);
            }
        }

        [Fact]
        public static void TestShuffle()
        {
            var arr = new[] { 1, 2, 3, 4, 5 };

            var shuffled = arr.Shuffle()
                              .ToArray();

            Assert.Equal(arr.Length, shuffled.Length);
            Assert.Equal(arr.Length, shuffled.Distinct().Count());

            for (int i = 0; i < shuffled.Length; i++)
            {
                var value = shuffled[i];
                Assert.True(arr.Contains(value));
            }
        }
    }
}
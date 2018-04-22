using System;
using Xunit;
using EasyRegression.Core;
using EasyRegression.Core.Preprocessing.DataPatching;
using EasyRegression.Core.Common.Models;

namespace EasyRegression.Test.Preprocessing.DataPatching
{
    public static class MeanDataPatcherTests
    {
        private const int _places = 6;
        private const double _nan = double.NaN;
        private const double _pInf = double.PositiveInfinity;
        private const double _nInf = double.NegativeInfinity;

        [Fact]
        public static void TestPatch()
        {
            var data = new[]
            {
                new[] { 1.0,   3.0,   _nan },
                new[] { 3.0,   _pInf, 3.0  },
                new[] { _nInf, 1.0,   1.0  },
            };

            var expectedData = new[]
            {
                new[] { 1.0, 3.0, 2.0 },
                new[] { 3.0, 2.0, 3.0 },
                new[] { 2.0, 1.0, 1.0 },
            };

            var meanPatcher = new MeanDataPatcher();

            var matrix = new Matrix<double>(data);
            var expected = new Matrix<double>(expectedData);
            var actual = meanPatcher.Patch(matrix);

            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected.Width, actual.Width);

            for (int i = 0; i < expected.Length; i++)
            {
                for (int j = 0; j < expected.Width; j++)
                {
                    Assert.Equal(expected[i][j], actual[i][j], _places);
                }
            }
        }

        [Fact]
        public static void TestNullablePatch()
        {
            var meanPatcher = new MeanDataPatcher();

            var data = new[]
            {
                new double?[] { 1.0,   3.0,   _nan },
                new double?[] { null,  _pInf, 3.0  },
                new double?[] { _nInf, 1.0,   null },
                new double?[] { 3.0,   null,  1.0  },
            };

            var expectedData = new[]
            {
                new[] { 1.0, 3.0, 2.0 },
                new[] { 2.0, 2.0, 3.0 },
                new[] { 2.0, 1.0, 2.0 },
                new[] { 3.0, 2.0, 1.0 },
            };

            var matrix = new Matrix<double?>(data);
            var expected = new Matrix<double>(expectedData);
            var actual = meanPatcher.Patch(matrix);

            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected.Width, actual.Width);

            for (int i = 0; i < expected.Length; i++)
            {
                for (int j = 0; j < expected.Width; j++)
                {
                    Assert.Equal(expected[i][j], actual[i][j], _places);
                }
            }
        }

        [Fact]
        public static void TestRePatch()
        {
            var meanPatcher = new MeanDataPatcher();

            var trainingData = new[]
            {
                new[] { 1.0,   3.0,   _nan },
                new[] { 3.0,   _pInf, 3.0  },
                new[] { _nInf, 1.0,   1.0  },
            };

            var matrix = new Matrix<double>(trainingData);
            meanPatcher.Patch(matrix);

            var testingData = new[] { _nan, _pInf, _nInf };
            var expectedData = new[] { 2.0, 2.0, 2.0 };
            var actual = meanPatcher.RePatch(testingData);

            Assert.Equal(expectedData.Length, actual.Length);

            for (int i = 0; i < expectedData.Length; i++)
            {
                Assert.Equal(expectedData[i], actual[i], _places);
            }
        }

        [Fact]
        public static void TestNullableRePatch()
        {
            var meanPatcher = new MeanDataPatcher();

            var trainingData = new[]
            {
                new double?[] { 1.0,   3.0,   _nan },
                new double?[] { null,  _pInf, 3.0  },
                new double?[] { _nInf, 1.0,   null },
                new double?[] { 3.0,   null,  1.0  },
            };

            var matrix = new Matrix<double?>(trainingData);
            meanPatcher.Patch(matrix);

            var testingData = new double?[] { null, _nan, _pInf };
            var expectedData = new[] { 2.0, 2.0, 2.0 };
            var actual = meanPatcher.RePatch(testingData);

            Assert.Equal(expectedData.Length, actual.Length);

            for (int i = 0; i < expectedData.Length; i++)
            {
                Assert.Equal(expectedData[i], actual[i], _places);
            }
        }

        [Fact]
        public static void TestSerialise()
        {
            var data = new[]
            {
                new[] { 1.0,   3.0,   _nan },
                new[] { 3.0,   _pInf, 3.0  },
                new[] { _nInf, 1.0,   1.0  },
            };

            var meanPatcher = new MeanDataPatcher();
            meanPatcher.Patch(new Matrix<double>(data));

            var serialised = meanPatcher.Serialise();
            var deserialised = BaseDataPatcher.Deserialise(serialised);

            var testingData = new[] { _nan, _pInf, _nInf };
            var expectedData = new[] { 2.0, 2.0, 2.0 };
            var actual = deserialised.RePatch(testingData);

            Assert.Equal(expectedData.Length, actual.Length);

            for (int i = 0; i < expectedData.Length; i++)
            {
                Assert.Equal(expectedData[i], actual[i], _places);
            }
        }
    }
}
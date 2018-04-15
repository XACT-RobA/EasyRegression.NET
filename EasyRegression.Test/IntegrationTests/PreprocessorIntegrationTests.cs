using System;
using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Preprocessing;
using Xunit;

namespace EasyRegression.Test.Integration
{
    public static class PreprocessorIntegrationTests
    {
        private const int _places = 6;
        private const double _nan = double.NaN;

        private static double[][] data
        {
            get
            {
                return new[]
                {
                    new[] { 1.0, _nan },
                    new[] { 2.0, 3.0 },
                    new[] { 3.0, 1.0 },
                    new[] { _nan, 2.0 },
                };
            }
        }

        private static double[][] expectedData
        {
            get
            {
                return new[]
                {
                    new[] { 1.0, -1.41421356, 0.0         },
                    new[] { 1.0, 0.0        , 1.41421356  },
                    new[] { 1.0, 1.41421356 , -1.41421356 },
                    new[] { 1.0, 0.0        , 0.0         },
                };
            }
        }

        [Fact]
        public static void TestDefault()
        {
            var input = new Matrix<double>(data);
            var expected = new Matrix<double>(expectedData);

            var preprocessor = new Preprocessor();
            var actual = preprocessor.Preprocess(input);

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
        public static void TestSerialise()
        {
            var preprocessor = new Preprocessor();
            preprocessor.Preprocess(new Matrix<double>(data));

            var serialised = preprocessor.Serialise();
            var deserialised = Preprocessor.Deserialise(serialised);

            for (int i = 0; i < data.Length; i++)
            {
                var expected = expectedData[i];
                var actual = deserialised.Reprocess(data[i]);

                for (int j = 0; j < expected.Length; j++)
                {
                    Assert.Equal(expected[j], actual[j], _places);
                }
            }
        }
    }
}
using System;
using Xunit;
using EasyRegression.Core.Preprocessing.DataSmoothing;
using EasyRegression.Core.Common.Models;

namespace EasyRegression.Test.Preprocessing.DataSmoothing
{
    public static class NormalisationTests
    {
        private const int _places = 6;

        private static double[][] trainingData
        {
            get
            {
                return new[]
                {
                    new[] {  1.0,  2.0, 5.0 },
                    new[] {  1.0,  3.0, 4.0 },
                    new[] { -1.0,  4.0, 1.0 },
                };
            }
        }

        [Fact]
        public static void TestSmooth()
        {
            var normaliser = new DataNormaliser();

            var expectedData = new[]
            {
                new[] { 1.0, 0.0, 1.0  },
                new[] { 1.0, 0.5, 0.75 },
                new[] { 0.0, 1.0, 0.0  },
            };

            var matrix = new Matrix<double>(trainingData);
            var expected = new Matrix<double>(expectedData);
            var actual = normaliser.Smooth(matrix);

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
        public static void TestReSmoothInRange()
        {
            var matrix = new Matrix<double>(trainingData);
            var normaliser = new DataNormaliser();
            normaliser.Smooth(matrix);

            var testing = new[] { 0.5, 2.5, 2.0 };
            var expected = new[] { 0.75, 0.25, 0.25 };
            var actual = normaliser.ReSmooth(testing);

            Assert.Equal(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }

        [Fact]
        public static void TestReSmoothOutOfRange()
        {
            var matrix = new Matrix<double>(trainingData);
            var normaliser = new DataNormaliser();
            normaliser.Smooth(matrix);

            var testing = new[] { 2.0, 1.0, -1.0 };
            var expected = new[] { 1.5, -0.5, -0.5 };
            var actual = normaliser.ReSmooth(testing);

            Assert.Equal(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }
    }
}
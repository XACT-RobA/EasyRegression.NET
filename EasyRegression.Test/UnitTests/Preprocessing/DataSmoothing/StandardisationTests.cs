using System;
using Xunit;
using EasyRegression.Core.Preprocessing.DataSmoothing;
using EasyRegression.Core.Common.Models;

namespace EasyRegression.Test.Preprocessing.DataSmoothing
{
    public static class StandardisationTests
    {
        private const int _places = 6;

        private static double[][] trainingData
        {
            get
            {
                return new[]
                {
                    new[] { 1.0,  2.0, 5.0 },
                    new[] { 1.0,  3.0, 4.0 },
                    new[] { -1.0, 4.0, 1.0 },
                };
            }
        }

        [Fact]
        public static void TestSmooth()
        {
            var standardiser = new DataStandardiser();

            var expectedData = new[]
            {
                new[] {  0.70710678, -1.22474487, 0.98058068 },
                new[] {  0.70710678,  0.0,        0.39223227 },
                new[] { -1.41421356, 1.22474487, -1.37281295 },
            };

            var matrix = new Matrix<double>(trainingData);
            var expected = new Matrix<double>(expectedData);
            var actual = standardiser.Smooth(matrix);

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
            var standardiser = new DataStandardiser();
            standardiser.Smooth(matrix);

            var testing = new[] { 0.5, 2.5, 2.0 };
            var expected = new[] { 0.17677670, -0.61237244, -0.78446454 };
            var actual = standardiser.ReSmooth(testing);

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
            var standardiser = new DataStandardiser();
            standardiser.Smooth(matrix);

            var testing = new[] { 2.0, 1.0, -1.0 };
            var expected = new[] { 1.76776695, -2.44948974, -2.54950975 };
            var actual = standardiser.ReSmooth(testing);

            Assert.Equal(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }
    }
}
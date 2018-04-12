using System;
using Xunit;
using EasyRegression.Core.Common.Maths;
using EasyRegression.Core.Common.Models;

namespace EasyRegression.Test.Common.Maths
{
    public static class MatrixExtensionTests
    {
        private const int _places = 6;

        [Fact]
        public static void TestVectorDotProduct()
        {
            var a = new[] { 1.0, 2.0 };
            var b = new[] { 3.0, 4.0 };

            Assert.Equal(11.0, a.DotProduct(b), _places);
            Assert.Equal(11.0, b.DotProduct(a), _places);
        }

        [Fact]
        public static void TestMatrixVectorDotProduct()
        {
            var data = new[]
            {
                new[] { 1.0, 2.0 },
                new[] { 3.0, 4.0 },
            };

            var a = new Matrix<double>(data);
            var b = new[] { 5.0, 6.0 };

            var expected = new[] { 17.0, 39.0 };
            var actual = a.DotProduct(b);

            Assert.Equal(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }
    }
}
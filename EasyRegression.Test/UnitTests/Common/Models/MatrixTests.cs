using System;
using EasyRegression.Core.Common.Models;
using Xunit;

namespace EasyRegression.Test.Common.Models
{
    public class MatrixTests
    {
        [Fact]
        public void TestValidMatrix()
        {
            var data = new[]
            {
                new[] { 1.0, 2.0 },
                new[] { 3.0, 4.0 },
                new[] { 5.0, 6.0 }
            };

            var matrix = new Matrix<double>(data);

            Assert.Equal(3, matrix.Length);
            Assert.Equal(2, matrix.Width);

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Width; j++)
                {
                    Assert.Equal(data[i][j], matrix[i][j]);
                }
            }
        }

        [Fact]
        public void TestZeroLengthMatrix()
        {
            var data = new double[0][];

            Assert.Throws(typeof(ArgumentException), () => new Matrix<double>(data));
        }

        [Fact]
        public void TestZeroWidthMatrix()
        {
            var data = new[] { new double[0] };

            Assert.Throws(typeof(ArgumentException), () => new Matrix<double>(data));
        }

        [Fact]
        public void TestUnevenMatrix()
        {
            var data = new[]
            {
                new[] { 1.0 },
                new[] { 2.0, 3.0 }
            };

            Assert.Throws(typeof(ArgumentException), () => new Matrix<double>(data));
        }
    }
}
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;
using EasyRegression.Core.Common;

namespace EasyRegression.Test
{
    public class MathsTests
    {
        private double _delta = 1e-8;

        // Statistics
        // Mean

        private static IEnumerable<object[]> TestMeanData()
        {
            yield return new object[] { new double[] { 1.0, 2.0, 3.0 }, 2.0 };
            yield return new object[] { new double[] { 1.0, 3.0 }, 2.0 };
            yield return new object[] { new double[] { 2.0 }, 2.0 };
            yield return new object[] { new double[] { 2.0, double.NaN }, 2.0 };
            yield return new object[] { new double[] { 1.0, 3.0, double.PositiveInfinity }, 2.0 };
            yield return new object[] { new double[] { 1.0, 2.0, 3.0, double.NegativeInfinity }, 2.0 };
            yield return new object[] { new double[] { double.NaN }, 1.0 };
        }

        [Theory, MemberData(nameof(TestMeanData))]
        public void TestMean(double[] data, double answer)
        {
            double error = Math.Abs(data.Mean() - answer);

            Assert.True(error < _delta);
        }

        private static IEnumerable<object[]> TestNullableMeanData()
        {
            yield return new object[] { new double?[] { 1.0, 2.0, 3.0 }, 2.0 };
            yield return new object[] { new double?[] { 1.0, 3.0 }, 2.0 };
            yield return new object[] { new double?[] { 2.0 }, 2.0 };
            yield return new object[] { new double?[] { 2.0, double.NaN }, 2.0 };
            yield return new object[] { new double?[] { 1.0, 3.0, double.PositiveInfinity }, 2.0 };
            yield return new object[] { new double?[] { 1.0, 2.0, 3.0, double.NegativeInfinity }, 2.0 };
            yield return new object[] { new double?[] { double.NaN }, 1.0 };
            yield return new object[] { new double?[] { 2.0, null }, 2.0 };
            yield return new object[] { new double?[] { null }, 1.0 };
        }

        [Theory, MemberData(nameof(TestNullableMeanData))]
        public void TestNullableMean(double?[] data, double answer)
        {
            double error = Math.Abs(data.Mean() - answer);

            Assert.True(error < _delta);
        }
    }
}
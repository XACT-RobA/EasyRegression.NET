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

        // Numeric validation

        private static IEnumerable<object[]> TestValidDoubleData()
        {
            yield return new object[] { 1.0, true };
            yield return new object[] { -1.0, true };
            yield return new object[] { 0.0, true };
            yield return new object[] { -0.0, true };
            yield return new object[] { double.MaxValue, true };
            yield return new object[] { double.MinValue, true };
            yield return new object[] { double.NaN, false };
            yield return new object[] { double.PositiveInfinity, false };
            yield return new object[] { double.NegativeInfinity, false };
        }

        [Theory, MemberData(nameof(TestValidDoubleData))]
        public void TestValidDouble(double input, bool answer)
        {
            Assert.Equal(input.IsValidDouble(), answer);
        }

        private static IEnumerable<object[]> TestValidNullableDoubleData()
        {
            foreach (var data in TestValidDoubleData()) yield return data;
            yield return new object[] { null, false };
        }

        [Theory, MemberData(nameof(TestValidNullableDoubleData))]
        public void TestValidNullableDouble(double? input, bool answer)
        {
            Assert.Equal(input.IsValidDouble(), answer);
        }

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
            yield return new object[] { new double[] { double.NaN }, 0.0 };
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
            yield return new object[] { new double?[] { double.NaN }, 0.0 };
            yield return new object[] { new double?[] { 2.0, null }, 2.0 };
            yield return new object[] { new double?[] { null }, 0.0 };
        }

        [Theory, MemberData(nameof(TestNullableMeanData))]
        public void TestNullableMean(double?[] data, double answer)
        {
            double error = Math.Abs(data.Mean() - answer);
            Assert.True(error < _delta);
        }

        private static IEnumerable<object[]> TestMedianData()
        {
            yield return new object[] { new double[] { 1.0, 2.0, 3.0 }, 2.0 };
            yield return new object[] { new double[] { 1.0, 3.0, 2.0 }, 2.0 };
            yield return new object[] { new double[] { 1.0, 3.0 }, 2.0 };
            yield return new object[] { new double[] { 2.0 }, 2.0 };
            yield return new object[] { new double[] { 2.0, double.NaN }, 2.0 };
            yield return new object[] { new double[] { 1.0, 3.0, double.PositiveInfinity }, 2.0 };
            yield return new object[] { new double[] { 1.0, 2.0, 3.0, double.NegativeInfinity }, 2.0 };
            yield return new object[] { new double[] { 1.0, 3.0, 2.0, double.NegativeInfinity }, 2.0 };
            yield return new object[] { new double[] { double.NaN }, 0.0 };
        }

        [Theory, MemberData(nameof(TestMedianData))]
        public void TestMedian(double[] data, double answer)
        {
            double error = Math.Abs(data.Median() - answer);
            Assert.True(error < _delta);
        }

        private static IEnumerable<object[]> TestNullableMedianData()
        {
            yield return new object[] { new double?[] { 1.0, 2.0, 3.0 }, 2.0 };
            yield return new object[] { new double?[] { 1.0, 3.0, 2.0 }, 2.0 };
            yield return new object[] { new double?[] { 1.0, 3.0 }, 2.0 };
            yield return new object[] { new double?[] { 2.0 }, 2.0 };
            yield return new object[] { new double?[] { 2.0, double.NaN }, 2.0 };
            yield return new object[] { new double?[] { 1.0, 3.0, double.PositiveInfinity }, 2.0 };
            yield return new object[] { new double?[] { 1.0, 2.0, 3.0, double.NegativeInfinity }, 2.0 };
            yield return new object[] { new double?[] { 1.0, 3.0, 2.0, double.NegativeInfinity }, 2.0 };
            yield return new object[] { new double?[] { double.NaN }, 0.0 };
            yield return new object[] { new double?[] { 2.0, null }, 2.0 };
            yield return new object[] { new double?[] { null }, 0.0 };
        }

        [Theory, MemberData(nameof(TestNullableMedianData))]
        public void TestNullableMedian(double?[] data, double answer)
        {
            double error = Math.Abs(data.Median() - answer);
            Assert.True(error < _delta);
        }
    }
}
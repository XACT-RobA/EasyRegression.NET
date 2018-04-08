using System;
using System.Collections.Generic;
using Xunit;
using EasyRegression.Core.Common;
using System.Linq;

namespace EasyRegression.Test.Common
{
    public class PolynomialTreeTests
    {
        private int _places = 6;

        private static IEnumerable<object[]> TestData()
        {
            yield return new object[] { new[] { 2.0, 3.0 }, 1, new[] { 1.0, 3.0, 2.0, 6.0 } };
            yield return new object[] { new[] { 2.0, 3.0 }, 2, new[] { 1.0, 3.0, 9.0, 2.0, 6.0, 18.0, 4.0, 12.0, 36.0 } };
        }

        [Theory, MemberData(nameof(TestData))]
        public void Test(double[] input, int order, double[] expected)
        {
            var tree = new PolynomialTree(order, input);
            var output = tree.GetExpandedData();

            System.Console.WriteLine($"Output: {string.Join(", ", output)}");

            Assert.Equal(output.Length, expected.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], output[i], _places);
            }
        }
    }
}
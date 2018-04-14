using System.Linq;
using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Preprocessing.DataFiltering
{
    public class BaseDataFilter : IDataFilter
    {
        protected virtual Range<double>[] CalculateLimits(Matrix<double> input)
        {
            throw new System.NotImplementedException();
        }

        protected virtual bool IsWithinLimits(double[] row, Range<double>[] limits)
        {
            for (int i = 0; i < row.Length; i++)
            {
                if (row[i] > limits[i].Upper ||
                    row[i] < limits[i].Lower)
                {
                    return false;
                }
            }
            return true;
        }

        public Matrix<double> Filter(Matrix<double> input)
        {
            var limits = CalculateLimits(input);
            var data = input.Data.Where(row => IsWithinLimits(row, limits))
                                 .ToArray();
            return new Matrix<double>(data);
        }

        public string Serialise()
        {
            throw new System.NotImplementedException();
        }

        public IDataFilter Deserialise(string data)
        {
            throw new System.NotImplementedException();
        }
    }
}
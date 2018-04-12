using EasyRegression.Core.Common;
using EasyRegression.Core.Common.Maths;

namespace EasyRegression.Core.Preprocessing.DataPatching
{
    public abstract class BaseDataPatcher : IDataPatcher
    {
        protected double[] _parameters;

        protected virtual void CalculateParameters(Matrix<double?> input)
        {
            _parameters = new double[input.Width];
        }

        protected virtual void CalculateParameters(Matrix<double> input)
        {
            _parameters = new double[input.Width];
        }

        public Matrix<double> Patch(Matrix<double?> input)
        {
            CalculateParameters(input);

            int length = input.Length;
            int width = input.Width;

            double[][] patchedData = new double[length][];

            for (int il = 0; il < length; il++)
            {
                patchedData[il] = new double[width];

                for (int iw = 0; iw < width; iw++)
                {
                    var value = input[il][iw];

                    patchedData[il][iw] = value.IsValidDouble() ?
                        value.Value : _parameters[iw];
                }
            }

            return new Matrix<double>(patchedData);
        }

        public Matrix<double> Patch(Matrix<double> input)
        {
            CalculateParameters(input);

            int length = input.Length;
            int width = input.Width;

            double[][] patchedData = new double[length][];

            for (int il = 0; il < length; il++)
            {
                patchedData[il] = new double[width];

                for (int iw = 0; iw < width; iw++)
                {
                    var value = input[il][iw];

                    patchedData[il][iw] = value.IsValidDouble() ?
                        value : _parameters[iw];
                }
            }

            return new Matrix<double>(patchedData);
        }

        public double[] RePatch(double?[] data)
        {
            int width = data.Length;

            double[] patchedData = new double[width];

            for (int i = 0; i < width; i++)
            {
                var value = data[i];

                patchedData[i] = value.IsValidDouble() ?
                    value.Value : _parameters[i];
            }

            return patchedData;
        }

        public double[] RePatch(double[] data)
        {
            int width = data.Length;

            double[] patchedData = new double[width];

            for (int i = 0; i < width; i++)
            {
                var value = data[i];

                patchedData[i] = value.IsValidDouble() ?
                    value : _parameters[i];
            }

            return patchedData;
        }

        public void StoreParameters(string filepath)
        {
            throw new System.NotImplementedException();
        }

        public void LoadParameters(string filepath)
        {
            throw new System.NotImplementedException();
        }
    }
}
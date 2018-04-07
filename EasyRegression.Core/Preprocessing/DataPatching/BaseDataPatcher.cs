using EasyRegression.Core.Common;

namespace EasyRegression.Core.Preprocessing.DataPatching
{
    public abstract class BaseDataPatcher : IDataPatcher
    {
        protected double[] _parameters;

        protected virtual void CalculateParameters(double?[][] data)
        {
            _parameters = new double[data[0].Length];
        }

        protected virtual void CalculateParameters(double[][] data)
        {
            _parameters = new double[data[0].Length];
        }

        public double[][] Patch(double?[][] data)
        {
            CalculateParameters(data);

            int length = data.Length;
            int width = data[0].Length;

            double[][] patchedData = new double[length][];

            for (int il = 0; il < length; il++)
            {
                patchedData[il] = new double[width];

                for (int iw = 0; iw < width; iw++)
                {
                    var value = data[il][iw];

                    patchedData[il][iw] = value.IsValidDouble() ?
                        value.Value : _parameters[iw];
                }
            }

            return patchedData;
        }

        public double[][] Patch(double[][] data)
        {
            CalculateParameters(data);

            int length = data.Length;
            int width = data[0].Length;

            double[][] patchedData = new double[length][];

            for (int il = 0; il < length; il++)
            {
                patchedData[il] = new double[width];

                for (int iw = 0; iw < width; iw++)
                {
                    var value = data[il][iw];

                    patchedData[il][iw] = value.IsValidDouble() ?
                        value : _parameters[iw];
                }
            }

            return patchedData;
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
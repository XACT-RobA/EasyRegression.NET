namespace EasyRegression.Core.Preprocessing.DataSmoothing
{
    public abstract class BaseDataSmoother : IDataSmoother
    {
        protected double[] _subtractors;
        protected double[] _divisors;

        protected virtual void CalculateParameters(double[][] data)
        {
            throw new System.NotImplementedException();
        }

        public virtual double[][] Smooth(double[][] data)
        {
            CalculateParameters(data);

            int length = data.Length;
            int width = data[0].Length;

            double[][] smoothedData = new double[length][];

            for (int il = 0; il < length; il++)
            {
                smoothedData[il] = new double[width];

                for (int iw = 0; iw < width; iw++)
                {
                    smoothedData[il][iw] = (data[il][iw] - _subtractors[iw])
                        / _divisors[iw];
                }
            }

            return smoothedData;
        }

        public virtual double[] ReSmooth(double[] data)
        {
            int width = data.Length;

            double[] smoothedData = new double[width];

            for (int i = 0; i < width; i++)
            {
                smoothedData[i] = (data[i] - _subtractors[i]) / _divisors[i];
            }
            
            return smoothedData;
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
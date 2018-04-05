using EasyRegression.Core.Definitions;
using EasyRegression.Core.Common;

namespace EasyRegression.Core.Preprocessing
{
    public class MissingDataPatcher : IPreprocessingPlugin
    {
        private PreprocessingDefinitions.MissingDataPatchMethod _missingDataPatchMethod;
        private double?[][] _inputdata;
        private double[] _fillParameters;
        private int _width;

        public MissingDataPatcher(double?[][] inputdata)
        {
            _missingDataPatchMethod = PreprocessingDefinitions.MissingDataPatchMethod.mean;

            _inputdata = inputdata;
        }

        public MissingDataPatcher(double?[][] inputdata,
            PreprocessingDefinitions.MissingDataPatchMethod missingDataPatchMethod)
            : this(inputdata)
        {
            _missingDataPatchMethod = missingDataPatchMethod;
        }

        public double[][] Process()
        {
            return FillData();
        }

        public double[] Reprocess(double?[] inputdata)
        {
            var outputdata = new double[_width];

            for (int iw = 0; iw < _width; iw++)
            {
                outputdata[iw] = inputdata[iw].IsValidDouble() ?
                    inputdata[iw].Value : _fillParameters[iw];
            }

            return outputdata;
        }

        public void StoreParameters(string filepath)
        {
            throw new System.NotImplementedException();
        }

        public void LoadParameters(string filepath)
        {
            throw new System.NotImplementedException();
        }

        private void PopulateParameters()
        {
            _width = _inputdata[0].Length;

            _fillParameters = new double[_width];

            for (int i = 0; i < _width; i++)
            {
                _fillParameters[i] = CalculateParameter(i);
            }
        }

        private double CalculateParameter(int column)
        {
            switch (_missingDataPatchMethod)
            {
                case (PreprocessingDefinitions.MissingDataPatchMethod.median):
                    return _inputdata.ColumnMedian(column);
                case (PreprocessingDefinitions.MissingDataPatchMethod.zero):
                    return 0.0;
                case (PreprocessingDefinitions.MissingDataPatchMethod.mean):
                default:
                    return _inputdata.ColumnMean(column);
            }
        }

        private double[][] FillData()
        {
            int length = _inputdata.Length;

            var outputdata = new double[length][];

            for (int il = 0; il < length; il++)
            {
                outputdata[il] = new double[_width];

                for (int iw = 0; iw < _width; iw++)
                {
                    outputdata[il][iw] = _inputdata[il][iw].IsValidDouble() ?
                        _inputdata[il][iw].Value : _fillParameters[iw];
                }
            }

            return outputdata;
        }
    }
}
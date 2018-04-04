using EasyRegression.Core.Definitions;
using EasyRegression.Core.Common;

namespace EasyRegression.Core.Preprocessing
{
    public class MissingDataPatcher : IPreprocessingPlugin
    {
        private PreprocessingDefinitions.MissingDataPatchMethod _missingDataPatchMethod;

        private double?[][] _inputdata;
        private double[] _fillParameters;

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
            throw new System.NotImplementedException();
        }

        public double[] Reprocess(double[] input)
        {
            throw new System.NotImplementedException();
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
            int length = _inputdata.Length;
            int width = _inputdata[0].Length;

            _fillParameters = new double[width];

            for (int i = 0; i < width; i++)
            {
                _fillParameters[i] = CalculateParameter(i);
            }
        }

        private double CalculateParameter(int column)
        {
            switch (_missingDataPatchMethod)
            {
                case (PreprocessingDefinitions.MissingDataPatchMethod.median):
                    throw new System.NotImplementedException();
                case (PreprocessingDefinitions.MissingDataPatchMethod.zero):
                    return 0.0;
                case (PreprocessingDefinitions.MissingDataPatchMethod.mean):
                default:
                    return _inputdata.ColumnMean(column);
            }
        }

        private double[][] FillData()
        {
            throw new System.NotImplementedException();
        }
    }
}
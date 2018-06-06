using EasyRegression.Core.Common.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyRegression.Core.Validation
{
    public class LinearRegessionCrossValidator<T>
    {
        private int _folds;
        private bool _shuffleEveryFold;
        private double[] _averageErrors;
        private TrainingModel<T>[] _dataChunks;

        public LinearRegessionCrossValidator()
        {
            _folds = 5;
            _averageErrors = new double[_folds];
            _shuffleEveryFold = false;
        }

        public void SetFolds(int folds)
        {
            _folds = folds;
        }

        public void Validate(LinearRegressionEngine regressionEngine, TrainingModel<T> trainingData)
        {
            Split(trainingData);

            for (int fold = 0; fold < _folds; fold++)
            {
                var train = GetFoldTrainingData(fold);
                var test = GetFoldTestingData(fold);

                var errors = new List<double>();

                if (typeof(T) == typeof(double))
                {
                    regressionEngine.Train(train as TrainingModel<double>);
                    for (int i = 0; i < test.Length; i++)
                    {
                        var prediction = regressionEngine.Predict(test.X[i] as double[]);
                        var error = Math.Abs(prediction - test.Y[i]);

                    }
                }
                else if (typeof(T) == typeof(double?))
                {
                    regressionEngine.Train(train as TrainingModel<double?>);
                }
            }

            // shuffle into n groups of data
            // train model with (n-1)/n of the data
            // test model with other 1/n of the data
            // repeat for each group
        }

        private void Split(TrainingModel<T> trainingData)
        {
            var dataList = new List<TrainingModel<T>>(_folds);

            int chunkSize = (trainingData.Length / _folds) + 1;

            var indexes = Enumerable.Range(0, trainingData.Length).ToArray();
            var shuffledIndexes = indexes.Shuffle<int>();

            for (int i = 0; i < _folds; i++)
            {
                var xData = new List<T[]>(chunkSize);
                var yData = new List<double>(chunkSize);

                var chunkIndexes = shuffledIndexes.Skip(i * chunkSize).Take(chunkSize);

                foreach (var index in chunkIndexes)
                {
                    xData.Add(trainingData.X[index]);
                    yData.Add(trainingData.Y[index]);
                }

                dataList.Add(new TrainingModel<T>(xData.ToArray(), yData.ToArray()));
            }

            _dataChunks = dataList.ToArray();
        }

        private TrainingModel<T> GetFoldTrainingData(int fold)
        {
            return TrainingModel<T>.Combine(_dataChunks.Where((x, i) => i != fold).ToArray());
        }

        private TrainingModel<T> GetFoldTestingData(int fold)
        {
            return _dataChunks[fold];
        }
    }
}

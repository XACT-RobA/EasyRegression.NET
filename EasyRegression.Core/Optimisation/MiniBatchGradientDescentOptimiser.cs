using System;
using System.Collections.Generic;
using System.Linq;
using EasyRegression.Core.Common.Maths;
using Newtonsoft.Json;

namespace EasyRegression.Core.Optimisation
{
    public class MiniBatchGradientDescentOptimiser : BaseOptimiser
    {
        private int _length;
        private int _width;

        private double[][] _x;
        private double[] _y;

        private double _learn;
        private double _limit;        
        private int _batchSize;
        private int _iter;
        private int _maxIter;
        private bool _converged;
        private bool _diverged;
        private List<double> _errors;

        private double[] _params;
        private double[] _diffs;
        private double[] _grads;

        private int[] _rowIndexes;
        private int[] _iterRowIndexes;

        private Random _rng;

        public MiniBatchGradientDescentOptimiser()
        {
            _learn = 0.1;
            _limit = 1e-9;
            _batchSize = 100;
            _iter = 0;
            _maxIter = 1000;
            _converged = false;
            _diverged = false;
            _errors = new List<double>(_maxIter);
            _rng = new Random();
        }

        internal MiniBatchGradientDescentOptimiser(double[] parameters)
            :this()
        {
            _params = parameters;
        }

        public void SetRandom(Random rng)
        {
            _rng = rng;
        }

        public void SetLearningRate(double learningRate)
        {
            base.ValidateLearningRate(learningRate);

            _learn = learningRate;
        }

        public void SetMaxIterations(int maxIterations)
        {
            base.ValidateMaxIterations(maxIterations);

            _maxIter = maxIterations;
        }

        public void SetConvergenceLimit(double convergenceLimit)
        {
            base.ValidateConvergenceLimit(convergenceLimit);

            _limit = convergenceLimit;
        }

        public void SetBatchSize(int batchSize)
        {
            base.ValidateBatchSize(batchSize);

            _batchSize = batchSize;
        }

        public override void Train(Matrix<double> x, double[] y)
        {
            Initialise(x, y);

            while(_iter < _maxIter &&
                !_converged &&
                !_diverged)
                {
                    UpdateParameters();
                    UpdateError();
                    _iter++;
                }
        }

        public override double Predict(double[] x)
        {
            return x.DotProduct(_params);
        }

        public bool HasConverged
        {
            get { return _converged; }
        }

        public bool HasDiverged
        {
            get { return _diverged; }
        }

        public double[] GetCostProgression()
        {
            return _errors.Take(_iter)
                          .ToArray();
        }

        private void Initialise(Matrix<double> x, double[] y)
        {
            _length = x.Length;
            _width = x.Width;

            _x = x.Data;
            _y = y;

            if (_batchSize > _length)
            {
                _batchSize = _length;
            }

            _params = new double[_width];
            _diffs = new double[_batchSize];
            _grads = new double[_width];

            _rowIndexes = Enumerable.Range(0, _length)
                                    .ToArray();
        }

        private void UpdateParameters()
        {
            _iterRowIndexes = _rowIndexes.Shuffle<int>(_rng)
                                         .Take(_batchSize)
                                         .ToArray();

            for (int i = 0; i < _batchSize; i++)
            {
                var row = _iterRowIndexes[i];
                var product = _x[row].DotProduct(_params);
                _diffs[i] = product - _y[row];
            }

            Array.Clear(_grads, 0, _width);
            for (int i = 0; i < _batchSize; i++)
            {
                int row = _iterRowIndexes[i];
                for (int column = 0; column < _width; column++)
                {
                    _grads[column] += _diffs[i] * _x[row][column];
                }
            }

            for (int column = 0; column < _width; column++)
            {
                _params[column] -= _learn * (_grads[column] / _batchSize);
            }
        }

        private void UpdateError()
        {
            _errors.Add(_diffs.DotProduct(_diffs) / (2 * _batchSize));
            if (_iter > 0)
            {
                var diff = Math.Abs(_errors[_iter] - _errors[_iter - 1]);

                if (!diff.IsValidDouble())
                {
                    _diverged = true;
                    return;
                }

                if (diff <= _limit)
                {
                    _converged = true;
                }
            }
        }

        public override string Serialise()
        {
            var data = new
            {
                optimiserType = GetOptimiserType(),
                parameters = _params,
            };
            return JsonConvert.SerializeObject(data);
        }
    }
}
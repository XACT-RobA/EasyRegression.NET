using System;
using System.Collections.Generic;
using EasyRegression.Core.Common.Maths;

namespace EasyRegression.Core.Optimisation
{
    public class BatchGradientDescentOptimiser : BaseOptimiser
    {
        private int _length;
        private int _width;

        private double[][] _x;
        private double[] _y;

        private double _learn;
        private double[] _errors;
        private double _limit;
        private int _iter;
        private int _maxIter;
        private bool _converged;

        private double[] _params;
        private double[] _diffs;
        private double[] _grads;

        public BatchGradientDescentOptimiser(double learningRate = 0.1,
            int maxIterations = 1000,
            double convergenceLimit = 1e-9)
        {
            _iter = 0;
            _maxIter = maxIterations;
            _learn = learningRate;
            _errors = new double[maxIterations];
            _limit = convergenceLimit;
            _converged = false;
        }

        public override void Train(double[][] x, double[] y)
        {
            Initialise(x, y);

            while(_iter < _maxIter && !_converged)
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

        private void Initialise(double[][] x, double[] y)
        {
            _length = x.Length;
            _width = x[0].Length;

            _x = x;
            _y = y;

            _params = new double[_width];
            _diffs = new double[_length];
            _grads = new double[_width];
        }

        private void UpdateParameters()
        {
            for (int row = 0; row < _length; row++)
            {
                var product = _x[row].DotProduct(_params);
                _diffs[row] = product - _y[row];
            }

            Array.Clear(_grads, 0, _width);
            for (int row = 0; row < _length; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    _grads[column] += _diffs[row] * _x[row][column];
                }
            }

            for (int column = 0; column < _width; column++)
            {
                _params[column] -= _learn * (_grads[column] / _length);
            }
        }

        private void UpdateError()
        {
            _errors[_iter] = _diffs.DotProduct(_diffs) / (2 * _length);
            if (_iter > 0)
            {
                var diff = Math.Abs(_errors[_iter] - _errors[_iter - 1]);
                if (diff <= _limit) _converged = true;
            }
        }
    }
}
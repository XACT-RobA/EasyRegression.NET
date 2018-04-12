using System;
using System.Collections.Generic;
using EasyRegression.Core.Common.Models;
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
        private double _limit;
        private int _iter;
        private int _maxIter;
        private bool _converged;
        private readonly double[] _errors;
        
        private double[] _params;
        private double[] _diffs;
        private double[] _grads;

        public BatchGradientDescentOptimiser()
        {
            _iter = 0;
            _maxIter = 1000;
            _learn = 0.1;
            _limit = 1e-9;
            _converged = false;
            _errors = new double[_maxIter];
        }

        public void SetLearningRate(double learningRate)
        {
            if (learningRate.IsValidDouble() &&
                learningRate > 0) { _learn = learningRate; }
        }

        public void SetMaxIterations(int maxIterations)
        {
            if (maxIterations > 0) { _maxIter = maxIterations; }
        }

        public void SetConvergenceLimit(double convergenceLimit)
        {
            if (convergenceLimit.IsValidDouble() &&
                convergenceLimit >= 0.0) { _limit = convergenceLimit; }
        }

        public override void Train(Matrix<double> x, double[] y)
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

        private void Initialise(Matrix<double> x, double[] y)
        {
            _length = x.Length;
            _width = x.Width;

            _x = x.Data;
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
                if (diff <= _limit)
                {
                    _converged = true;
                }
            }
        }
    }
}

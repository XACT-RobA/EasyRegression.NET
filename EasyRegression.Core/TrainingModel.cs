using System;
using System.Collections.Generic;
using System.Linq;
using EasyRegression.Core.Common.Models;

// Included in main namespace so only one using statement required
namespace EasyRegression.Core
{
    public class TrainingModel<T>
    {
        private Matrix<T> _x;
        private double[] _y;

        public TrainingModel(T[][] x, double[] y)
            : this(new Matrix<T>(x), y) { }

        public TrainingModel(Matrix<T> x, double[] y)
        {
            if (x.Length != y.Length)
            {
                throw new ArgumentException("Training data length mismatch");
            }

            _x = x;
            _y = y;
        }

        public T[] this[int index]
        {
            get { return _x[index]; }
        }

        public Matrix<T> X
        {
            get { return _x; }
        }

        public double[] Y
        {
            get { return _y; }
        }

        public int Length
        {
            get { return _x.Length; }
        }

        public void UpdateX(Matrix<T> x)
        {
            if (x.Length != _y.Length)
            {
                throw new ArgumentException("Matrix dimension mismatch");
            }

            _x = x;
        }

        public void UpdateY(double[] y)
        {
            if (_x.Length != y.Length)
            {
                throw new ArgumentException("Training data length mismatch");
            }

            _y = y;
        }

        public void UpdateXAndY(Matrix<T> x, double[] y)
        {
            if (x.Length != y.Length)
            {
                throw new ArgumentException("Training data length mismatch");
            }

            _x = x;
            _y = y;
        }

        public void Filter(IEnumerable<int> indexes)
        {
            var indexSet = new HashSet<int>(Enumerable.Range(0, Length));
            indexSet.ExceptWith(indexes);

            var filteredX = new List<T[]>(indexSet.Count);
            var filteredY = new List<double>(indexSet.Count);

            foreach (var index in indexSet)
            {
                filteredX.Add(X[index]);
                filteredY.Add(Y[index]);
            }

            var matrix = new Matrix<T>(filteredX.ToArray());

            UpdateXAndY(matrix, filteredY.ToArray());
        }

        public static TrainingModel<T> Combine(params TrainingModel<T>[] models)
        {
            var totalLength = models.Select(m => m.Length).Sum();
            var xData = new List<T[]>(totalLength);
            var yData = new List<double>(totalLength);

            for (int i = 0; i < models.Length; i++)
            {
                for (int j = 0; j < models[i].Length; j++)
                {
                    xData.Add(models[i].X[j]);
                    yData.Add(models[i].Y[j]);
                }
            }

            return new TrainingModel<T>(xData.ToArray(), yData.ToArray());
        }
    }
}
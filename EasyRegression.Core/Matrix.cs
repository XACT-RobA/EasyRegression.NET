using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EasyRegression.Core
{
    public class Matrix<T>
    {
        public Matrix(T[][] data)
        {
            if (data.Length < 1)
            { 
                throw new ArgumentException("Matrix cannot have zero length");
            }

            if (data[0].Length < 1)
            {
                throw new ArgumentException("Matrix cannot have zero width");
            }
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i].Length != data[0].Length)
                {
                    throw new ArgumentException("Matrix is not rectangular");
                }
            }

            Data = data;
        }

        public T[] this[int index]
        {
            get { return Data[index]; }
        }

        public T[][] Data { get; }

        public int Length
        {
            get
            {
                return Data.Length;
            }
        }

        public int Width
        {
            get
            {
                return Data[0].Length;
            }
        }
    }
}

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
            Length = data.Length;
            if (Length < 1)
            { 
                throw new ArgumentException("Matrix cannot have zero length");
            }

            Width = data[0].Length;
            if (Width < 1)
            {
                throw new ArgumentException("Matrix cannot have zero width");
            }
            for (int i = 1; i < Length; i++)
            {
                if (data[i].Length != Width)
                {
                    throw new ArgumentException("Matrix is not rectangular");
                }
            }

            Data = data;
        }

        public T[] this[int index]
        {
            get { return Data[index]; }
            set { Data[index] = value; }
        }

        public T[][] Data { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }
    }
}

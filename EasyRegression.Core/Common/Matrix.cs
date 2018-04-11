using System;

namespace EasyRegression.Core.Common
{
    public struct Matrix<T>
    {
        public Matrix(T[][] data)
        {
            Length = data.Length;
            if (Length < 1)
            { 
                throw new Exception("Matrix has no length");
            }

            Width = data[0].Length;
            for (int i = 1; i < Length; i++)
            {
                if (data[i].Length != Width)
                {
                    throw new Exception("Matrix is not rectangular");
                }
            }

            Data = data;
        }

        public T[][] Data { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }
    }
}
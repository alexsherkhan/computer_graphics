using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_affine_transformations.Primitives
{
    class Transformation
    {
        private float[] matrix = new float[9];

        public Transformation() { }

        public Transformation(float a, float b, float c,
                              float d, float e, float f,
                              float g, float h, float i)
        {
            matrix = new float[9] { a, b, c, d, e, f, g, h, i };
        }

        public Transformation(float[] matrix)
        {
            if (9 != matrix.Count()) throw new Exception("Bad number of elements in matrix.");
            this.matrix = matrix;
        }

        public float Get(int row, int col)
        {
            return matrix[row * 3 + col];
        }

        public void Set(int row, int col, float value)
        {
            matrix[row * 3 + col] = value;
        }

        public static Transformation operator *(Transformation t1, Transformation t2)
        {
            Transformation result = new Transformation();
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 3; ++j)
                {
                    float value = 0;
                    for (int k = 0; k < 3; ++k)
                        value += t1.Get(i, k) * t2.Get(k, j);
                    result.Set(i, j, value);
                }
            return result;
        }

    }
}

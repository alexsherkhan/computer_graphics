using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;

namespace Lab6.Primitives
{

    public class Point3d
    {

        private double[] coords = new double[] { 0, 0, 0, 1 };

        public double X { get { return coords[0]; } set { coords[0] = value; } }
        public double Y { get { return coords[1]; } set { coords[1] = value; } }
        public double Z { get { return coords[2]; } set { coords[2] = value; } }
        public PointF TextureCoordinates { get; set; } = new PointF();
        public Point3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        //Load from file
        public Point3d(string s)
        {
            var arr = s.Split(' ');

            float x = float.Parse(arr[1], CultureInfo.InvariantCulture);
            float y = float.Parse(arr[2], CultureInfo.InvariantCulture);
            float z = float.Parse(arr[3], CultureInfo.InvariantCulture);
            X = x;
            Y = y;
            Z = z;

        }

        public Point3d(Point3d p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
        }

        public string to_string()
        {
            return X.ToString(CultureInfo.InvariantCulture) + " " +
                Y.ToString(CultureInfo.InvariantCulture) + " " +
                Z.ToString(CultureInfo.InvariantCulture);
        }

        public void Apply(Transformation t)
        {
            double[] newCoords = new double[4];
            for (int i = 0; i < 4; ++i)
            {
                newCoords[i] = 0;
                for (int j = 0; j < 4; ++j)
                    newCoords[i] += coords[j] * t.Matrix[j, i];
            }
            coords = newCoords;
        }

        public PointF toPointF(Projection pr)
        {
            switch (pr)
            {
                case Projection.ISOMETRIC:
                    return new PointF((float)X, (float)Y);
                case Projection.ORTHOGR_X:
                    return new PointF((float) Y, (float) Z);
                case Projection.ORTHOGR_Y:
                    return new PointF((float) X, (float) Z); 
                case Projection.ORTHOGR_Z:
                    return new PointF((float) X, (float) Y); 
                default:
                    return new PointF((float) (X / coords[3]), (float) (Y / coords[3]));
            }
        }

        static public List<double> mul_matrix(List<double> matr1, int m1, int n1, List<double> matr2, int m2, int n2)
        {
            if (n1 != m2)
                return new List<double>();
            int l = m1;
            int m = n1;
            int n = n2;

            List<double> c = new List<double>();
            for (int i = 0; i < l * n; ++i)
                c.Add(0f);

            for (int i = 0; i < l; ++i)
                for (int j = 0; j < n; ++j)
                {
                    for (int r = 0; r < m; ++r)
                        c[i * l + j] += matr1[i * m1 + r] * matr2[r * n2 + j];
                }
            return c;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab4_affine_transformations.Primitives
{
    interface Primitive
    {
        void Draw(Graphics g, bool selected);
        void Apply(Transformation t);
    }
}

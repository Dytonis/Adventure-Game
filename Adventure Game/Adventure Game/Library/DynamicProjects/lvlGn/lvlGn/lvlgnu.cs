using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lvlGn
{
    class lvlgnu
    {
        internal struct Vector
        {
            internal Vector(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            internal int x;
            internal int y;
        }

        internal static Random r = new Random();

        internal static int rand(int min, int max)
        {
            return r.Next(min, max + 1);
        }
    }   
}

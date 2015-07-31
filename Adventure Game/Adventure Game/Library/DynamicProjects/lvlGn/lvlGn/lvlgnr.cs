using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace lvlGn
{
    class lvlgnr
    {
        internal static int sizeSquared = 128;
        internal static int Range = 100;

        internal static int getRandomPin()
        {
            return lvlgnu.rand(0, sizeSquared);
        }
    }   
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lvlGn.lvlgnu;
using static lvlGn.lvlgnr;

namespace lvlGn
{
    internal class lvlgnds
    {
        private static int rands = 10;

        private static Dictionary<lvlgnu.Vector, int> map = new Dictionary<Vector, int>();

        internal static Dictionary<Vector, int> CreateMap()
        {
            for (int y = 0; y < sizeSquared; y++)
            {
                for (int x = 0; x < sizeSquared; x++)
                {
                    Vector v = new Vector(x, y);
                    map.Add(v, 0);
                }
            }

            DiamondSquare(new Vector(0, 0), new Vector(sizeSquared, sizeSquared), lvlgnr.Range, 64);

            for (int i = 0; i < 3; i++)
            {
                float rough = 0.3f;

                for (int y = 0; y < sizeSquared; y++)
                {
                    for (int x = 1; x < sizeSquared; x++)
                    {
                        Vector v = new Vector(x, y);
                        Vector v2 = new Vector(x - 1, y);

                        map[v] = (int)(map[v2] * (1 - rough) + map[v] * rough);
                    }
                }
                for (int y = 0; y < sizeSquared; y++)
                {
                    for (int x = sizeSquared - 2; x >= 0; x--)
                    {
                        Vector v = new Vector(x, y);
                        Vector v2 = new Vector(x + 1, y);

                        map[v] = (int)(map[v2] * (1 - rough) + map[v] * rough);
                    }
                }
                for (int x = 0; x < sizeSquared; x++)
                {
                    for (int y = 1; y < sizeSquared; y++)
                    {
                        Vector v = new Vector(x, y);
                        Vector v2 = new Vector(x, y - 1);

                        map[v] = (int)(map[v2] * (1 - rough) + map[v] * rough);
                    }
                }
                for (int x = 0; x < sizeSquared; x++)
                {
                    for (int y = sizeSquared - 2; y >= 0; y--)
                    {
                        Vector v = new Vector(x, y);
                        Vector v2 = new Vector(x, y + 1);

                        map[v] = (int)(map[v2] * (1 - rough) + map[v] * rough);
                    }
                }
            }

            return map;
        } 
        internal static void DiamondSquare(lvlgnu.Vector v1, lvlgnu.Vector v2, float range, int level)
        {
            if (level < 1) return;

            // diamonds
            for (int i = v1.x + level; i < v2.x; i += level)
                for (int j = v1.y + level; j < v2.y; j += level)
                {
                    Vector v = new Vector(i - level, j - level);
                    int a = map[v];
                    int b = map[new Vector(i, j-level)];
                    int c = map[new Vector(i-level, j)];
                    int d = map[new Vector(i, j)];
                    int e = map[new Vector(i - level / 2, j - level / 2)] = (int)((a + b + c + d) / 4 + rand(-rands, rands) * range);
                }

            // squares
            for (int i = v1.x + 2 * level; i < v2.x; i += level)
                for (int j = v1.y + 2 * level; j < v2.y; j += level)
                {
                    int a = map[new Vector(i - level, j - level)];
                    int b = map[new Vector(i, j - level)];
                    int c = map[new Vector(i - level, j)];
                    int d = map[new Vector(i, j)];
                    int e = map[new Vector(i - level/2, j - level/2)];
                    //i - 3 * level / 2][j - level / 2
                    float f = map[new Vector(i - level, j - level / 2)] = (int)((a + c + e + map[new Vector(i - 3 * level / 2, j - level / 2)]) / 4 + rand(-rands, rands) * range);
                    float g = map[new Vector(i - level / 2, j - level)] = (int)((a + b + e + map[new Vector(i - level / 2, j - 3 * level / 2)]) / 4 + rand(-rands, rands) * range);
                }

            DiamondSquare(v1, v2, range / 2, level / 2);
        }
    }
}

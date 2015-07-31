using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lvlGn.lvlgnu;
using static lvlGn.lvlgnr;

namespace lvlGn
{
    class lvlgnpd
    {

        private static Dictionary<Vector, int> map = new Dictionary<Vector, int>(); 
        internal static Dictionary<Vector, int> CreateMap()
        {

            //fill
            for (int y = 0; y < sizeSquared; y++)
            {
                for (int x = 0; x < sizeSquared; x++)
                {
                    Vector v = new Vector(x, y);
                    map.Add(v, 0);
                }
            }
            //sift

            Vector pin = new Vector(sizeSquared / 2, sizeSquared / 2);

            tick(pin, 5000);

            pin.x = getRandomPin();
            pin.y = getRandomPin();

            tick(pin, 5000);

            ///////////smoothing filter

            for (int i = 0; i < 10; i++)
            {
                float rough = 0.9f;

                for (int y = 0; y < sizeSquared; y++)
                {
                    for (int x = 1; x < sizeSquared; x++)
                    {
                        Vector v = new Vector(x, y);
                        Vector v2 = new Vector(x - 1, y);

                        map[v] = (int) (map[v2]*(1 - rough) + map[v]*rough);
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

        private static int lastHeight = 0;

        private static void tick(Vector pin, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                switch (rand(1, 6))
                {
                    case 1:
                        if (pin.x < sizeSquared)
                            pin.x+=1;
                        break;

                    case 2:
                        if (pin.y < sizeSquared)
                            pin.y+=1;
                        break;

                    case 3:
                        if (pin.x > 0)
                            pin.x-=1;
                        break;

                    case 4:
                        if (pin.y > 0)
                            pin.y-=1;
                        break;
                }

                for (int x = 0; x < 9; x++)
                {
                    for (int y = 0; y < 9; y++)
                    {
                        Vector v = new Vector(pin.x + x, pin.y + y);

                        if (map.ContainsKey(v))
                            map[v]+= rand(-2, 3);
                        else
                            map.Add(v, 1);
                    }
                }
                

                lastHeight = map[pin];
            }
        }
    }
}

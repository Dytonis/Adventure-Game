using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using static lvlGn.lvlgnu;
using static lvlGn.lvlgnpd;
using static lvlGn.lvlgnr;

namespace lvlGn
{
    public class lvlgn
    {
        public static Bitmap CreateHeightmap(int range) //dll entry
        {
            return CreateImage(lvlgnds.CreateMap(), sizeSquared);
        }

        private static Bitmap CreateImage(Dictionary<Vector, int> map, int sizeSquare)
        {
            Bitmap bitmap = new Bitmap(sizeSquare, sizeSquare, PixelFormat.Format48bppRgb);

            for (int x = 0; x < sizeSquare; x++)
            {
                for (int y = 0; y < sizeSquare; y++)
                {
                    Vector v = new Vector(x, y);
                    int c = 0;
                    if (map.ContainsKey(v))
                        c = map[v];

                    if (c < -70)
                        c = -70;

                    c += 70;

                    bitmap.SetPixel(x, y, Color.FromArgb(c));
                }
            }
            return bitmap;
        }
    }
}

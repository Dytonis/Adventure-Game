using System;
using UnityEngine;
using System.Drawing;
using System.Drawing.Imaging;
using lvlGn;

namespace Assets.Scripts.Level
{
    public class HeightmapToTerrain : MonoBehaviour
    {
        public Terrain t;

        public void Start()
        {
            lvlgn.Options(70, 128);
            Bitmap heightMap = lvlGn.lvlgn.CreateHeightmap();
            heightMap.Save("C:\\Users\\Velvet\\Desktop\\image.bmp", ImageFormat.Bmp);

            float[,] heights = new float[128,128];

            for (int y = 0; y < heightMap.Height; y++)
            {
                for (int x = 0; x < heightMap.Width; x++)
                {
                    System.Drawing.Color hc = heightMap.GetPixel(x, y);
                    float c = hc.ToArgb() + 16777146;

                    c /= 700;

                    Debug.Log(c);

                    heights[x, y] += c;                    
                }
            }

            t.terrainData.SetHeights(0, 0, heights);

            transform.GetComponent<TextureAssign>().Run();
        }
    }
}

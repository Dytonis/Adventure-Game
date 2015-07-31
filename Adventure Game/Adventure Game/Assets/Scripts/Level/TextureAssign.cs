using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class TextureAssign : MonoBehaviour {

        // Use this for initialization

        //private readonly int UNASSIGNED = 0;
        private readonly int SAND = 0;
        private readonly int GRASS = 1;
        private readonly int CLIFF = 2;

        public Terrain T;

        public void Run()
        {
            float[,,] alphaMap = new float[2048,2048,3];
            float[,] heights = new float[2048,2048]; //resample heightmap after unity applies its interprolation

            //resample heightmap
            for (int x = 0; x < 2048; x++)
            {
                for (int y = 0; y < 2048; y++)
                {
                    RaycastHit hit;
                    Physics.Raycast(new Ray(new Vector3(y*5, 100, x*5), Vector3.down), out hit);
                    heights[x, y] = 100 - hit.distance;
                }
            }

            //layer 1
            for (int x = 0; x < 2048; x++)
            {
                for (int y = 0; y < 2048; y++)
                {
                    if (heights[x, y] <= 16)
                        alphaMap[x, y, SAND] = 1;
                    if (heights[x, y] > 16 && heights[x, y] <= 19)
                    {
                        float grassperc = 1 - ((19 - heights[x, y])/3);
                        float sandperc = 1 - grassperc;

                        alphaMap[x, y, GRASS] = grassperc;
                        alphaMap[x, y, SAND] = sandperc;
                    }
                    if (heights[x, y] > 19)
                        alphaMap[x, y, GRASS] = 1;
                }
            }

            T.terrainData.SetAlphamaps(0, 0, alphaMap);
        }
    }
}

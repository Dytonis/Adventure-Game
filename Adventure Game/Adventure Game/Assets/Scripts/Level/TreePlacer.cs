using UnityEngine;

namespace Assets.Scripts.Level
{
    public class TreePlacer : MonoBehaviour
    {
        public Terrain T;

        // Use this for initialization
        public void Run(float[,] heights)
        {
            for (int x = 0; x < 2048; x++)
            {
                for (int y = 0; y < 2048; y++)
                {
                    TreeInstance instance = new TreeInstance();
                    instance.prototypeIndex = 0;
                }
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Globals
{
    public class Configuration : MonoBehaviour
    {
        public static class LevelGeneration
        {
            public static class TownSizeCategorization
            {
                public static object[,] Values = 
                {
                    { "Shack", 1, 1, 1.0f, 0 },
                    
                    { "Farm", 3, 9, 1.0f, 1 },
                    
                    { "Campground", 10, 15, 1.0f, 2 },

                    { "Powerplant", 20, 30, 0.5f, 3 },
                    { "Farms", 20, 30, 1.0f, 4 },

                    { "Village", 40, 70, 0.4f, 5 },
                    { "Millitary Town", 40, 70, 0.65f, 6 },
                    { "Airport", 40, 70, 0.90f, 7 },
                    { "Millitary Airstrip", 40, 70, 1f, 8 },
                    
                    { "Small Town", 70, 99, 1.0f, 9 },

                    { "Town", 100, 179, 1.0f, 10 },

                    { "Large Town", 180, 9999, 1.0f, 11 }
                };

                public static string RetrieveCategoryFromSize(int size)
                {
                    List<int> idMatches = new List<int>();
                    for (int i = 0; i < Values.GetLength(0); i++)
                    {
                        if (size >= (int) Values[i, 1] && size <= (int) Values[i, 2])
                        {
                            idMatches.Add((int) Values[i,4]);
                        }
                    }

                    Dictionary<int, int> results = new Dictionary<int, int>();
                    int pick = Random.Range(1, 100);
                    foreach (int i in idMatches)
                    {
                        int result = (int)(((float) Values[i, 3] * 100) - pick);
                        results.Add(i, result);
                    }

                    int lowVal = 100;
                    int lowKey = 0;
                    foreach (KeyValuePair<int, int> pair in results)
                    {
                        if (pair.Value < lowVal && pair.Value > 0)
                        {
                            lowVal = pair.Value;
                            lowKey = pair.Key;
                        }
                    }

                    return (string) Values[lowKey, 0];
                }
            }
        }
    }
}

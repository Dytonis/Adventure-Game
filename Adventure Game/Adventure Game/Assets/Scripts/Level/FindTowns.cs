using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Globals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Level
{
    public class FindTowns : MonoBehaviour
    {
        public GameObject TownBorderPrefab;

        private List<List<Vector3>> TownPointList = new List<List<Vector3>>(); 

        public void Run(List<List<Vector3>> vectorsLists)
        {
            List<List<Vector3>> UnkillableTownList = new List<List<Vector3>>();

            foreach (List<Vector3> vectorsList in vectorsLists)
            {
                //vectorsLists.RemoveAll(item => item.Count < 20);

                if (vectorsList.Count > 150) //must split
                {
                    foreach (List<Vector3> v2 in Split(vectorsList))
                    {
                        TownPointList.Add(v2);
                    }
                }
                else
                {
                    TownPointList.Add(vectorsList);
                    UnkillableTownList.Add(vectorsList);
                }
            }

            TownPointList.RemoveAll(item => !UnkillableTownList.Contains(item) && Random.Range(1, 3) == 1);

            foreach (List<Vector3> points in TownPointList)
            {
                Bounds b = new Bounds(points[0], Vector3.one);

                GameObject prefabP = Instantiate(TownBorderPrefab, b.center,
                    TownBorderPrefab.transform.rotation) as GameObject;

                if (prefabP != null)
                {
                    prefabP.name =
                        Configuration.LevelGeneration.TownSizeCategorization.RetrieveCategoryFromSize(points.Count);

                    foreach (Vector3 v in points)
                    {
                        GameObject prefab = Instantiate(TownBorderPrefab, v,
                            TownBorderPrefab.transform.rotation) as GameObject;

                        if (prefab != null) prefab.transform.parent = prefabP.transform;
                    }
                }
            }
        }

        public List<List<Vector3>> Split(List<Vector3> TownArea)
        {
            _vertexes = new List<Vector3>();
            _runner = new List<Vector3>();
            _points = new List<List<Vector3>>();

            int lowX = 10240;
            int lowZ = 10240;
            int maxX = 0;
            int maxZ = 0;
            foreach (Vector3 v in TownArea)
            {
                if (v.x < lowX)
                    lowX = (int)v.x;

                if (v.z < lowZ)
                    lowZ = (int) v.z;

                if (v.x > maxX)
                    maxX = (int) v.x;

                if (v.z > maxZ)
                    maxZ = (int) v.z;
            }

            for (int i = lowX; i < maxX; i += Random.Range(11, 19) * 80)
            {
                TownArea.RemoveAll(item => Math.Abs(item.x - i) < 1);
            }
            for (int i = lowZ; i < maxZ; i += Random.Range(11, 19) * 80)
            {
                TownArea.RemoveAll(item => Math.Abs(item.z - i) < 1);
            }

            foreach (Vector3 v in TownArea)
            {
                _vertexes.Add(v);
            }

            int Safety = 0;
            while (_vertexes.Count > 0)
            {
                Safety++;
                if (Safety > 100)
                    break;

                int x = (int)_vertexes[0].x;
                int z = (int)_vertexes[0].z;

                Seperate(x, z);

                List<Vector3> vList = new List<Vector3>();
                vList = _runner.ToList();

                _runner = new List<Vector3>();

                _points.Add(vList);
            }

            return _points;
        }

        List<List<Vector3>> _points = new List<List<Vector3>>();
        List<Vector3> _runner = new List<Vector3>();
        List<Vector3> _vertexes = new List<Vector3>();
        private void Seperate(int x, int z)
        {
            Vector3? vector3 = GetPoint(_vertexes, x, z);
            if (vector3 != null)
            {
                _vertexes.Remove((Vector3)vector3);
                _runner.Add(new Vector3(x, 1000, z));
            }
            else return;

            Seperate(x + 80, z);
            Seperate(x, z + 80);
            Seperate(x - 80, z);
            Seperate(x, z - 80);
        }

        private Vector3? GetPoint(List<Vector3> vectorList, int x, int z)
        {
            int index = 0;
            foreach (Vector3 v in vectorList)
            {
                if (Math.Abs(v.x - x) < 10 && Math.Abs(v.z - z) < 10)
                    return vectorList[index];

                index++;
            }

            return null;
        }
    }
}

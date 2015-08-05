using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Assets.Scripts.Globals;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class FindTownAreas : MonoBehaviour
    {
        public GameObject TownBorderPrefab;
        public int[] TownCountDefinition;
        public int[] TownAreaDifinition;
        public float NoiseFactor = 0.50f;

        public void Run()
        {
            FindTownableAreas();
            SeperateMeshes();

            transform.GetComponent<FindTowns>().Run(_points);
        }

        public void FindTownableAreas()
        {
            List<Vector3> vertexList = new List<Vector3>();

            for (int x = 0; x < 10240; x += 80)
            {
                for (int z = 0; z < 10248; z += 80)
                {
                    RaycastHit hit;
                    Physics.Raycast(new Vector3(x, 1000, z), Vector3.down, out hit);

                    float y = 1000 - hit.distance;

                    

                    if (y > 18 && y < 800)
                    {
                        if (Vector3.Angle(hit.normal, Vector3.up) < 10)
                        {
                            vertexList.Add(new Vector3(x, y, z));
                        }
                    }
                }
            }

            _vertexes = vertexList;
        }

        private void SeperateMeshes()
        {
            int Safety = 0;

            while (_vertexes.Count > 0)
            {
                Safety++;
                if (Safety > 100)
                    break;

                int x = (int) _vertexes[0].x;
                int z = (int) _vertexes[0].z;

                Seperate(x, z);

                List<Vector3> vList = new List<Vector3>();
                vList = _runner.ToList();

                _runner = new List<Vector3>();

                _points.Add(vList);
            }
        }

        List<List<Vector3>> _points = new List<List<Vector3>>(); 
        List<Vector3> _runner = new List<Vector3>();
        List<Vector3> _vertexes = new List<Vector3>(); 
        private void Seperate(int x, int z)
        {
            Vector3? vector3 = GetPoint(_vertexes, x, z);
            if (vector3 != null)
            {
                _vertexes.Remove((Vector3) vector3);
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

        private bool CheckPoint(List<Vector3> vectorList, int x, int z)
        {
            return vectorList.Any(v => Math.Abs(v.x - x) < 1 && Math.Abs(v.z - z) < 1);
        }

        public Vector3 AverageVector3(List<Vector3> v)
        {
            float ax = 0;
            float ay = 0;
            float az = 0;

            foreach (Vector3 vector in v)
            {
                ax += vector.x;
                ay += vector.y;
                az += vector.z;
            }

            return new Vector3(ax / v.Count, ay / v.Count, az / v.Count);
        }
    }
}

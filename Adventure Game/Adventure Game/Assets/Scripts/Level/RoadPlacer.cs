using System.Linq;
using System.Net.Sockets;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class RoadPlacer : MonoBehaviour
    {
        public GameObject Prefab;
        public GameObject node1;
        public GameObject node2;

        //plan:

        //1. get the two nodes
        //2. from the first node, put the first square mesh facing the second node
        //3. change the angle of the first node by a random ammount
        //4. create a new mesh in front of the last one, and rotate a random ammount (small) towards the second node
        //5. if the current mesh is facing the node, then rotate a random ammount away from the second node unless it is within a certain distance
        //6. attach the two meshes' vertexes
        //7. repeat from 4 until the newest mesh is within a certain distance from the second node

        private GameObject lastObject;

        public void Start()
        {
            Run();
        }

        private Mesh startMesh;

        public void Run()
        {
            Mesh m = new Mesh
            {
                vertices = new Vector3[]
                {
                    new Vector3(-10, 0.05f, 10),
                    new Vector3(10, 0.05f, 10),
                    new Vector3(-10, -0.05f, 10),
                    new Vector3(10, -0.05f, 10),
                    new Vector3(-10, 0.05f, -10),
                    new Vector3(10, 0.05f, -10),
                    new Vector3(-10, -0.05f, -10),
                    new Vector3(10, -0.05f, -10)
                },

                triangles = new int[]
                {
                    3,
                    2,
                    1,

                    2,
                    0,
                    1,

                    1,
                    7,
                    3,

                    4,
                    7,
                    5,

                    2,
                    4,
                    0,

                    1,
                    0,
                    4,

                    6,
                    2,
                    3,

                    4,
                    5,
                    1,

                    7,
                    6,
                    3,

                    1,
                    5,
                    7,

                    6,
                    7,
                    4,

                    6,
                    4,
                    2
                },

                uv = new Vector2[]
                {
                    new Vector2(1, 0),
                    new Vector2(1, 1),
                    new Vector2(1, 0),
                    new Vector2(1, 1),
                    new Vector2(0, 0),
                    new Vector2(1, 0),
                    new Vector2(0, 0),
                    new Vector2(1, 0)
                }
              
            };

            startMesh = m;

            m.triangles = m.triangles.Reverse().ToArray();
            m.RecalculateBounds();

            GameObject p = Instantiate(Prefab, node1.transform.position, node1.transform.rotation) as GameObject;

            p.GetComponent<MeshFilter>().mesh = m;
           
            lastObject = p;

            Continue();
        }

        private void Continue()
        {
            Vector3 EulerTrue = node1.transform.rotation.eulerAngles;

            bool TrackP = false;
            bool TrackN = false;
            bool Sensitive = false;

            for (int i = 0; i < 1000; i++)
            {
                GameObject New = Instantiate(Prefab, lastObject.transform.position, node1.transform.rotation) as GameObject;

                New.transform.LookAt(node2.transform);

                Vector3 localAngle = New.transform.rotation.eulerAngles;
                float angle =lastObject.transform.rotation.eulerAngles.y - localAngle.y;

                Vector3 EulerNewQ = lastObject.transform.rotation.eulerAngles;
                Quaternion newQ = lastObject.transform.rotation;

                

                if (Vector3.Distance(lastObject.transform.position, node2.transform.position) < 50)
                {
                    New.transform.LookAt(node2.transform);
                    newQ = New.transform.rotation;

                    Debug.DrawRay(lastObject.transform.position, localAngle, Color.red, 10000f);
                }
                else if (Vector3.Distance(lastObject.transform.position, node2.transform.position) < 100)
                {
                    if (angle < 0)
                        newQ = Quaternion.Euler(EulerNewQ.x, EulerNewQ.y += -angle / 4, EulerNewQ.z);
                    else if (angle > 0)
                        newQ = Quaternion.Euler(EulerNewQ.x, EulerNewQ.y += -angle / 4, EulerNewQ.z);

                    Debug.DrawRay(lastObject.transform.position, localAngle, Color.yellow, 10000f);
                }
                else if (Vector3.Distance(lastObject.transform.position, node2.transform.position) < 500)
                {
                    if(angle < -10)
                        newQ = Quaternion.Euler(EulerNewQ.x, EulerNewQ.y += Random.Range(5, 15), EulerNewQ.z);
                    else if (angle > 10)
                        newQ = Quaternion.Euler(EulerNewQ.x, EulerNewQ.y += Random.Range(-15, -5), EulerNewQ.z);
                    else newQ = Quaternion.Euler(EulerNewQ.x, EulerNewQ.y += Random.Range(-5, 5), EulerNewQ.z);

                    Debug.DrawRay(lastObject.transform.position, localAngle, Color.green, 10000f);
                }
                else if (Vector3.Distance(lastObject.transform.position, node2.transform.position) >= 500)
                {
                    if (angle < -30)
                        newQ = Quaternion.Euler(EulerNewQ.x, EulerNewQ.y += Random.Range(0, 5), EulerNewQ.z);
                    else if (angle > 30)
                        newQ = Quaternion.Euler(EulerNewQ.x, EulerNewQ.y += Random.Range(-5, 0), EulerNewQ.z);
                    else newQ = Quaternion.Euler(EulerNewQ.x, EulerNewQ.y += Random.Range(-10, 10), EulerNewQ.z);

                    Debug.DrawRay(lastObject.transform.position, localAngle, Color.blue, 10000f);
                }
                

                ////////////////////

                New.transform.rotation = newQ;
                New.transform.position += lastObject.transform.forward*10;
                New.GetComponent<MeshFilter>().mesh = startMesh;

                Mesh mesh = New.GetComponent<MeshFilter>().mesh;
                Mesh oldMesh = lastObject.GetComponent<MeshFilter>().mesh;
                Vector3 VertexWorldPosition0 = lastObject.transform.TransformPoint(oldMesh.vertices[0]);
                Vector3 AltVertexWorldPosition0 = New.transform.TransformPoint(mesh.vertices[0]);
                Vector3 VertexWorldPosition2 = lastObject.transform.TransformPoint(oldMesh.vertices[2]);
                Vector3 AltVertexWorldPosition2 = New.transform.TransformPoint(mesh.vertices[2]);
                Vector3 VertexWorldPosition1 = lastObject.transform.TransformPoint(oldMesh.vertices[1]);
                Vector3 AltVertexWorldPosition1 = New.transform.TransformPoint(mesh.vertices[1]);
                Vector3 VertexWorldPosition3 = lastObject.transform.TransformPoint(oldMesh.vertices[3]);
                Vector3 AltVertexWorldPosition3 = New.transform.TransformPoint(mesh.vertices[3]);
                Vector3 NewVertexWorldPosition2 = new Vector3();
                Vector3 NewVertexWorldPosition3 = new Vector3();
                Vector3 NewVertexWorldPosition0 = new Vector3();
                Vector3 NewVertexWorldPosition1 = new Vector3();

                RaycastHit HitInfo;
                Physics.Raycast(new Vector3(AltVertexWorldPosition3.x, AltVertexWorldPosition3.y + 1000, AltVertexWorldPosition3.z),
                    Vector3.down, out HitInfo);

                NewVertexWorldPosition3 = HitInfo.point;

                Physics.Raycast(new Vector3(AltVertexWorldPosition2.x, AltVertexWorldPosition2.y + 1000, AltVertexWorldPosition2.z),
                    Vector3.down, out HitInfo);

                NewVertexWorldPosition2 = HitInfo.point;

                Physics.Raycast(new Vector3(AltVertexWorldPosition0.x, AltVertexWorldPosition0.y + 1000, AltVertexWorldPosition0.z),
                    Vector3.down, out HitInfo);

                NewVertexWorldPosition0 = new Vector3(HitInfo.point.x, HitInfo.point.y + 0.05f, HitInfo.point.z);

                Physics.Raycast(new Vector3(AltVertexWorldPosition1.x, AltVertexWorldPosition1.y + 1000, AltVertexWorldPosition1.z),
                    Vector3.down, out HitInfo);

                NewVertexWorldPosition1 = new Vector3(HitInfo.point.x, HitInfo.point.y + 0.05f, HitInfo.point.z);

                Vector3[] newVerts = mesh.vertices;
                newVerts[4] = New.transform.InverseTransformPoint(VertexWorldPosition0);
                newVerts[6] = New.transform.InverseTransformPoint(VertexWorldPosition2);
                newVerts[5] = New.transform.InverseTransformPoint(VertexWorldPosition1);
                newVerts[7] = New.transform.InverseTransformPoint(VertexWorldPosition3);
                newVerts[3] = New.transform.InverseTransformPoint(NewVertexWorldPosition3);
                newVerts[2] = New.transform.InverseTransformPoint(NewVertexWorldPosition2);
                newVerts[1] = New.transform.InverseTransformPoint(NewVertexWorldPosition1);
                newVerts[0] = New.transform.InverseTransformPoint(NewVertexWorldPosition0);

                New.GetComponent<MeshFilter>().mesh.vertices = newVerts;

                lastObject = New;

                if (Vector3.Distance(lastObject.transform.position, node2.transform.position) <= 20) break;
            }

            //transform.GetComponent<TreePlacer>().Run();
        }
    }
}

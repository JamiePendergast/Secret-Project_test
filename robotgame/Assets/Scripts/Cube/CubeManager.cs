using System.Collections.Generic;
using UnityEngine;

namespace Cube
{
    public sealed class CubeManager : MonoBehaviour
    { 
        private List<CubeObject> cubeObjects = new List<CubeObject>();
        private CubeObject self;

        private void SetSelf(ref CubeObject obj)
        {
            self = obj;
        }

        private void Start()
        {
            CubeObject cubeObject = GetComponent<CubeObject>();
            SetSelf(ref cubeObject);
        }

        public CubeObject PlaceCube(CubeObject prefab, Vector3 position, Quaternion rotation)
        {
            CubeObject cubeObject = Instantiate<CubeObject>(prefab, position, rotation, transform) as CubeObject;
            cubeObject.SetRenderers();
            for (int connectionPoint = 0; connectionPoint < prefab.ConnectionPoints.Count; connectionPoint++)
                cubeObject.AttachmentPoints.Add(cubeObject.transform.TransformPoint(prefab.ConnectionPoints[connectionPoint]));
            cubeObjects.Add(cubeObject);

            for (int i = 0; i < cubeObject.AttachmentPoints.Count; i++)
            {
                Vector3 where = cubeObject.AttachmentPoints[i];
                CubeObject part2 = GetCube(transform.InverseTransformPoint(where));
                if ((Object)part2 != (Object)null)
                {
                    for (int j = 0; j < part2.AttachmentPoints.Count; j++)
                    {
                        Vector3 where2 = part2.AttachmentPoints[j];
                        CubeObject part3 = GetCube(transform.InverseTransformPoint(where2));
                        if ((Object)part3 != (Object)null && (Object)cubeObject == (Object)part3 && !cubeObject.ConnectedCubes.Contains(part2))
                        {
                            cubeObject.ConnectedCubes.Add(part2);
                        }
                    }
                }
            }
            foreach(CubeObject cube in cubeObject.ConnectedCubes)
            {
                cube.ConnectedCubes.Add(cubeObject);
            }
            return cubeObject;
        }   

        public CubeObject GetCube(Vector3 where)
        {
            foreach (CubeObject obj in cubeObjects)
                if (obj.transform.localPosition == where)
                    return obj;
            return null;
        }

        public void DestroyCube(Vector3 where)
        {
            CubeObject cube = GetCube(where);
            if (cube != null)
            {
                cubeObjects.Remove(cube);

                foreach (CubeObject connection in cube.ConnectedCubes)
                    connection.ConnectedCubes.Remove(connection);

                Destroy(cube.gameObject);
            }
        }

        public void Connect()
        {
            CubeObject current;
            Queue<CubeObject> cubeObjects = new Queue<CubeObject>();
            cubeObjects.Enqueue(self);

            while (cubeObjects.Count != 0)
            {
                current = cubeObjects.Dequeue();
                current.Connect();

                foreach(CubeObject other in current.ConnectedCubes)
                {
                    if(!other.IsConnected())
                    {
                        cubeObjects.Enqueue(other);
                    }
                }
            }
        }


        public CubeManager[] GetChildren()
        {
            return GetComponentsInChildren<CubeManager>();
        }
    }
}
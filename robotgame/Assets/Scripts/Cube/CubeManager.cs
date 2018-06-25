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

        public CubeObject PlaceCube(CubeObject prefab, Vector3 where, Quaternion rotation)
        {
            CubeObject cubeObject = Instantiate<CubeObject>(prefab, where, rotation, transform) as CubeObject;
            for (int connectionPoint = 0; connectionPoint < prefab.ConnectionPoints.Count - 1; connectionPoint++)
                cubeObject.AttachmentPoints.Add(cubeObject.transform.TransformPoint(prefab.ConnectionPoints[connectionPoint]));
            cubeObjects.Add(cubeObject);
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
                        other.Connect();
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
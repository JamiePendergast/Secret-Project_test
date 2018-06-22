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
            where = transform.InverseTransformPoint(where);
            for(int index = 0; index < cubeObjects.Count - 1; index++)
            {
                if (cubeObjects[index].transform.position == where)
                    return cubeObjects[index];
            }
            return null;
        }

        public void DestroyCube(Vector3 where)
        {
            CubeObject cube = GetCube(where);
            if (cube != null)
                Destroy(cube.gameObject);
        }
    }
}
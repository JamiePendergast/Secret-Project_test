using System.Collections.Generic;
using UnityEngine;

namespace Cube
{
    public sealed class CubeObject : MonoBehaviour
    {
        private List<CubeObject> connectedCubes = new List<CubeObject>();
        public List<Vector3> ConnectionPoints = new List<Vector3>();
        public List<Vector3> AttachmentPoints = new List<Vector3>();

        public MeshRenderer[] GetRenderers()
        {
            return GetComponentsInChildren<MeshRenderer>(true);
        }

        public MeshFilter[] GetMeshFilters()
        {
            return GetComponentsInChildren<MeshFilter>(true);
        }

        public List<Material> GetMaterials()
        {
            List<Material> materials = new List<Material>();
            var renderers = GetRenderers();

            for (int rendererIndex = 0; rendererIndex < renderers.Length - 1; rendererIndex++)
            {   
                for(int materialIndex = 0; materialIndex < renderers[rendererIndex].materials.Length; materialIndex++)
                {
                    materials.Add(renderers[rendererIndex].materials[materialIndex]);
                }
            }

            return materials;
        }
    }
}
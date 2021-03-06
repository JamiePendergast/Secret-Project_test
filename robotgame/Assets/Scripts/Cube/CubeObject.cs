﻿using System.Collections.Generic;
using UnityEngine;

namespace Cube
{
    public sealed class CubeObject : MonoBehaviour
    {
        public List<CubeObject> ConnectedCubes = new List<CubeObject>();
        public List<Vector3> ConnectionPoints = new List<Vector3>();
        public List<Vector3> AttachmentPoints = new List<Vector3>();
        private bool isConnected;

        private MeshRenderer[] renderers;

        public bool IsConnected()
        {
            return isConnected;
        }

        public void Connect()
        {
            isConnected = true;
        }

        public void Disconnect()
        {
            isConnected = false;
        }

        public MeshRenderer[] GetRenderers()
        {
            return GetComponents<MeshRenderer>();
        }

        public MeshFilter[] GetMeshFilters()
        {
            return GetComponents<MeshFilter>();
        }

        public List<Material> GetMaterials()
        {
            List<Material> materials = new List<Material>();
            var renderers = GetRenderers();

            for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {   
                for(int materialIndex = 0; materialIndex < renderers[rendererIndex].materials.Length; materialIndex++)
                {
                    materials.Add(renderers[rendererIndex].materials[materialIndex]);
                }
            }

            return materials;
        }

        public CubeManager GetCubeManager()
        {
            Transform current = transform;
            while(current)
            {
                if (current.GetComponent<CubeManager>())
                    return current.GetComponent<CubeManager>();
                current = current.parent;
            }
            return null;
        }

        public void SetMaterialsTo(Material material)
        {
            for (int r = 0; r < renderers.Length; r++)
            {
                for(int m = 0; m < renderers[r].materials.Length; m++)
                    renderers[r].materials[m].CopyPropertiesFromMaterial(material);
            }
        }

        public void SetRenderers()
        {
            renderers = GetComponents<MeshRenderer>();
        }
    }
}
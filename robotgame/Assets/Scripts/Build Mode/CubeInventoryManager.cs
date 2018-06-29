using Cube;
using System.Collections.Generic;
using UnityEngine;

public sealed class CubeInventoryManager : MonoBehaviour
{
    private Dictionary<string, CubeObject> pallete = new Dictionary<string, CubeObject>();

    public CubeObject GetCubeObject(string name)
    {
        return pallete[name];
    }

    private void LoadCube(string name) {
        pallete[name] = Resources.Load<CubeObject>("Cubes/CubeObject Prefabs/" + name);
    }

    private void LoadPallete()
    {
        LoadCube("Cube");  //Just the one for now
    }

    private void Start()
    {
        LoadPallete();
    }

}

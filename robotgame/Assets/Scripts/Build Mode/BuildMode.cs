using Control;
using Cube;
using UnityEngine;
namespace Build_Mode
{
    public class BuildMode : MonoBehaviour
    {
        public ControlSchemeContainer ControlScheme;
        public RaycastComponent Raycaster;
        public CubeObject SelectedCube;

        [Header("Debug Values")]
        [SerializeField]
        private bool debugMode;
        [SerializeField]
        private Transform debugHitpoint;
        [Range(0.0f,1.0f)]
        public float Margin = 0.5001f;


        public float GetMostDistance(Vector3 start, Vector3 end)
        {
            Vector3 vector = start - end;
            return Mathf.Max(Mathf.Abs(vector.x), Mathf.Max(Mathf.Abs(vector.y), Mathf.Abs(vector.z)));
        }

        public bool CanPlace(ref CubeObject cube)
        {
            Vector3 start = cube.transform.InverseTransformPoint(Raycaster.GetPoint());
            for (int i = 0; i < cube.ConnectionPoints.Count; i++)
            {
                Vector3 vector = cube.ConnectionPoints[i];
                float mostDistance = GetMostDistance(start, vector);
                if (mostDistance < Margin && mostDistance > 1f - Margin)
                {
                    return true;
                }
            }
            return false;
        }
        
        public Vector3 GetPlacementPosition(ref CubeObject cube)
        {
            Vector3 start = cube.transform.InverseTransformPoint(Raycaster.GetPoint());
            for (int i = 0; i < cube.ConnectionPoints.Count; i++)
            {
                Vector3 vector = cube.ConnectionPoints[i];
                float mostDistance = GetMostDistance(start, vector);
                if (mostDistance < Margin && mostDistance > 1f - Margin)
                {
                    return cube.transform.TransformPoint(vector);
                }
            }
            return Vector3.zero;
        }

        public bool CanDestroy(ref CubeObject cube)
        {
            return true;
        }

        private void Start()
        {
            CursorLockController.MouseLocked = true;
        }

        private void Update()
        {
            if(debugMode)
            {
                debugHitpoint.gameObject.SetActive(Raycaster.Test());
            }    
        }

        private void LateUpdate()
        {
            if(Raycaster.Test())
            {
                //Get cube
                var cube = Raycaster.GetTransform().GetComponent<CubeObject>();

                if(cube != null)
                {
                   
                    var cubeManager = cube.GetCubeManager();
                    if(cubeManager != null)
                    {
                        //check inputs
                        var scheme = ControlScheme.Get();

                        if(scheme.PlaceCube() && CanPlace(ref cube))
                        {
                            Vector3 placementPos = GetPlacementPosition(ref cube);
                            cubeManager.PlaceCube(SelectedCube, placementPos, Quaternion.identity);
                        }

                        else if(scheme.DestroyCube() && CanDestroy(ref cube))
                        {
                            cubeManager.DestroyCube(Raycaster.GetTransform().localPosition);
                        }
                    }
                }

                if(debugMode)
                {
                    debugHitpoint.transform.position = Raycaster.GetPoint();
                }
            }
        }  
    }
}
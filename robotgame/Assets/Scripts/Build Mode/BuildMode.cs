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
        public int RotationPhase = 0;
        private Transform rotationSlave;

        
        [Header("Debug Values")]
        [SerializeField]
        private bool debugMode;
        [SerializeField]
        private Transform debugHitpoint;
        [Range(0.0f,1.0f)]
        public float Margin = 0.5001f;

        public GameObject PlacementCursor;

        public void Rotation(float amount)
        {
            RotationPhase += Mathf.FloorToInt(amount);
            rotationSlave.Rotate(0.0f, 90.0f * RotationPhase, 0.0f);
            Debug.Log(rotationSlave.eulerAngles);
        }

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
            rotationSlave = new GameObject("Rotation Slave").transform;
            PlacementCursor = Instantiate(SelectedCube.gameObject);
            Destroy(PlacementCursor.GetComponent<Collider>());
        }

        private void Update()
        {
            if(debugMode)
            {
                debugHitpoint.gameObject.SetActive(Raycaster.Test());
            }

            PlacementCursor.gameObject.SetActive(Raycaster.Test());
        }

        private void LateUpdate()
        {
            
            if (Raycaster.Test())
            {
                //Get cube
                var cube = Raycaster.GetTransform().GetComponent<CubeObject>();

                if(cube != null)
                {  
                    var cubeManager = cube.GetCubeManager();
                    if(cubeManager != null)
                    {
                        rotationSlave.up = Raycaster.GetNormal();
                        PlacementCursor.transform.up = rotationSlave.up;
                        
                        //check inputs
                        var scheme = ControlScheme.Get();

                        if (CanPlace(ref cube))
                        {
                            Vector3 placementPos = GetPlacementPosition(ref cube);
                            PlacementCursor.transform.position = placementPos;
                            if ((Input.GetAxis("Mouse ScrollWheel") != 0))
                            {
                                Rotation((int)Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));
                                PlacementCursor.transform.rotation = rotationSlave.rotation;

                            }

                            

                            if (scheme.PlaceCube())
                            {
                                cubeManager.PlaceCube(SelectedCube, placementPos, rotationSlave.rotation);
                            }
                        }

                        if (scheme.DestroyCube() && CanDestroy(ref cube))
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
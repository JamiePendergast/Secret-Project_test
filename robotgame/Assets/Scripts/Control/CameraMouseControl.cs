using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{

    public class CameraMouseControl : MonoBehaviour
    {
        [Range(0.25f,2.0f)]
        public float Sensitivity = 0.5f;
        private float mouseX, mouseY;
        public bool CanLook;

        // Update is called once per frame
        void Update()
        {
            if(CanLook)
            {
                mouseX += Input.GetAxis("Mouse X");
                mouseY -= Input.GetAxis("Mouse Y");
                mouseY = Mathf.Clamp(mouseY, -90.0f, 90.0f);
                transform.rotation = Quaternion.Euler(mouseY, mouseX, 0.0f);
            }
        }
    }
}
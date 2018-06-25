using UnityEngine;

namespace Control
{
    public sealed class CursorLockController
    {
        public static bool MouseLocked {
            get {
                return mouseLocked;
            }
            set {
                mouseLocked = value;
                Cursor.visible = !mouseLocked;
                Cursor.lockState = mouseLocked ? CursorLockMode.Locked : CursorLockMode.None;
            }
        }

        private static bool mouseLocked;   
    }
}
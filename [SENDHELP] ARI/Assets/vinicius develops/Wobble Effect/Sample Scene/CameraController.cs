using UnityEngine;

namespace vnc.FX
{
    public class CameraController : MonoBehaviour
    {
        public WaterCamera waterCamera;
        public float minHeight = 20;

        private void Update()
        {
            float yAxis = transform.position.y;
            yAxis += Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
            yAxis -= Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
            yAxis = Mathf.Clamp(yAxis, 10, 40);
            transform.position = Vector3.up * yAxis;

            waterCamera.effectActive = transform.position.y < minHeight;
        }

    }
}


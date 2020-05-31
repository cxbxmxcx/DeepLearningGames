using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IL.DeepLearningGames
{
    [RequireComponent(typeof(Camera))]
    public class ZoomFloatCamera : MonoBehaviour
    {
        public float maxZoom = -50;
        public float minZoom = -650;
        public float panSpeed = 1.0f;
        public float zoomSpeed = 1.0f;

        private Vector3 movement;        
        private Camera cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
        }
        // Update is called once per frame
        void Update()
        {
            movement = Vector3.zero;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement.x -= panSpeed;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movement.x += panSpeed;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                movement.y += panSpeed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                movement.y -= panSpeed;
            }
            var scroll = Input.mouseScrollDelta.y;            
            movement.z += scroll * zoomSpeed;
            
            var vec = cam.transform.position + movement;
            vec.z = Mathf.Clamp(vec.z, minZoom, maxZoom);
            cam.transform.position = vec;

        }
    }
}

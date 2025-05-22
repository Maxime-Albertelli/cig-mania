using UnityEngine;

namespace CameraMovement
{
    /// <summary>
    /// Camera movement
    /// </summary>
    public class Movement : MonoBehaviour
    {
        [Header("Camera Sensitivity")]
        [SerializeField] private float moveSpeed = 1f;

        [SerializeField] protected float zoomSpeed = 0.0025f;
        [SerializeField] protected float minZoom = 1f;
        [SerializeField] private float maxZoom = 10f;

        private Vector3 _firstMousePos;
        private Vector3 _firstPos;
        private bool _isDragging;
        private Camera _camera;


        protected void Start()
        {
            _camera = Camera.main;
        }

        protected void Update()
        {
            if (Input.touchCount != 2) return;

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            //Zoom
            if (_camera.orthographic)
            {
                _camera.orthographicSize += deltaMagnitudeDiff * zoomSpeed;
                _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, minZoom, maxZoom);
            }
            else
            {
                _camera.fieldOfView += deltaMagnitudeDiff * zoomSpeed;
                _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, minZoom, maxZoom);
            }

            //Move
            Vector2 delta = touchZero.deltaPosition + touchOne.deltaPosition;
            transform.position += new Vector3(-delta.x, -delta.y, 0) * moveSpeed;
        }

    }
}
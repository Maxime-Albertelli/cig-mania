using UnityEngine;

namespace CameraMovement
{
    /// <summary>
    /// Restrain camera movement
    /// </summary>
    public class MovementBlocked : Movement
    {
        [Header("Movement properties")]
        [Tooltip("Maximum Y position")]
        [SerializeField] private float positionYMax = 5f;
        [Tooltip("Minimum Y position")]
        [SerializeField] private float positionYMin = -5f;
        [Tooltip("X limit")]
        [SerializeField] private float limitX = 10f;

        private Camera _cam;

        private new void Start()
        {
            base.Start();
            _cam = Camera.main;
        }
    
        private new void Update()
        {
            base.Update();
            Limit();
        }

        private void OnDrawGizmosSelected()
        {
            var topRight = new Vector2(limitX, positionYMax);
            var bottomRight = new Vector2(limitX, positionYMin);
            var bottomLeft = new Vector2(-limitX, positionYMin);
            var topLeft = new Vector2(-limitX, positionYMax);

            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
            Gizmos.DrawLine(topLeft, topRight);
        }

        private void Limit()
        {
            Vector2 zoom = new Vector2(_cam.orthographicSize * _cam.aspect, _cam.orthographicSize);
            Vector3 pos = transform.position;
            if (pos.y > positionYMax - zoom.y)
                pos.y = positionYMax - zoom.y;
            else if (pos.y < positionYMin + zoom.y) pos.y = positionYMin + zoom.y;

            if (pos.x > limitX - zoom.x)
                pos.x = limitX - zoom.x;
            else if (pos.x < -limitX + zoom.x) pos.x = -limitX + zoom.x;

            transform.position = pos;
        
            // Adjust the maxOrthographicSize for landscape mode
            var maxOrthographicSizeY = Mathf.Min(pos.y - positionYMin, positionYMax - pos.y);
            var maxOrthographicSizeX = Mathf.Min(pos.x + limitX, limitX - pos.x) * Screen.height / Screen.width;
            var maxOrthographicSize = Mathf.Min(maxOrthographicSizeX, maxOrthographicSizeY);

            _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize, minZoom, maxOrthographicSize);
        }
    }
}
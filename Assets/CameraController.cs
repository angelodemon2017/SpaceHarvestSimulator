using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private Transform _cornerMin;
    [SerializeField] private Transform _cornerMax;
    [SerializeField] private Transform _minScale;
    [SerializeField] private Transform _maxScale;
    [SerializeField] private Transform _camera;

    private float Xaxis;
    private float Zaxis;
    private float _scaleView = 0.5f;
    private float _lastScale;

    private void Update()
    {
        Xaxis = Input.GetAxis("Horizontal");
        Zaxis = -Input.GetAxis("Vertical");
        _scaleView -= Input.GetAxis("Mouse ScrollWheel");
    }

    private void FixedUpdate()
    {
        Moving();
        Scaling();
    }

    private void Moving()
    {
        Vector3 movement = new Vector3(Zaxis, 0f, Xaxis) * _speed * Time.fixedDeltaTime;
        Vector3 newPosition = transform.position + movement;

        newPosition.x = Mathf.Clamp(newPosition.x, _cornerMin.position.x, _cornerMax.position.x);
        newPosition.z = Mathf.Clamp(newPosition.z, _cornerMin.position.z, _cornerMax.position.z);

        transform.position = newPosition;
    }

    private void Scaling()
    {
        _scaleView = Mathf.Clamp(_scaleView, 0, 1);

        if (_lastScale != _scaleView)
        {
            _lastScale = _scaleView;
            _camera.position = Vector3.Lerp(_minScale.position, _maxScale.position, _lastScale);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Vector3 center = (_cornerMin.position + _cornerMax.position) / 2f + Vector3.up * 3 / 2f;
        Vector3 size = _cornerMax.position - _cornerMin.position + Vector3.up * 3;
        Gizmos.DrawWireCube(center, size);
    }
}
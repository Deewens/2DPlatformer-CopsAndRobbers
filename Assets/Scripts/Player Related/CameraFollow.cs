using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _target;

    public float smoothSpeed = 0.125f;

    public Vector3 _offset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = _target.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    void LoadCameraPosition()
    {
        GameData data = SaveSystem.LoadGameData();
        transform.position = new Vector3(data.positionCamera[0] , data.positionCamera[1], data.positionCamera[2]);
    }
}

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = .15f;
    public float offsetY = -2;

    Vector3 currentVelocity = Vector3.zero;
    Vector3 newPos;
    float storeMinY;

    void LateUpdate()
    {
        if (target != null)
        {
            storeMinY = Mathf.Min(target.position.y - offsetY, storeMinY);
            newPos = new Vector3(transform.position.x, storeMinY, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothSpeed);
        } else 
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }
}

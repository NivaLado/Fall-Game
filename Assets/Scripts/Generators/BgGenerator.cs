using UnityEngine;

public class BgGenerator : MonoBehaviour
{
    public Transform generationPoint;
    public Vector3 spawnPosition;
    public string poolTag;

    ObjectPooler objectPooler;
    Quaternion rotation = Quaternion.Euler(0, 0, 90);

    void Start()
    {
        transform.position = spawnPosition;
        objectPooler = ObjectPooler.Instance;
    }

    void FixedUpdate()
    {
        if (transform.position.y > generationPoint.position.y)
        {
            transform.position = new Vector3(-0.1f, transform.position.y - 12.5f, -1.1f);
            objectPooler.spawnFromPool(poolTag, transform.position, rotation);
        }
    }
}

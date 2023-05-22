using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public Transform generationPoint;

    public float leverWidth = 2.5f;
    public string poolTag;

    private float xRandom;
    private float x2Random;
    ObjectPooler objectPooler;

    public Vector3 spawnPosition;

     void Start()
    {
        transform.position = spawnPosition;
        objectPooler = ObjectPooler.Instance;
    }

    void FixedUpdate () {
        if (transform.position.y > generationPoint.position.y)
        {
            xRandom = Random.Range(-leverWidth, leverWidth);
            x2Random = Random.Range(-leverWidth, leverWidth);
            int temp = Random.Range(0, 100);


            transform.position = new Vector3(0 + xRandom, transform.position.y - 6, transform.position.z);

            objectPooler.spawnFromPool(poolTag, transform.position, Quaternion.identity);
            if(temp <= 25)
                objectPooler.spawnFromPool("Coin", new Vector3(0 + x2Random,transform.position.y + 0.5f, transform.position.z), Quaternion.Euler(0,0,45));
        }
	}
}

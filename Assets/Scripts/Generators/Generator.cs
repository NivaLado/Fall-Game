using UnityEngine;

public class Generator : MonoBehaviour , IPooledObj
{
    public void OnObjectSpawn()
    {
        float r = Random.Range(0f,1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        gameObject.GetComponent<Renderer>().material.color = new Color(r, g, b, 1);
    }
}

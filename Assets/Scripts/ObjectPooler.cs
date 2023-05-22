using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionaty;
    public Transform poolParent;

    #region Singleton
    public static ObjectPooler Instance;
    #endregion

    private void Awake()
    {
        Instance = this;

        poolDictionaty = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(poolParent, false);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionaty.Add(pool.tag, objectPool);
        }
    }



    public GameObject spawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (poolDictionaty.ContainsKey(tag))
        {
            GameObject objectToSpawn = poolDictionaty[tag].Dequeue();

            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            objectToSpawn.SetActive(true);

            IPooledObj pooledObj = objectToSpawn.GetComponentInChildren<IPooledObj>();
            if (pooledObj != null)
               pooledObj.OnObjectSpawn();

            poolDictionaty[tag].Enqueue(objectToSpawn);
            return objectToSpawn;
        }

        return null;
    }

    public GameObject spawnFromPoolText(string tag, Transform target, Transform parent, int score)
    {
        if (poolDictionaty.ContainsKey(tag))
        {
            GameObject objectToSpawn = poolDictionaty[tag].Dequeue();

            Vector2 screePos = Camera.main.WorldToScreenPoint(target.position);
            objectToSpawn.transform.SetParent(parent, false);
            objectToSpawn.transform.position = screePos;
            TextMeshProUGUI mText = objectToSpawn.GetComponentInChildren<TextMeshProUGUI>();
            mText.text = score.ToString();
            objectToSpawn.SetActive(true);

            poolDictionaty[tag].Enqueue(objectToSpawn);
            return objectToSpawn;
        }

        return null;
    }

}

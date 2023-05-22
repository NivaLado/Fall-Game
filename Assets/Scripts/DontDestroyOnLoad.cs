using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
	private static DontDestroyOnLoad instance;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Object.Destroy(gameObject);
        }
    }

    void Start()
    {
        CoinsManager.instance.Check(
			SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RGB111110Float),
			SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf)
		);
    }
}

using UnityEngine;
using TMPro;

public class SettingsInit : MonoBehaviour
{
	public TMP_Dropdown m_Dropdown;
	GameObject temp;

    enum Quality
    {
        High, Low
    }

    void Awake()
    {
		QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality", 0));
		
        if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RGB111110Float))
        {
			Camera.main.GetComponent<FastMobileBloom>().enabled = false; //Disable Bloom
            PlayerPrefs.SetInt("Quality", (int)Quality.Low);
            QualitySettings.SetQualityLevel((int)Quality.Low);
        }

		m_Dropdown.value = PlayerPrefs.GetInt("Quality", 0); // Set Dropdown Laber to Prefs Quality

		
    }
}

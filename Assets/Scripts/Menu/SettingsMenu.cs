using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour {

    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        /*Change volume decay exponental not linearly Slider must be from 0 to 100*/
        float vol = volume/10f;
		if(volume > 10)
			vol = Mathf.Log(vol,10f);
		else 
			vol = (vol/10f)/2.5f;
		
		vol = Remap(vol,0,1,-80,0);

        audioMixer.SetFloat("Volume", vol);
    }

    public void SetQuality(int qualityIndex)
    {
        PlayerPrefs.SetInt("Quality", qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
        if(qualityIndex == 0) // If High
            Camera.main.GetComponent<FastMobileBloom>().enabled = true;
        else
            Camera.main.GetComponent<FastMobileBloom>().enabled = false;
    }

    //Scale one value to another
    public float Remap (float v, float minOld, float maxOld, float minNew,  float maxNew)
    {
       float result = ( maxNew - minNew / maxOld - minOld ) * ( v - maxOld ) + maxNew;
       return result;
    }
}
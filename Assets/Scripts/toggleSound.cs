using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleSound : MonoBehaviour
{
    public Text soundButtonText;

    void Start()
    {
        //sound on at start
        soundButtonText.text = "on";
    }
    public void toggleAllSound()
    {
        //toggles audiolistener, public method turns off ALL sounds via settigs button on canvas

        if(AudioListener.volume == 1f)
        {
            AudioListener.volume = 0f;
            soundButtonText.text = "off";
        }
        else if(AudioListener.volume == 0f)
        {
            AudioListener.volume = 1f;
            soundButtonText.text = "on";
        }
        
    }
}

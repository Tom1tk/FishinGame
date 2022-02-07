using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialScript : MonoBehaviour
{
    public Text tutorialButtonText;
    public GameObject tutorialText1, tutorialText2, tutorialText3, tutorialText4, tutorialText5, tutorialText6;
    public bool playTutorial;

    public void startTutorial()
    {
        if(playTutorial == true)
        {
            Invoke("showText1", 2f);
            //starts tutorial invoke chain of waiting then displaying new tutorial text
        }
      
    }
    void showText1()
    {
        tutorialText1.SetActive(true);
        Invoke("showText2", 4f);
    }
    void showText2()
    {
        tutorialText2.SetActive(true);
        Invoke("showText3", 4f);
    }
    void showText3()
    {
        tutorialText3.SetActive(true);
        Invoke("showText4", 4f);
    }
    void showText4()
    {
        tutorialText4.SetActive(true);
        Invoke("showText5", 4f);
    }
    void showText5()
    {
        tutorialText5.SetActive(true);
        Invoke("showText6", 5f);
    }
    void showText6()
    {
        tutorialText6.SetActive(true);
    }

    public void endTutorial()
    {
        tutorialText1.SetActive(false);
        tutorialText2.SetActive(false);
        tutorialText3.SetActive(false);
        tutorialText4.SetActive(false);
        tutorialText5.SetActive(false);
        tutorialText6.SetActive(false);

        PlayerPrefs.SetInt("playTutorial", 1);
        PlayerPrefs.Save();

        //permanently saves tutorial played, only local changes to replay tutorial

        playTutorial = false;
    }


    public void repeatTutorial()
    {
        playTutorial = !playTutorial;
        //toggles local tutoral bool for replay
    }

    // Start is called before the first frame update
    void Start()
    {
        tutorialText1.SetActive(false);
        tutorialText2.SetActive(false);
        tutorialText3.SetActive(false);
        tutorialText4.SetActive(false);
        tutorialText5.SetActive(false);
        tutorialText6.SetActive(false);

        if(PlayerPrefs.GetInt("playTutorial") == 0)
        {
            playTutorial = true;
            //plays tutorial on first boot of game
        }
        else
        {
            playTutorial = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playTutorial == false)
        {
            //toggles button text to match if tutorial will be played
            tutorialButtonText.text = "off";
        }
        else
        {
            tutorialButtonText.text = "on";
        }
    }
}

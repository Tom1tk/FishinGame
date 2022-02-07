using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControl : MonoBehaviour
{
    public Animator MenuAnimator;
    public Animator HSAnimator;
    public GameObject BGMRef;
    public FishSpawn spawnerRef;
    public Text HSText;
    public Text LSText;
    public Text musicButtonText;
    public bool gamePlaying = false;
    public bool HTP = false;
    public bool HS = false;
    public bool paused = false;
    public GameObject pauseScreen;

    public void GameTransition()
    {
        //toggles game playing value
        gamePlaying = !gamePlaying;

        //toggles animator boolean for animation controller
        if(gamePlaying == true){
            MenuAnimator.SetBool("Playing", true);
        }else{
            MenuAnimator.SetBool("Playing", false);
        }
    }

    public void HTPTransition()
    {
        //used to be tutorial (How To Play, HTP) but is now settings screen
        //toggles settings bool
        HTP = !HTP;

        //toggles animator boolean for animation controller
        if(HTP == true){
            MenuAnimator.SetBool("HTP", true);
        }else{
            MenuAnimator.SetBool("HTP", false);
        }
    }

    public void HSTransition()
    {
        //same as above but for Highscores screen
        HS = !HS;

        if(HS == true){
            HSAnimator.SetBool("HSshow", true);
        }else{
            HSAnimator.SetBool("HSshow", false);
        }
    }

    public void pause()
    {
        if(spawnerRef.spawning == true)
        {
            //pauses the game and shows pause menu if game is running
            switch(paused)
            {
                case true:
                    pauseScreen.SetActive(false);
                    Time.timeScale = 1f;
                    paused = false;
                    break;
                case false:
                    pauseScreen.SetActive(true);
                    Time.timeScale = 0f;
                    paused = true;
                    break;

            }
        }
    }

    public void toggleMusic()
    {
        //UI button method for toggling the music, mutes the music audio source
        BGMRef.GetComponent<AudioSource>().mute = !BGMRef.GetComponent<AudioSource>().mute;
    }

    // Start is called before the first frame update
    void Start()
    {
        gamePlaying = false;
        paused = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //dynamically changes button text to reflect current setting
        if(BGMRef.GetComponent<AudioSource>().mute == true)
        {
            musicButtonText.text = "off";
        }
        else
        {
            musicButtonText.text = "on";
        }
        
        //reads and displays last score from PlayerPrefs
        LSText.text = "Score: \n" + PlayerPrefs.GetFloat("LatestScore1");

        ///reads and displays last scores and highscore from PlayerPrefs
        HSText.text = "Highscore: \n" 
        + PlayerPrefs.GetString("HighscoreDateTime") + " - " + PlayerPrefs.GetFloat("Highscore") + "\n" 
        + "\n Last five Scores: \n" 
        + PlayerPrefs.GetString("ScoreDateTime1") + " - " + PlayerPrefs.GetFloat("LatestScore1") + "\n"
        + PlayerPrefs.GetString("ScoreDateTime2") + " - " + PlayerPrefs.GetFloat("LatestScore2") + "\n"
        + PlayerPrefs.GetString("ScoreDateTime3") + " - " + PlayerPrefs.GetFloat("LatestScore3") + "\n"
        + PlayerPrefs.GetString("ScoreDateTime4") + " - " + PlayerPrefs.GetFloat("LatestScore4") + "\n"
        + PlayerPrefs.GetString("ScoreDateTime5") + " - " + PlayerPrefs.GetFloat("LatestScore5");
    }
}

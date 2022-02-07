using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishCollection : MonoBehaviour
{
    public GameObject hookRef;
    public GameObject endScreen;
    public GameObject pauseButton;
    public GameObject fishSpawner;
    public GameObject tutorialRef;
    public UIControl UICtrlRef;
    public Text gameTimerText;
    public float gameTimer;
    public float gameTimerFull;
    int min = 0;
    int sec = 0;
    float tempfl;
    string tempst;
    public Text counter;
    public float caughtFish = 0;
    public GameObject Basket;
    SpriteRenderer basketRenderer;
    [SerializeField]
    Sprite BasketEmpty, Basket1, Basket2, Basket3;
    
    public void startTimer()
    {
        //fills timer
        gameTimer = gameTimerFull;

        //plays tutorial if enabled
        if(tutorialRef.GetComponent<tutorialScript>().playTutorial == false)
        {
            //runs the timer method once every second
            InvokeRepeating("GameTimerRun", 0f, 1f);
        }
        
        caughtFish = 0;
        pauseButton.SetActive(true);
    }

    public void resetGameTimer()
    {
        //used to replay the game after gameover, cancels timer and resets values
        CancelInvoke("GameTimerRun");
        caughtFish = 0;
        gameTimer = gameTimerFull;
        pauseButton.SetActive(true);
    }

    void GameTimerRun()
    {
        if(gameTimer == 0)
        {
            //if timer is finished, cancels timer
            CancelInvoke("GameTimerRun");

            //stops spawning
            fishSpawner.GetComponent<FishSpawn>().stopSpawning();

            //disables and shows relevant UI
            endScreen.SetActive(true);
            pauseButton.SetActive(false);

            //see method, this one is messy
            shuffleLatestScores();

            if(caughtFish > PlayerPrefs.GetFloat("Highscore"))
            {
                //if current score is a highscore, overwrites old highscore
                PlayerPrefs.SetFloat("Highscore", caughtFish);
                PlayerPrefs.SetString("HighscoreDateTime", System.DateTime.Now.ToString("dd/MM, h:mmtt"));
            }

            //saves last score to the highscore menu
            PlayerPrefs.SetFloat("LatestScore1", caughtFish);
            PlayerPrefs.SetString("ScoreDateTime1", System.DateTime.Now.ToString("dd/MM, h:mmtt"));

            //writes teh PlayerPrefs variables to memory for persistence between sessions
            PlayerPrefs.Save();

        }
        else
        {
            //counts down timer once a second
            gameTimer--;
        }
    }

    void shuffleLatestScores()
    {
        //ugly code that pushes down each saved score in the latest scores seciton
        //I wanted to do this properly but was worried PlayerPrefs would misbehave with variables in variable names since it only takes string

        //takes the 4th score and date and swaps it onto the 5th slot
        tempfl = PlayerPrefs.GetFloat("LatestScore4");
        PlayerPrefs.SetFloat("LatestScore5", tempfl);
        tempst = PlayerPrefs.GetString("ScoreDateTime4");
        PlayerPrefs.SetString("ScoreDateTime5", tempst);

        //moves 3rd to 4th
        tempfl = PlayerPrefs.GetFloat("LatestScore3");
        PlayerPrefs.SetFloat("LatestScore4", tempfl);
        tempst = PlayerPrefs.GetString("ScoreDateTime3");
        PlayerPrefs.SetString("ScoreDateTime4", tempst);

        //etc
        tempfl = PlayerPrefs.GetFloat("LatestScore2");
        PlayerPrefs.SetFloat("LatestScore3", tempfl);
        tempst = PlayerPrefs.GetString("ScoreDateTime2");
        PlayerPrefs.SetString("ScoreDateTime3", tempst);

        //leaving 1st and 2nd the same so 1st can be written over by the latest data
        tempfl = PlayerPrefs.GetFloat("LatestScore1");
        PlayerPrefs.SetFloat("LatestScore2", tempfl);
        tempst = PlayerPrefs.GetString("ScoreDateTime1");
        PlayerPrefs.SetString("ScoreDateTime2", tempst);
        
        //saves all the above data to the device for persistence between sessions
        PlayerPrefs.Save();
    }

    void Start()
    {
        gameTimerFull = 90f;
        gameTimer = gameTimerFull;
        caughtFish = 0;
        counter.text = "x " + caughtFish;
        basketRenderer = Basket.GetComponent<SpriteRenderer>();
        basketRenderer.sprite = BasketEmpty;
        endScreen.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //updates basket counter
        counter.text = "x " + caughtFish;

        if(UICtrlRef.gamePlaying == true)
        {
            //if game is playing, shows and updates timer with formatting for minutes and seconds
            gameTimerText.gameObject.SetActive(true);
            min = Mathf.FloorToInt(gameTimer / 60);
            sec = Mathf.FloorToInt(gameTimer % 60);
            gameTimerText.text = min.ToString("0") + ":" + sec.ToString("00");;
        }
        else
        {
            //disables timer
            gameTimerText.gameObject.SetActive(false);
        }

        //changes basket sprite depending on how many fish are caught so far
        switch (caughtFish)
            {
                case 0:
                    basketRenderer.sprite = BasketEmpty;
                    return;
                case float n when (n >= 1 && n < 5):
                    basketRenderer.sprite = Basket1;
                    return;
                case float n when (n >= 5 && n < 10):
                    basketRenderer.sprite = Basket2;
                    return;
                case float n when (n >= 10):
                    basketRenderer.sprite = Basket3;
                    return;
                default:
                    return;
            }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("Fish"))
        {
            //Increments collected fish variable and destroys collected fish
            hookRef.GetComponent<Movement>().hookedFish = 0f;

            caughtFish++;
            
            Destroy(col.gameObject);
        }
    }
}

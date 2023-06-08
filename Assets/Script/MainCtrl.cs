using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCtrl : MonoBehaviour
{
    public float afterDmgTime;
    public Image[] hearts;      // change in unity
    public Text winningTxt;     // change in unity
    public Text losingTxt;      // change in unity
    public Button respawnBtn;   // change in unity 

    private float TimeLeft;
    public bool TimerOn;
    public Text TimerTxt;       // change in unity

    public Text counter;        // change in unity

    public GameObject pauseMenu;// change in unity
    public bool pausedNow;

    void Start()
    {
        // player related
        Globals.playerHP = 3;
        Globals.playerStatus = "normal";
        Globals.powerUp = false;
        Globals.cd = false;
        Globals.canCtrl = true;
        Globals.canJmp = true;
        afterDmgTime = 3.0f;
        // collectable related
        Globals.required = 15;
        Globals.collected = 0;
        counter.text = string.Format("0/{0}", Globals.required);
        // game state related
        Globals.paused = false;
        Globals.gameState = "running";
        // menu related
        respawnBtn.onClick.AddListener(ReloadScene);
        winningTxt.enabled = false;
        losingTxt.enabled = false;
        respawnBtn.gameObject.SetActive(false);
        pausedNow = false;
        pauseMenu.SetActive(pausedNow);
        // timer related
        Time.timeScale = 1;
        TimeLeft = 75;
        TimerOn = true;
    }
    
    void Update()
    {
        // game ended
        // FIX: Globals.paused should be renamed as Globals.ended
        if (Globals.paused || TimeLeft <= 0)
        {
            Time.timeScale = 0;
            // display gameover/winning text
            if (Globals.gameState == "dead" || TimeLeft <= 0) { losingTxt.enabled = true; }
            else if (Globals.gameState == "win") { winningTxt.enabled = true; }
            // display respawn button
            respawnBtn.gameObject.SetActive(true);
            return;
        }
        // actual pause state
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausedNow = !pausedNow;
            pauseMenu.SetActive(pausedNow);
            Time.timeScale = (Time.timeScale + 1) % 2;
        }
        // player just received damage
        if (Globals.playerStatus == "damaged") { StartCoroutine(AfterDmgProcess()); }
        // player died
        if (Globals.playerHP == 0)
        {
            StopAllCoroutines();
            Globals.gameState = "dead";
            Globals.paused = true;
        }
        // timer update
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                TimerOn = false;
            }
        }
        // collectable counter update
        counter.text = string.Format("{0}/{1}", Globals.collected, Globals.required);
    }

    private IEnumerator AfterDmgProcess()
    {
        Globals.playerStatus = "invincible";
        Globals.playerHP -= 1; // 3 >> 2
        hearts[Globals.playerHP].enabled = false; // hearts[2], which is the 3rd heart
        // action needed for player to realize after-damage protection
        yield return new WaitForSeconds(afterDmgTime);
        Globals.playerStatus = "normal";
    }

    private void ReloadScene() { SceneManager.LoadScene(0); }

    private void updateTimer(float currTime)
    {
        currTime += 1;
        float min = Mathf.FloorToInt(currTime / 60);
        float sec = Mathf.FloorToInt(currTime % 60);
        TimerTxt.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}

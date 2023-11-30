using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private PlayerActions playerActions;

    // Pause variables
    public GameObject pauseMenu;
    public bool isPaused = false;

    // Timer variables
    public TMP_Text timer;
    public float currentTime = 0;

    // These are here for now, can implement trophies
    public float bronzeTime;
    public float silverTime;
    public float goldTime;

    bool bronzeFail = false, silverFail = false, goldFail = false;

    // Interact text
    public TMP_Text interactText;

    // Hearth icons
    public List<Image> hearthIcons;

    private void Awake()
    {
        playerActions = new PlayerActions();
        playerActions.Default.Pause.performed += ctx => Pause();
        playerActions.Default.Restart.performed += ctx => Restart();

    }
    private void OnEnable()
    {
        playerActions.Default.Enable();
    }

    private void OnDisable()
    {
        playerActions.Default.Disable();
    }

    void Start()
    {
        timer.color = new Color(0.97f, 0.71f, 0.19f);
    }

    void Update()
    {
        /*
        // ESC to pause
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();

        // R to restart
        if (Input.GetKeyDown(KeyCode.R))
            
        */
        // Timer!!!
        if (!isPaused)
            Timer();
    }

    // Timer function. Change color and bool if timer goal was missed
    void Timer()
    {
        currentTime += Time.deltaTime;
        timer.SetText(string.Format("{0:00}:{1:00}", Mathf.FloorToInt(currentTime / 60), Mathf.FloorToInt(currentTime % 60)));

        // Gold time failed
        if (currentTime > goldTime && !goldFail)
        {
            timer.color = new Color(0.85f, 0.85f, 0.85f);
            goldFail = true;
        }
            
        // Silver time failed
        else if (currentTime > silverTime && !silverFail)
        {
            timer.color = new Color(0.79f, 0.39f, 0.12f);
            silverFail = true;
        }
    }

    // Pause function. Set pause status with isPaused
    void Pause()
    {
        if (!isPaused)
        {
            isPaused = !isPaused;
            Time.timeScale = 0f;
        } else
        {
            isPaused = !isPaused;
            Time.timeScale = 1f;
        }
        //isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
    }

    void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void Continue()
    {
        Debug.Log("Continued");
        Pause();
    }

    public void Settings()
    {
        
    }

    public void InteractTextVisibility(bool visible)
    {
        interactText.gameObject.SetActive(visible);
    }

    public void SetHealthIcons(int amount)
    {
        //Disable every hearth icon
        for (int i = 0; i < hearthIcons.Count; i++)
        {
            hearthIcons[i].gameObject.SetActive(false);
        }

        //Enable amount of hearth icons corresponding to players health
        for (int i = 0; i <= amount -1; i++)
        {
            if (i < hearthIcons.Count)
                hearthIcons[i].gameObject.SetActive(true);
        }
    }


    public void QuitLevel()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
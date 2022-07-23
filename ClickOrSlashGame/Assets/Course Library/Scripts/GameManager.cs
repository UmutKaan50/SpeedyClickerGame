using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    public GameObject titleScreen;

    public Button restartButton;

    public Slider volumeSlider;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;

    private AudioSource backgroundMusic;

    private float spawnRate = 1;

    private int score;
    private int lives;

    private bool isPaused = false;
    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic =  GetComponent<AudioSource>();
        backgroundMusic.Play();

        
    }





    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            backgroundMusic.Play();
        }
        else
        {
            backgroundMusic.Pause();
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives()
    {
        lives--;
        livesText.text = "Lives: " + lives;
        if (lives < 1)
        {
            GameOver();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(float difficulty)
    {
        titleScreen.gameObject.SetActive(false);   
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        spawnRate /= difficulty;

        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
        lives = 3;
        livesText.text = "Lives: " + lives;
    }
}

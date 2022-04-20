using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Slider volumeSlider;

    private AudioManager audioManager;

    private int scores;
    private float spawnRate = 1.5f;
    private int lives;

    public bool isGameActive;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive && Input.GetKeyDown(KeyCode.P)) PauseGame();

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

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        scores = 0;
        lives = 3;
        spawnRate /= difficulty;

        UpdateLives();
        UpdateScores(scores);
        StartCoroutine(SpawnTarget());

        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        titleScreen.SetActive(false);
    }

    public void UpdateScores(int scoreToAdd)
    {
        scores += scoreToAdd;
        scoreText.text = "Score: " + scores;
    }

    void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
    }

    public void DecreaseLives()
    {
        lives -= 1;
        UpdateLives();

        if (lives < 1) GameOver();
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void PauseGame()
    {
        isPaused = !isPaused;
        pauseScreen.SetActive(isPaused);
        audioManager.Pause();

        if ( isPaused )
        {
            Time.timeScale = 0f;
        
        } else
        {
            Time.timeScale = 1f;
            
        }
    }
}
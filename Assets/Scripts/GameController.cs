using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] asteroids;
    [SerializeField] GameObject star;
    [SerializeField] GameObject gameoverPanel;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI totalScore;

    [SerializeField] AudioSource _aus;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip collectSound;
    [SerializeField] AudioClip gameoverSound;
    [SerializeField] AudioClip shootSound;

    [SerializeField] Animator transition;

    CountTime countTime;

    public int count = 2;
    public int maxCount = 4;
    public float score = 50;
    public float spawnTimeAsteroids = 3f;
    public bool isGameOver = false;
    private bool canSpawnAsteroid = true;
    private bool canSpawnStar = true;
    private bool timeOver = false;

    private void Awake()
    {
        gameoverPanel.SetActive(false);
        scoreText.enabled = true;
        countTime = FindObjectOfType<CountTime>();
    }
    
    void Update()
    {
        if(countTime.time < 50)
        {
            maxCount = 5;
            if (countTime.time < 30) spawnTimeAsteroids = 2.5f;
            if(countTime.time < 0) timeOver = true;
        }
        scoreText.text = "Score: " + score;
        if (isGameOver)
        {
            
            gameoverPanel.SetActive(true);
            totalScore.text = scoreText.text;

        }
        if (canSpawnStar && !isGameOver && countTime.time>2)
        {
            StartCoroutine(WaitToSpawn(1));
        }
        
        if(count <=maxCount && canSpawnAsteroid && !isGameOver)
        {
            StartCoroutine(WaitToSpawn(2));
            
        }
    }
    
    private IEnumerator WaitToSpawn(int type)
    {
        if (type == 1)
        {
            canSpawnStar = false;
            yield return new WaitForSeconds(0.2f);
            Vector2 starPosition = new Vector2(Random.Range(-8.4f, 8.4f), 5f);
            Instantiate(star, starPosition, Quaternion.identity);
            canSpawnStar=true;
        }
        if (type == 2 && !timeOver)
        {
            count++;
            canSpawnAsteroid = false;
            yield return new WaitForSeconds(spawnTimeAsteroids);
            int index = Random.Range(0, asteroids.Length);
            GameObject asteroid = asteroids[index];
            Vector2 spawnPos = new Vector2(Random.Range(-7.5f, 7.5f), 3.4f);
            Instantiate(asteroid, spawnPos, Quaternion.identity);
            canSpawnAsteroid = true;
        }
     
    }
    IEnumerator GameOver()
    {
        _aus.Stop();
        yield return new WaitForSeconds(0.1f);
        _aus.PlayOneShot(gameoverSound);
    }
    public void PlaySound(int type)
    {
        if(_aus && explosionSound && type == 1)
        {
            _aus.PlayOneShot(explosionSound);
        }
        if(_aus && collectSound && type == 2)
        {
            _aus.PlayOneShot(collectSound);
        }
        if(_aus && gameoverSound && type == 3)
        {
            StartCoroutine(GameOver());
        }
        if (_aus && shootSound && type == 4)
        {
            _aus.PlayOneShot(shootSound);
        }
    }
    public void Replay() {
        
        StartCoroutine(LoadGame("SampleScene"));
    }
    public void BackToMenu()
    {
        
        StartCoroutine(LoadGame("MenuScene"));
    }

    IEnumerator LoadGame(string scene)
    {
        scoreText.enabled = false;
        countTime.timeTxt.enabled = false;
        isGameOver = false;
        gameoverPanel.SetActive(false);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
}
    


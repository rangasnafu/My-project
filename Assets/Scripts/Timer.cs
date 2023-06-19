using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 10.4f;

    public GameObject gameOverUI;

    public string newGameScene;

    [SerializeField] TMP_Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime < 0)
        {
            Debug.Log("GameOver!");
            Time.timeScale = 0f;
            gameOverUI.SetActive(true);

        }

        if (gameOverUI.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene("Level1");
        }
        //if (gameOverUI.activeSelf && Input.GetKeyDown(KeyCode.R))
        //{
        //Time.timeScale = 1f;
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
        // }

        // if (gameOverUI.activeSelf && Input.GetKeyDown(KeyCode.Space))
        // {
        //Time.timeScale = 1f;
        //SceneManager.LoadScene(newGameScene);
        // }

    }


}

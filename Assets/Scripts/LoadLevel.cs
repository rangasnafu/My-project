using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    //public string levelName;
    //public float loadLevelDelay = 3.0f;
    //public GameObject playerObject;
    //public GameObject openDoorUI;

    //public AudioClip audioClip;
    //private AudioSource audioSource;

    // private bool doorOpened = false;

    //void Start()
    //{
    //    audioSource = GetComponent<AudioSource>();
    //}

    //void Update()
    //{
    //    if (openDoorUI.activeSelf && Input.GetKeyDown(KeyCode.E))
    //    {
    //        Invoke("LoadLevelDelayed", loadLevelDelay);
    //        doorOpened = true;
    //
    //        MusicManager musicManager = GameObject.FindObjectOfType<MusicManager>();
    //        musicManager.OpenDoorSound();
    //
    //        if (!doorOpened)
    //        {
    //            Time.timeScale = 1f;
    //           Scene scene = SceneManager.GetActiveScene();
    //           SceneManager.LoadScene(scene.name);
    //        }
    //    }
    // }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Time.timeScale = 0f;
    //        openDoorUI.SetActive(true);
    //    }
    //}

    public string levelName;
    public float loadLevelDelay = 3.0f;
    public GameObject openDoorUI;

    public AudioClip audioClip;
    private AudioSource audioSource;

    //private bool doorOpened = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    private void Update()
    {
        if (openDoorUI.activeSelf && Input.GetKeyDown(KeyCode.E)) //&& !doorOpened)
        {
            //StartCoroutine(PlaySoundAndLoadLevel());
            MusicManager musicManager = GameObject.FindObjectOfType<MusicManager>();
            musicManager.OpenDoorSound();

            Time.timeScale = 1f;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            openDoorUI.SetActive(true);
        }
    }

    //private System.Collections.IEnumerator PlaySoundAndLoadLevel()
    //{
    //    doorOpened = true;
//
    //    audioSource.Play();
    //
    //    yield return new WaitForSeconds(audioClip.length + loadLevelDelay);
    //
    //    Time.timeScale = 1f;
    //    SceneManager.LoadScene(levelName);
    //}
}

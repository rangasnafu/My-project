using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public AudioClip playerCaughtSound;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MusicManager musicManager = GameObject.FindObjectOfType<MusicManager>();

            musicManager.PlayerCaughtSound();
        }
    }
}

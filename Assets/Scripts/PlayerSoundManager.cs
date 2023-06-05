using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip caughtSound;

    public void PlayerWalkSound()
    {
        //audioSource.clip = walkSound;
        //audioSource.Play();
        Debug.Log("Walking sound played");
        audioSource.PlayOneShot(walkSound);

    }

    public void PlayerRunSound()
    {
        audioSource.clip = runSound;
        audioSource.Play();
    }

    public void PlayerCaughtSound()
    {
        audioSource.clip = caughtSound;
        audioSource.Play();
    }
}

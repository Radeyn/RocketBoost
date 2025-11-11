using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip crashSound;
    
    AudioSource audioSource;

    private bool isControlable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if(!isControlable) {return;}
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        isControlable = false;
        audioSource.Stop();
        // todo add sfx and particles
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", levelLoadDelay);
        
        audioSource.PlayOneShot(crashSound);
        
    }
    
    private void StartSuccessSequence()
    {
        isControlable = false;
        audioSource.Stop();
        // todo add sfx and particles
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
        
        audioSource.PlayOneShot(successSound);
        
    }

    void ReloadScene()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene(currentScene);
    }
    
    void LoadNextLevel()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        
        SceneManager.LoadScene(nextScene);
    }
}

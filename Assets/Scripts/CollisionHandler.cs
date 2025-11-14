using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f;
    [SerializeField] private AudioClip successSFX;
    [SerializeField] private AudioClip crashSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    
    AudioSource audioSource;
    

    private bool isControlable = true;
    private bool isCollidable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespontToDebugKeys();
    }
    
    void RespontToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
            
            Debug.Log("c key was pressed");
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if(!isControlable || !isCollidable) {return;}
        
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
        
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", levelLoadDelay);
    }
    
    void StartSuccessSequence()
    {
        isControlable = false;
        audioSource.Stop();
        
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
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

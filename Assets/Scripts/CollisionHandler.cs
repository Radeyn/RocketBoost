using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f;
    
    private void OnCollisionEnter(Collision other)
    {
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
        
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", levelLoadDelay);
    }
    
    private void StartSuccessSequence()
    {
        
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

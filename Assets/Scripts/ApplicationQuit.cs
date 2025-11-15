using UnityEngine;
using UnityEngine.InputSystem;

public class ApplicationQuit : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
            Debug.Log("Application quit");
        }
        
    }
}

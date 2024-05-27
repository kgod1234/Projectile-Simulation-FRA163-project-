using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_app : MonoBehaviour
{
    // This method will be called when the button is clicked
    public void QuitGame()
    {
        // Log a message for testing purposes
        Debug.Log("Quit button clicked!");

        // Quit the application
        Application.Quit();

        // If running in the Unity Editor, stop playing the scene
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

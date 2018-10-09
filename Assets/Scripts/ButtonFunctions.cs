using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour {

    // Restart fall
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Quit fall
    public void QuitGame()
    {
        // Athugar ef leikurinn er að runna í editornum eða annar staðar 
        // og lokar leiknum á mismunandi vegur eftir því
        #if UNITY_EDITOR
                 UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
                 Application.OpenURL("about:blank");
        #else
                 Application.Quit();
        #endif
    }
}

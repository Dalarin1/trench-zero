using UnityEngine;

public class DoorExit : MonoBehaviour
{
    void OnMouseDown()
    {
        QuitGame();
    }

    void QuitGame()
    {
        Debug.Log("Game Closed");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
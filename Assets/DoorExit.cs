using UnityEngine;

public class DoorExit : MonoBehaviour, IInteractable
{
    public void OnClick()
    {
        Debug.Log("Exit Door clicked");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OnHover()
    {
        throw new System.NotImplementedException();
    }

    public void OnUnover()
    {
        throw new System.NotImplementedException();
    }
}
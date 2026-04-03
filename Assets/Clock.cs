using UnityEngine;

public class Clock : MonoBehaviour, IInteractable
{
    public void OnClick()
    {
        Debug.Log("clock LICKED");
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
using UnityEngine;

public class Rulonen : MonoBehaviour, IInteractable
{
    public void OnClick()
    {
        Debug.Log("rulonen LICKED");
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
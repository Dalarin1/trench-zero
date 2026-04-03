using UnityEngine;
public class Radio : MonoBehaviour, IInteractable
{
    public void OnClick()
    {
        Debug.Log("RADIO LICKED"); 
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
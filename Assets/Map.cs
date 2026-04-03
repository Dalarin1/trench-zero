using UnityEngine;

public class Map : MonoBehaviour, IInteractable
{
    public void OnClick()
    {
        Debug.Log("MAP LICKED");
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
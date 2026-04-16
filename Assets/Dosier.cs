using UnityEngine;

public class DosierController : MonoBehaviour, IInteractable
{
    public void OnClick()
    {
        Debug.Log("dosier LICKED");
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
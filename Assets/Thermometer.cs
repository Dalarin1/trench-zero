using UnityEngine;

public class Thermometer : MonoBehaviour, IInteractable
{
    public void OnClick()
    {
        Debug.Log("Thermometer LICKED");
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
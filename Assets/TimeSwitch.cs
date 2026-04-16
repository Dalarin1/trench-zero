using UnityEngine;

public class TimeSwitch : MonoBehaviour, IInteractable
{
    public GameController gameController;

    public void Awake()
    {
        gameController = new GameController();
    }
    public void OnClick()
    {
        gameController.SwitchTime();
        Debug.Log("Time switched;  NOw: " + (gameController.time == GameController.GameTime.Day ? "Day" : "NIGHT"));
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
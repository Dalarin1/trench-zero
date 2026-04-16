
using UnityEngine;

public class SoldierStateMachine : MonoBehaviour
{
    public SoldierState CurrentState { get; private set; }
    private Soldier soldier;

    void Start()
    {
        soldier = GetComponent<Soldier>();
        CurrentState = soldier.state;
    }

    public void ChangeState(SoldierState newState)
    {
        OnStateExit(CurrentState);
        CurrentState = newState;
        soldier.state = newState;
        OnStateEnter(newState);
    }

    void OnStateEnter(SoldierState state)
    {
        switch (state)
        {
            case SoldierState.Idle:
                // Фишка стоит вертикально
                UpdateChipPosition(Vector3.zero);
                break;

            case SoldierState.Defending:
                // Фишка наклонена вперед
                UpdateChipPosition(new Vector3(15, 0, 0));
                break;

            case SoldierState.Wounded:
                // Фишка наклонена 45°
                UpdateChipPosition(new Vector3(45, 0, 0));
                // Добавить слой на фото
                UpdatePortrait("Wounded");
                break;

            case SoldierState.Dead:
                // Фишка лежит
                UpdateChipPosition(new Vector3(90, 0, 0));
                // Перевернуть карточку
                FlipCard();
                break;
        }
    }

    void OnStateExit(SoldierState state)
    {
        // Очистка состояния
    }

    void UpdateChipPosition(Vector3 rotation)
    {
        // Обновление 3D-фишки на карте
        Transform chip = MapManager.Instance.GetChipForSoldier(soldier);
        chip.DORotate(rotation, 0.5f);
    }

    void UpdatePortrait(string layer)
    {
        PortraitLayerSystem.Instance.AddLayer(soldier, layer);
    }

    void FlipCard()
    {
        SoldierCard card = CardManager.Instance.GetCardForSoldier(soldier);
        card.Flip();
    }
}

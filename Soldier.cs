using UnityEngine;

public class Soldier : MonoBehaviour
{
    public SoldierType type;
    public SoldierState state;
    public SoldierTrait trait;
    public string rank;

    public SoldierStateMachine stateMachine;

    void Awake()
    {
        stateMachine = GetComponent<SoldierStateMachine>();
    }

    public void TakeDamage(int amount)
    {
        // Логика получения урона
        if (state == SoldierState.Idle || state == SoldierState.Defending)
        {
            stateMachine.ChangeState(SoldierState.Wounded);
        }
        else if (state == SoldierState.Wounded)
        {
            stateMachine.ChangeState(SoldierState.Dead);
        }
    }

    public void Heal()
    {
        if (state == SoldierState.Wounded)
        {
            stateMachine.ChangeState(SoldierState.Idle);
        }
    }
}

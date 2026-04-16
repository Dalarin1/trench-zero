using UnityEngine;

public enum SoldierType
{
    Basic,
    Medic,
    Harmonist,
    Cook,
    Spy,
    Mutant
}

public enum SoldierState
{
    Idle,
    Defending,
    Wounded,
    Dead
}

public class SoldierTrait
{
    public string Description;
    public int MoraleBonus;
    
}

public class Soldier
{
    public SoldierType type;
    public SoldierState state;
    public SoldierTrait trait;
    public bool has_gas_mask = false;
    public string rank;

    public void TakeDamage()
    {
        // Логика получения урона
        if (state == SoldierState.Idle || state == SoldierState.Defending)
        {
            state = SoldierState.Wounded;
        }
        else if (state == SoldierState.Wounded)
        {
            state = SoldierState.Dead;
        }
    }
    public void Heal()
    {
        if (state == SoldierState.Wounded)
        {
            state = SoldierState.Idle;
        }
    }
    public void TryMutate()
    {
        if (!has_gas_mask)
        {
            type = SoldierType.Mutant;
        }
    }
}

public class SoldierDosier
{
    public Soldier soldier;
    public string Name;
    public string Description;

    //public Photo photo
}

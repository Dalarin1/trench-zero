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

public class OLDSoldier
{
    public SoldierType type;
    public SoldierState state;
    public SoldierTrait trait;
    public string rank;
}

public class SoldierDosier
{
    public Soldier soldier;
    public string Name;
    public string Description;

    //public Photo photo;
}
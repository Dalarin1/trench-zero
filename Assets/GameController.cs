using System;
using System.Collections.Generic;
using System.Linq;




public class OLDGameController
{
    private int _morale = 0;
    public int Morale
    {
        get { return _morale; }
        set
        {
            _morale = Math.Clamp(value, 0, 100);
        }
    }
    public int FoodAmount = 0;
    public int BulletsAmount = 0;
    public int MedicineAmount = 0;
    public int GasMaskAmount = 0;
    public enum GameTime { Day, Night };
    public GameTime time = GameTime.Day;

    public List<Soldier> Soldaten;
    public List<SoldierDosier> soldierDosiers;

    public void SwitchTime()
    {
        if (time == GameTime.Day)
        {
            time = GameTime.Night;
        }
        else
        {
            time = GameTime.Day;
        }
    }

    public bool HarmonistAlive() => Soldaten.Exists(x => x.type == SoldierType.Harmonist);

    public bool SabotageIsPossible() => Soldaten.Exists(x => x.type == SoldierType.Spy) || Soldaten.Exists(x => x.type == SoldierType.Mutant);

    public void ShootSoldier(int index)
    {

        BulletsAmount = Math.Max(BulletsAmount - 1, 0);
        int moraleDiff = -15;
        if (Soldaten[index].type == SoldierType.Mutant || Soldaten[index].type == SoldierType.Spy)
        {
            moraleDiff = 5;
        }
        Morale += moraleDiff;
        Soldaten.RemoveAt(index);
    }

    // отправка солдат
    public void SendSoldiers() { }

}
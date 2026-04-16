using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GameController
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
    public int Tokens = 0;
    public enum GameTime { Morning, Day, Night };
    public GameTime time = GameTime.Day;


    public Dictionary<int, Vector2> soldiers_on_map = new();

    public List<Vector2> enemies_on_map = new();
    public List<Vector2> fog_on_map = new();

    public List<Soldier> Soldaten = new();
    public List<SoldierDosier> soldierDosiers = new();

    public void SwitchTime()
    {
        switch (time)
        {
            case GameTime.Morning:
                StartDay();
                break;
            case GameTime.Day:
                StartNight();
                break;
            case GameTime.Night:
                StartMorning();
                break;
            default:
                break;
        }
    }

    private void StartNight()
    {
        time = GameTime.Night;

        int neededFood = Soldaten.Count;
        if (FoodAmount >= neededFood)
        {
            FoodAmount -= neededFood;
        }
        else
        {
            int deficit = neededFood - FoodAmount;
            FoodAmount = 0;
            Morale -= deficit * 5;
        }

        // выдача токенов
        Tokens = CalculateTokens();
    }

    private void StartMorning()
    {
        time = GameTime.Morning;

        ResolveCombat();
    }

    private void StartDay()
    {
        time = GameTime.Day;

        ResolveHealing();
        RemoveDead();

        Tokens = CalculateTokens();
    }
    

    private int CalculateTokens()
    {
        int baseTokens = 2 + Soldaten.Count / 2;
        int moraleBonus = Morale / 25; // 0–4

        return Math.Clamp(baseTokens + moraleBonus, 1, 10);
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
        soldiers_on_map.Remove(index);
        Soldaten.RemoveAt(index);
    }

    public bool CanMoveOnMap()
    {
        return time == GameTime.Day || Tokens > 0;
    }

    public bool MoveSoldierTo(int index, Vector2 newPos)
    {
        if (!CanMoveOnMap()) return false;
        if (!soldiers_on_map.ContainsKey(index)) return false;

        soldiers_on_map[index] = newPos;

        if (time == GameTime.Night)
            Tokens = Math.Max(0, Tokens - 1);

        return true;
    }

    public void RemoveDead()
    {
        var toRemove = new List<int>();

        foreach (var kvp in soldiers_on_map)
        {
            int id = kvp.Key;
            if (Soldaten[id].state == SoldierState.Dead)
            {
                toRemove.Add(id);
            }
        }

        foreach (var id in toRemove)
        {
            soldiers_on_map.Remove(id);
        }
    }

    public bool ShootAt(int soldier_id, Vector2 where)
    {
        if (BulletsAmount <= 0 || (time == GameTime.Night && Tokens <= 0)) return false;
        if (!soldiers_on_map.ContainsKey(soldier_id)) return false;

        BulletsAmount--;
        Tokens = Math.Max(0, Tokens - 1);
        if (enemies_on_map.Contains(where)) { 
            enemies_on_map.Remove(where); 
            Morale += 3; 
        }
        return true;
    }

    public void ResolveHealing()
    {
        int morale_diff = 0;
        foreach (var soldier in Soldaten)
        {
            if(soldier.state == SoldierState.Wounded && MedicineAmount > 0)
            {
                soldier.state = SoldierState.Idle;
                MedicineAmount--;
            }
            else if(soldier.state == SoldierState.Wounded && MedicineAmount <= 0)
            {
                morale_diff -= 3;
            }
        }
        Morale += morale_diff;
        return;
    }

    public void ResolveCombat()
    {
        // TODO
    }
}
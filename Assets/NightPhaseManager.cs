using System.Collections.Generic;
using UnityEngine;

public class NightPhaseController : MonoBehaviour
{
    [Header("Night Attack Settings")]
    public int MinSoldiersToSend = 2;
    public int MaxSoldiersToSend = 5;
    public List<Soldier> SelectedSoldiers = new List<Soldier>();

    [Header("Command Tokens")]
    public int CurrentTokens = 5;
    private int maxTokens = 5;

    void OnEnable()
    {
        PhaseManager.Instance.OnPhaseStart.AddListener(OnPhaseStarted);
    }

    void OnPhaseStarted(GamePhase phase)
    {
        if (phase == GamePhase.Night)
        {
            InitializeNightPhase();
        }
    }

    void InitializeNightPhase()
    {
        // Сброс токенов
        CurrentTokens = maxTokens;
        SelectedSoldiers.Clear();

        // UI запроса на выбор бойцов
        UIManager.Instance.ShowSoldierSelectionPanel(MinSoldiersToSend, MaxSoldiersToSend);
    }

    public void SelectSoldier(Soldier soldier)
    {
        if (SelectedSoldiers.Count >= MaxSoldiersToSend) return;

        SelectedSoldiers.Add(soldier);
        soldier.stateMachine.ChangeState(SoldierState.Defending);
    }

    public void ConfirmSelection()
    {
        if (SelectedSoldiers.Count < MinSoldiersToSend)
        {
            Debug.LogWarning("Недостаточно бойцов!");
            return;
        }

        StartCoroutine(ExecuteAttack());
    }

    IEnumerator ExecuteAttack()
    {
        // Запуск волн атаки
        for (int wave = 0; wave < 3; wave++)
        {
            yield return new WaitForSeconds(2f);
            SpawnEnemyWave(wave);
            yield return new WaitUntil(() => IsWaveCleared(wave));
        }

        // Завершение ночи
        CalculateCasualties();
        PhaseManager.Instance.TransitionToMorning();
    }

    void SpawnEnemyWave(int waveIndex)
    {
        // Генерация врагов
        WaveManager.Instance.SpawnWave(waveIndex);
    }

    bool IsWaveCleared(int wave)
    {
        return WaveManager.Instance.GetWaveEnemiesCount(wave) == 0;
    }

    void CalculateCasualties()
    {
        foreach (Soldier soldier in SelectedSoldiers)
        {
            // Рандомный урон
            float chance = Random.value;
            if (chance < 0.3f) // 30% шанс ранения
            {
                soldier.TakeDamage(1);
            }
        }
    }

    // Использование Command Token
    public bool UseToken()
    {
        if (CurrentTokens > 0)
        {
            CurrentTokens--;
            return true;
        }
        return false;
    }
}
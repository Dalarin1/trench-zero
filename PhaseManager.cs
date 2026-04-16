using System.Resources;
using UnityEngine.Events;
using UnityEngine;
using System.Collections;

public enum GamePhase
{
    Day,
    Night,
    Morning
}

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager Instance { get; private set; }

    [Header("Phase State")]
    public GamePhase CurrentPhase = GamePhase.Day;
    private float phaseTimer = 0f;

    [Header("Events")]
    public UnityEvent<GamePhase> OnPhaseStart;
    public UnityEvent<GamePhase> OnPhaseEnd;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void TransitionToNight()
    {
        if (CurrentPhase != GamePhase.Day) return;

        OnPhaseEnd?.Invoke(CurrentPhase);
        CurrentPhase = GamePhase.Night;
        OnPhaseStart?.Invoke(CurrentPhase);

        // Визуальные эффекты
        StartCoroutine(DimLightRoutine());

        // Автоматическое списание еды
        ResourceManager.Instance.AutoSpendFood();
    }

    public void TransitionToMorning()
    {
        if (CurrentPhase != GamePhase.Night) return;

        OnPhaseEnd?.Invoke(CurrentPhase);
        CurrentPhase = GamePhase.Morning;
        OnPhaseStart?.Invoke(CurrentPhase);

        // Обработка раненых
        SquadManager.Instance.ProcessWounded();
    }

    public void TransitionToDay()
    {
        if (CurrentPhase != GamePhase.Morning) return;

        OnPhaseEnd?.Invoke(CurrentPhase);
        CurrentPhase = GamePhase.Day;
        OnPhaseStart?.Invoke(CurrentPhase);

        // Инкремент дня
        GameController.Instance.DayCounter++;

        // Сохранение
        SaveSystem.Instance.SaveGame();
    }

    private IEnumerator DimLightRoutine()
    {
        Light mainLamp = LightingManager.Instance.MainLamp;
        float startIntensity = mainLamp.intensity;
        float targetIntensity = 0.3f;
        float duration = 1.5f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            mainLamp.intensity = Mathf.Lerp(startIntensity, targetIntensity, t / duration);
            yield return null;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ResourceType
{
    Food,
    Bullets,
    Medicine,
    GasMasks
}

[System.Serializable]
public class Resource
{
    public ResourceType Type;
    public int BackendValue; // Точное значение
    public GameObject VisualObject; // 3D рулон
    public AudioClip[] AudioStates; // Звуки для разных порогов

    public VisualState GetVisualState()
    {
        float percentage = BackendValue / (float)GetMaxValue();

        if (percentage > 0.5f) return VisualState.Full;
        if (percentage > 0.15f) return VisualState.Medium;
        if (percentage > 0.1f) return VisualState.Low;
        return VisualState.Empty;
    }

    public int GetMaxValue()
    {
        switch (Type)
        {
            case ResourceType.Food: return 40;
            case ResourceType.Bullets: return 50;
            case ResourceType.Medicine: return 10;
            case ResourceType.GasMasks: return 5;
            default: return 100;
        }
    }
}

public enum VisualState
{
    Full,    // >50%
    Medium,  // 15-50%
    Low,     // ≤10%
    Empty    // 0%
}

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    [Header("Resources")]
    public List<Resource> Resources = new List<Resource>();

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Получение ресурса по типу
    public Resource GetResource(ResourceType type)
    {
        return Resources.Find(r => r.Type == type);
    }

    // Тратить ресурс
    public bool SpendResource(ResourceType type, int amount)
    {
        Resource res = GetResource(type);
        if (res.BackendValue < amount) return false;

        res.BackendValue -= amount;
        UpdateVisual(res);
        PlayAudioFeedback(res);

        return true;
    }

    // Добавить ресурс
    public void AddResource(ResourceType type, int amount)
    {
        Resource res = GetResource(type);
        res.BackendValue = Mathf.Min(res.BackendValue + amount, res.GetMaxValue());
        UpdateVisual(res);
    }

    // Автоматическое списание еды при переходе Day→Night
    public void AutoSpendFood()
    {
        int activeSoldiers = SquadManager.Instance.GetActiveSoldiersCount();
        SpendResource(ResourceType.Food, activeSoldiers);
    }

    // Обновление визуального состояния рулона
    void UpdateVisual(Resource res)
    {
        ResourceRollController controller = res.VisualObject.GetComponent<ResourceRollController>();
        if (controller != null)
        {
            controller.UpdateVisual(res.BackendValue, res.GetVisualState());
        }
    }

    void PlayAudioFeedback(Resource res)
    {
        VisualState state = res.GetVisualState();
        AudioClip clip = res.AudioStates[(int)state];
        AudioManager.Instance.PlayOneShot(clip);
    }
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Directive
{
    public string ID;
    public string Title;
    [TextArea(3, 10)]
    public string Message;
    public bool IsCompleted;
    public int DayToTrigger;
}

public class RadioController : MonoBehaviour
{
    public static RadioController Instance;

    [Header("Radio UI")]
    public GameObject RadioPrefab;
    public Transform RadioSpawnPoint;
    private GameObject currentRadio;

    [Header("Directives")]
    public List<Directive> Directives = new List<Directive>();
    private Directive currentDirective;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void BroadcastDirective(int day)
    {
        Directive directive = Directives.Find(d => d.DayToTrigger == day);
        if (directive == null) return;

        currentDirective = directive;
        ShowRadioUI(directive);
    }

    void ShowRadioUI(Directive directive)
    {
        if (currentRadio != null) Destroy(currentRadio);

        currentRadio = Instantiate(RadioPrefab, RadioSpawnPoint);
        RadioUI radioUI = currentRadio.GetComponent<RadioUI>();
        radioUI.SetDirective(directive);

        // Звук радио
        AudioManager.Instance.Play("RadioStatic");
        StartCoroutine(PlayRadioMessage(directive.Message));
    }

    IEnumerator PlayRadioMessage(string message)
    {
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.Play("RadioMessage");

        // Показ текста
        RadioUI ui = currentRadio.GetComponent<RadioUI>();
        ui.DisplayMessage(message);
    }

    public void OnPlayerChoice(bool execute)
    {
        if (execute)
        {
            ExecuteDirective(currentDirective);
        }
        else
        {
            IgnoreDirective(currentDirective);
        }

        // Сохранить выбор
        SaveSystem.Instance.RecordDirectiveChoice(currentDirective.ID, execute);

        // Закрыть радио
        Destroy(currentRadio);
    }

    void ExecuteDirective(Directive directive)
    {
        directive.IsCompleted = true;
        // Логика выполнения директивы
        Debug.Log($"Директива {directive.Title} выполнена");
    }

    void IgnoreDirective(Directive directive)
    {
        // Штраф к морали
        GameController.Instance.Morale -= 10;
        Debug.Log($"Директива {directive.Title} проигнорирована. Мораль: {GameController.Instance.Morale}");
    }
}

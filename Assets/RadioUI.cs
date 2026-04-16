
```csharp
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RadioUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI MessageText;
    public Button ExecuteButton;
    public Button IgnoreButton;
    
    private Directive currentDirective;
    
    void Start()
    {
        ExecuteButton.onClick.AddListener(() => OnChoice(true));
        IgnoreButton.onClick.AddListener(() => OnChoice(false));
    }
    
    public void SetDirective(Directive directive)
    {
        currentDirective = directive;
        TitleText.text = directive.Title;
    }
    
    public void DisplayMessage(string message)
    {
        StartCoroutine(TypewriterEffect(message));
    }
    
    IEnumerator TypewriterEffect(string message)
    {
        MessageText.text = "";
        foreach (char c in message)
        {
            MessageText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    void OnChoice(bool execute)
    {
        RadioController.Instance.OnPlayerChoice(execute);
    }
}
```


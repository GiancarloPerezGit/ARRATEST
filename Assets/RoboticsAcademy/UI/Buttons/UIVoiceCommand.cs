using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

[RequireComponent(typeof(Selectable))]
public class UIVoiceCommand : MonoBehaviour
{
    private KeywordRecognizer recognizer;

    private void Awake()
    {
        recognizer = new KeywordRecognizer(new string[] { name });
        recognizer.OnPhraseRecognized += OnPhraseRecognized;
    }
    void OnEnable()
    {
        recognizer.Start();
    }

    void OnDisable()
    {
        recognizer.Stop();
    }

    void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (GetComponent<Button>())
        {
            GetComponent<Button>().onClick.Invoke();
        }
        else if (GetComponent<Toggle>())
        {
            Toggle t = GetComponent<Toggle>();
            t.isOn = !t.isOn;
        }
    }
}

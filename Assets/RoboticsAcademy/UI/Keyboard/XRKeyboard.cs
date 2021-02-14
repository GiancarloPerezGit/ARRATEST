using UnityEngine;
using TMPro;
using UnityEngine.Windows.Speech;
using System.Collections;

/// <summary>
/// Class that defines behavior for a standard UI mobile keyboard in unity.
/// </summary>
public class XRKeyboard : MonoBehaviour 
{
    // Input field to write text to.
    public TMP_InputField inputText;

    private DictationRecognizer dictationRec;
    bool speechActive = false;
    string recognizedText = "";

    // Word related variables.
    string word = "";
    int wordIndex = 0;

    // Keyboard UI references.
    [SerializeField] GameObject BaseKeyboard;
    [SerializeField] GameObject alphaShiftedCanvas;
    [SerializeField] GameObject alphaUnShiftedCanvas;
    [SerializeField] GameObject numberShiftedCanvas;
    [SerializeField] GameObject numberUnShiftedCanvas;
    [SerializeField] GameObject voiceCanvas;

    // Specific button references.
    bool lowOn = true;
    bool capsOn = false;
    bool numOn = false;
    bool numShiftOn = false;

    private void OnEnable()
    {
        dictationRec = new DictationRecognizer();

        dictationRec.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
            recognizedText = text;
        };

        // Start off.
        CloseKeyboard();
    }

    private void OnDisable()
    {
        if (dictationRec != null)
        {
            if (dictationRec.Status == SpeechSystemStatus.Running) dictationRec.Stop();
            dictationRec.Dispose();
        }
    }

    /// <summary>
    /// Safely switch input fields.
    /// </summary>
    public void AssignInputField(TMP_InputField field)
    {
        // Assign input field reference.
        inputText = field;

        // Get existing text/ text length to not overwrite original data.
        word = field.text;
        wordIndex = field.text.Length;
    }

    /// <summary>
    /// Check if keyboard is active.
    /// </summary>
    public bool GetBaseKeyboardActive()
    {
        return BaseKeyboard.activeSelf;
    }

    /// <summary>
    /// Open Keyboard.
    /// </summary>
    public void OpenKeyboard()
    {
        // Enable keyboard.
        BaseKeyboard.SetActive(true);
    }

    /// <summary>
    /// Close Keyboard.
    /// </summary>
    public void CloseKeyboard() 
    {
        // Remove text reference and set false.
        inputText = null;
        BaseKeyboard.SetActive(false);

        // Make sure voice is disabled and goes back to default keyboard.
        SwitchToKeyboardFromVoice();
    }

    /// <summary>
    /// Change to unshifted numeric key UI.
    /// </summary>
    public void SwitchToNumericKeys() 
    {
        // Disable all alpha keyboards and enable unshifted numeric keyboard.
        voiceCanvas.SetActive(false);
        alphaUnShiftedCanvas.SetActive(false);
        alphaShiftedCanvas.SetActive(false);
        numberShiftedCanvas.SetActive(false);
        numberUnShiftedCanvas.SetActive(true);


        // change boolean vars to reflect unshifted numeric state.
        numOn = true;
        numShiftOn = false;
        lowOn = true;
        capsOn = false;
    }

    /// <summary>
    /// Change to unshifted alpha key UI.
    /// </summary>
    public void SwitchToAlphaKeys() 
    {
        // Disable all numeric keyboards and enable unshifted alpha keyboard.
        voiceCanvas.SetActive(false);
        numberUnShiftedCanvas.SetActive(false);
        numberShiftedCanvas.SetActive(false);
        alphaShiftedCanvas.SetActive(false);
        alphaUnShiftedCanvas.SetActive(true);

        // change boolean vars to reflect unshifted alpha state.
        lowOn = true;
        numOn = false;
        numShiftOn = false;
    }

    /// <summary>
    /// Context-specific shift key behavior.
    /// </summary>
    public void ShiftKey() 
    {
        // If numeric and shift key pressed, change to shifted keyboard, change booleans.
        if (numOn) {
            numberUnShiftedCanvas.SetActive(false);
            numberShiftedCanvas.SetActive(true);

            numShiftOn = true;
            numOn = false;
        }
        // If numeric shift was already on, go back to unshifted numeric keyboard.
        else if (numShiftOn) {
            numberShiftedCanvas.SetActive(false);
            numberUnShiftedCanvas.SetActive(true);

            numOn = true;
            numShiftOn = false;
        }
        // Now check for alpha keyboards.
        else {
            // If caps was not on, go to shifted alpha keyboard, change boolean.
            if (!capsOn) {
            alphaUnShiftedCanvas.SetActive(false);
            alphaShiftedCanvas.SetActive(true);

            capsOn = true;
            }
            // Otherwise go to unshifted alpha keyboard, change boolean.
            else {
            alphaShiftedCanvas.SetActive(false);
            alphaUnShiftedCanvas.SetActive(true);

            capsOn = false;
            }
        }
    }

    /// <summary>
    /// Simple backspace behavior.
    /// </summary>
    public void BackSpaceKey()
    {
        if (wordIndex > 0)
        {
            // Decrement word index and delete text using substring.
            wordIndex--;
            word = inputText.text.Substring(0, wordIndex);
            inputText.text = word;
        }
    }

    /// <summary>
    /// Typical character key behavior.
    /// </summary>
    public void CharKeys(string key) 
    {
        // Append key to string and update index/ input field text.
        wordIndex++;
        word = word + key;
        inputText.text = word;
    }


    /// <summary>
    /// Change to voice UI.
    /// </summary>
    public void SwitchToVoiceFromKeyboard()
    {
        // Disable all keyboards, enable voice.
        voiceCanvas.SetActive(true);
        numberUnShiftedCanvas.SetActive(false);
        numberShiftedCanvas.SetActive(false);
        alphaShiftedCanvas.SetActive(false);
        alphaUnShiftedCanvas.SetActive(false);

        DictationEnable(true);
    }

    /// <summary>
    /// Change back to default keyboard.
    /// </summary>
    public void SwitchToKeyboardFromVoice()
    {
        SwitchToAlphaKeys();
        DictationEnable(false);
    }

    /// <summary>
    /// Typical character key behavior.
    /// </summary>
    void DictationEnable(bool active)
    {   
        if (active) StartCoroutine(DictationPolling());
        else speechActive = false;
    }

    IEnumerator DictationPolling()
    {
        // have to shut down all phrase recognizers.
        if (PhraseRecognitionSystem.Status == SpeechSystemStatus.Running) PhraseRecognitionSystem.Shutdown();

        // Have to wait until the system has actually turned off (not immediate).
        while (PhraseRecognitionSystem.Status == SpeechSystemStatus.Running) yield return null;

        dictationRec.Start();
        speechActive = true;

        while (speechActive)
        {
            word = recognizedText;
            wordIndex = word.Length;
            inputText.text = word;
            yield return null;
        }

        if (dictationRec.Status == SpeechSystemStatus.Running) dictationRec.Stop();

        // Have to wait until the system has actually turned off (not immediate).
        while (dictationRec.Status == SpeechSystemStatus.Running) yield return null;

        // restart them because dictation is done.
        if (PhraseRecognitionSystem.Status == SpeechSystemStatus.Stopped) PhraseRecognitionSystem.Restart();
    }
}
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using TMPro;
using UnityEngine;

/// <summary>
/// Custom Mixed Reality Keyboard to be used on multiple platforms using MRTK.
/// </summary>
[RequireComponent(typeof(XRKeyboard))]
public class XRKeyboardController : MonoBehaviour, IMixedRealityPointerHandler
{
    // Keyboard to work with.
    XRKeyboard keyboard;

    /// <summary>
    /// When enabled, register to recieve input events.
    /// </summary>
    void OnEnable()
    {
        // Register Handler to not need focusing.
        if (CoreServices.InputSystem != null) CoreServices.InputSystem.RegisterHandler<IMixedRealityPointerHandler>(this);
    }

    /// <summary>
    /// When disabled, unregister from input events.
    /// </summary>
    void OnDisable()
    {
        // Remove Handler
        if (CoreServices.InputSystem != null) CoreServices.InputSystem.UnregisterHandler<IMixedRealityPointerHandler>(this);
    }

    void Awake()
    {
       keyboard  = GetComponent<XRKeyboard>();
    }

    /// <summary>
    /// Enable keyboard if clicked on an input field, and allow it to add text to the field.
    /// </summary>
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        // Check if selected object is not null and it has an inputfield.
        GameObject selected = eventData.selectedObject;
        if (selected && selected.GetComponent<TMP_InputField>()) // Check if text box.
        {
            // If not already active, set it to active.
            if (!keyboard.GetBaseKeyboardActive())
            {
                keyboard.OpenKeyboard(); // Enable Keyboard.
            }
            // Set to current inputfield.
            keyboard.AssignInputField(selected.GetComponent<TMP_InputField>());
        }
    }

    /// <summary>
    /// OnUp functions.
    /// </summary>
    public void OnPointerUp(MixedRealityPointerEventData eventData) { }

    /// <summary>
    /// OnDown functions.
    /// </summary>
    public void OnPointerDown(MixedRealityPointerEventData eventData) { }

    /// <summary>
    /// OnDragged functions.
    /// </summary>
    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }
}
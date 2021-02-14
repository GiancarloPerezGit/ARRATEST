using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Basic Class for implementing a button that does a loop of commands while pressed.
/// </summary>
[System.Serializable]
public class PushButton : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealityFocusHandler
{
    // Pressed state.
    bool pressed = false;

    // Colors for when a button is highlighted or pressed.
    [SerializeField] Color highlightedColor;
    [SerializeField] Color pressedColor;

    // Material/Renderer references.
    [SerializeField] MeshRenderer buttonRenderer;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material pressMaterial;

    // Unity events for different button actions.
    [SerializeField] UnityEvent onPressedAction;
    [SerializeField] UnityEvent whilePressedAction;
    [SerializeField] UnityEvent onReleaseAction;

    /// <summary>
    /// Check if button is currently being pressed or not.
    /// </summary>
    public bool isPressed()
    {
        return pressed;
    }

    public void Start()
    {
        buttonRenderer.material = defaultMaterial;
    }

    /// <summary>
    /// When pointer is down, button is now pressed until pointer is up.
    /// </summary>
    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        pressed = true;
        Debug.Log(gameObject + " Pressed!");

        // Change material color to indicate state.
        SetColorMaterial(pressMaterial, pressedColor);

        // invoke on pressed functions.
        onPressedAction.Invoke();

        // Start press loop.
        StartCoroutine(PressedLoop());
    }

    /// <summary>
    /// When pointer is up, the button is no longer pressed.
    /// </summary>
    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        // Stop press loop, invoke release functions.
        StopCoroutine(PressedLoop());
        onReleaseAction.Invoke();

        pressed = false;
        Debug.Log(gameObject + " Released!");

        // Change material color to indicate state.
        SetColorMaterial(defaultMaterial, Color.white);
    }

    /// <summary>
    /// When button is focused, highlight it.
    /// </summary>
    public void OnFocusEnter(FocusEventData eventData)
    {
        // Change material color to indicate state.
        SetColorMaterial(pressMaterial, highlightedColor);
    }

    /// <summary>
    /// When no longer focused, go back to normal color.
    /// </summary>
    public void OnFocusExit(FocusEventData eventData)
    {
        // Change material color to indicate state.
        SetColorMaterial(defaultMaterial, Color.white);
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }

    public void OnPointerClicked(MixedRealityPointerEventData eventData) { }

    /// <summary>
    /// Simple coroutine to loop while press event every frame.
    /// </summary>
    IEnumerator PressedLoop()
    {
        // Infinite loop to invoke while pressed action.
        while(true)
        {
            whilePressedAction.Invoke();
            yield return null;
        }
    }

    /// <summary>
    /// Set material/color for button.
    /// </summary>
    private void SetColorMaterial(Material mat, Color col)
    {
        buttonRenderer.material = mat;
        buttonRenderer.material.SetColor("_Color", col);
    }
}

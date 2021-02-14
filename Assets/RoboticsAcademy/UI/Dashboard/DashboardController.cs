//using Leap.Unity;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DashboardController : MonoBehaviour, IMixedRealityPointerHandler
{
    [SerializeField] AudioSource dashboardMusic;
    [SerializeField] UIPanelMinMax drawerPanel;
    [SerializeField] ToggleGroup dashboardButtons;
    public XRKeyboard keyboard;

    //[SerializeField] LeapXRServiceProvider leapXrServiceProvider;
    [SerializeField] Vector3 DistanceFromCamera_Controller;
    [SerializeField] Vector3 Scale_Controller;
    [SerializeField] Vector3 DistanceFromCamera_Hands;
    [SerializeField] Vector3 Scale_Hands;

    [SerializeField] Toggle UIMenuButton = null;
    // only one panel is open at any given time.
    Toggle toggleToOpen = null;

    Vector3 DistanceFromCamera = Vector3.zero;
    Vector3 Scale = Vector3.one;

    // dashboard starts off.
    bool isOpen = false;
    public bool keyboardOpen { get; private set; } = false;

    public void Awake()
    {
        if (UIMenuButton) UIMenuButton.onValueChanged.AddListener(UIMenuButtonToggle);
    }

    public void UIMenuButtonToggle(bool on)
    {
        if (on) OpenDashboard();
        else CloseDashboard();
    }

    public void PhysicalMenuButtonToggle()
    {
        if (UIMenuButton) UIMenuButton.isOn = isOpen;
        if (!isOpen) OpenDashboard();
        else CloseDashboard();
        keyboard.CloseKeyboard();
    }

    public void OpenDashboard()
    {
        // Check with distance is needed.
        //if (!leapXrServiceProvider.enabled)
        //{
        //    DistanceFromCamera = DistanceFromCamera_Controller;
        //    Scale = Scale_Controller;
        //}
        //else
        //{
            DistanceFromCamera = DistanceFromCamera_Hands;
            Scale = Scale_Hands;
        

        if (!isOpen)
        {
            // Set up rotation/position for dashboard.
            Vector3 cameraRot = Camera.main.transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(cameraRot.x, cameraRot.y, 0);
            transform.position = Camera.main.transform.position + transform.forward * DistanceFromCamera.z
                                                                     + transform.up * DistanceFromCamera.y
                                                                     + transform.right * DistanceFromCamera.x;
            transform.localScale = Scale;
            if (toggleToOpen != null)
            {
                Debug.Log("something to open");
                toggleToOpen.isOn = true;
            }

            // Play dashboard music.
            if (dashboardMusic != null) dashboardMusic.Play();
            drawerPanel.MinMaxPanel(true);
            isOpen = true;
        }
    }

    public void CloseDashboard()
    {
        if (isOpen)
        {
            // keep track of panel to open when reopening menu.
            foreach (Toggle t in dashboardButtons.ActiveToggles())
            {
                toggleToOpen = t;
                t.isOn = false;
            }

            // Stop dashboard music.
            if (dashboardMusic != null) dashboardMusic.Stop();
            drawerPanel.MinMaxPanel(false);
            isOpen = false;

            // Dont use dashkeyboardclose, will bring back the drawer.
            if (keyboardOpen)
            {
                keyboard.CloseKeyboard();
                keyboardOpen = false;
            }
        }
    }

    // KEYBOARD RELATED CODE.
    public void OpenDashKeyboard()
    {
        keyboard.OpenKeyboard();
        drawerPanel.MinMaxPanel(false);
        keyboardOpen = true;
    }

    public void CloseDashKeyboard()
    {
        keyboard.CloseKeyboard();
        drawerPanel.MinMaxPanel(true);
        keyboardOpen = false;
    }

    public void AssignInputFieldDashKeyboard(TMP_InputField field)
    {
        keyboard.AssignInputField(field);
    }

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
            if (!keyboardOpen)
            {
                OpenDashKeyboard(); // Enable Keyboard.
            }
            // Set to current inputfield.
            AssignInputFieldDashKeyboard(selected.GetComponent<TMP_InputField>());
        }
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData) { }
    public void OnPointerDown(MixedRealityPointerEventData eventData) { }
    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }
}

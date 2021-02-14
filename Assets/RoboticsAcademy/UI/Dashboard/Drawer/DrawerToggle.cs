using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class DrawerToggle : MonoBehaviour
{
    Animator animator;
    public Toggle toggle { get; private set; }
    [SerializeField] private UIPanelMinMax panel;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(ButtonAnimate);
    }
    void ButtonAnimate(bool on)
    {
        animator.SetBool("ToggleOn", on);
        if (panel != null) panel.MinMaxPanel(on);
    }
}

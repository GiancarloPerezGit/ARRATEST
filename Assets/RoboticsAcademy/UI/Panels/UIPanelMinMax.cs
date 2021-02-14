using UnityEngine;

/// <summary>
/// Class that defines a UI panel with animated behaviour.
/// </summary>
public class UIPanelMinMax : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] bool startOpen = false;
    public bool isOpen { get; private set; } = false;
    float scaleMultiplier = 1;

    public void Awake()
    {
        MinMaxPanel(startOpen);

        // use scale on x to drive final scale.
        scaleMultiplier = transform.localScale.x;
    }
    /// <summary>
    /// Animated depending on if Min (false) or Max (true).
    /// </summary>
    public void MinMaxPanel(bool open)
    {
        if (isOpen != open) // make sure its not already open or closed.
        {
            isOpen = open;
            anim.SetBool("active", open);
        }
    }

    public void LateUpdate()
    {
        //if (transform.localScale.x != scaleMultiplier)
            transform.localScale *= scaleMultiplier;
    }
}

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class UIAudio : MonoBehaviour, IPointerUpHandler, IPointerEnterHandler
{
    [SerializeField] AudioClip clickSound, hoverSound;
    [SerializeField] AudioMixerGroup mixerGroup;
    AudioSource click, hover;

    private void Start()
    {
        
        click = gameObject.AddComponent<AudioSource>(); click.clip = clickSound;
        hover = gameObject.AddComponent<AudioSource>(); hover.clip = hoverSound;

        click.outputAudioMixerGroup = mixerGroup;
        hover.outputAudioMixerGroup = mixerGroup;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        click.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover.Play();
    }
}
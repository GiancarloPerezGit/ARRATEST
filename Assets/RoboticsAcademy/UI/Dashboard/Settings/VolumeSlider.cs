
using UnityEngine;
 public class VolumeSlider : MonoBehaviour
{
    
    [SerializeField] UnityEngine.UI.Slider slider;
    [SerializeField] UnityEngine.Audio.AudioMixer mixer;
    [SerializeField] string volumeVariable;

    private void Awake()
    {
        // slider goes from 0 to 1.
        slider.onValueChanged.AddListener(delegate { SetVolume(slider.value); });
    }

    public void SetVolume(float value)
    {
        mixer.SetFloat(volumeVariable, ConvertToDecibel(value));
    }
  
    float ConvertToDecibel(float value)
    {
        // Max volume is at 0 db.
        return Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;
    }
}
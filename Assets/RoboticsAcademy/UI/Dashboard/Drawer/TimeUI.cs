using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = System.DateTime.Now.ToString("HH:mm tt\nMM/dd/yyyy");
    }
}

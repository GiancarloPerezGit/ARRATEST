using UnityEngine;

public class KeyListener : MonoBehaviour
{
    [SerializeField] ButtonW a1p;
    [SerializeField] ButtonW a1m;
    [SerializeField] ButtonW a2p;
    [SerializeField] ButtonW a2m;
    [SerializeField] ButtonW a3p;
    [SerializeField] ButtonW a3m;
    [SerializeField] ButtonW a4p;
    [SerializeField] ButtonW a4m;

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if (e.character == '1')
            {
                a1p.setPress(true);
                Debug.Log("TEST");
            }
            else if (e.character == '2')
            {
                a1m.setPress(true);
                Debug.Log("TEST");
            }
            else if (e.character == '3')
            {
                a2p.setPress(true);
                Debug.Log("TEST");
            }
            else if (e.character == '4')
            {
                a2m.setPress(true);
                Debug.Log("TEST");
            }
            else if (e.character == '5')
            {
                a3p.setPress(true);
                Debug.Log("TEST");
            }
            else if (e.character == '6')
            { 
                a3m.setPress(true);
                Debug.Log("TEST");
            }
            else if (e.character == '7')
            {
                a4p.setPress(true);
                Debug.Log("TEST");
            }
            else if (e.character == '8')
            {
                a4m.setPress(true);
                Debug.Log("TEST");
            }
        }
        else
        {
            a1p.setPress(false);
            a1m.setPress(false);
            a2p.setPress(false);
            a2m.setPress(false);
            a3p.setPress(false);
            a3m.setPress(false);
            a4p.setPress(false);
            a4m.setPress(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenManagerScript : MonoBehaviour
{
    public Animator animator;
    private int LeveltoLoad;


    // Update is called once per frame
    public void OnClick(Button btn)
    {
        switch (btn.name)
        {
            case "BtnLesson1": FadeToLevel(1); break;
            case "BtnLesson2": FadeToLevel(1); break;
            case "BtnLesson3": FadeToLevel(1); break;
        }
    }
    public void FadeToLevel(int lvlIndex)
    {
        LeveltoLoad = lvlIndex;
        animator.SetTrigger("FadeOut");
    }
    
    public void OnfadeComplete()
    {
        SceneManager.LoadScene(LeveltoLoad);
    }

}

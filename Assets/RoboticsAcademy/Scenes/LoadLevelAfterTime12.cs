using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAfterTime12 : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoading = 0f;
    [SerializeField]
    private string sceneNameToLoad;

    private float timeElapsed;

    public Animator animator;

    // Update is called once per frame
    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > delayBeforeLoading)
        {
            FadeToLevel(sceneNameToLoad);
        }

    }

    public void FadeToLevel(string scenename)
    {
        animator.SetTrigger("FadeOut");
        //SceneManager.LoadScene(scenename);
    }
    public void OnFadeComplete() 
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
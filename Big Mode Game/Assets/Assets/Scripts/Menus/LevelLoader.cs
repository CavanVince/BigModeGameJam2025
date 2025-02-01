using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float WaitTime = 1;

    [SerializeField] Canvas Animation;

    public void LoadNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Animation.enabled = true;
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));


    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(WaitTime);

        SceneManager.LoadScene(levelIndex);
    }
}

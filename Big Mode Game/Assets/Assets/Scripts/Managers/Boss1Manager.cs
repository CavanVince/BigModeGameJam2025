using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Manager : MonoBehaviour
{
    [SerializeField]
    GameObject bricks;

    void Start()
    {
        bricks.SetActive(false);
        StartCoroutine(FightOpening());
    }

    IEnumerator FightOpening() 
    {
        // Enable the wizard and have them talk
        DialogueManager.Instance.EnableWizardSpeak();
        DialogueManager.Instance.StartDialogue("Welcome to the first boss fight, and your doom.");
        yield return new WaitForSeconds(5); // We just have to guess the timing

        DialogueManager.Instance.StartDialogue("To begin... I will start by taking your screen.");
        yield return new WaitForSeconds(5);

        DialogueManager.Instance.StartDialogue("Now, let the fun begin!");
        yield return new WaitForSeconds(5);

        bricks.SetActive(true);
    }
}

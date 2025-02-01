using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI multText;

    [SerializeField]
    private TextMeshProUGUI wizardText;

    #region Animate Text
    [SerializeField]
    private float textSpeed;

    [SerializeField]
    private string[] lines;

    private int index;
    #endregion

    private void Start()
    {
        index = 0;
        wizardText.text = "";
        //StartDialogue();
    }

    private void StartDialogue(bool shouldRandom = true)
    {
        if (shouldRandom) index = Random.Range(0, lines.Length);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            wizardText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}

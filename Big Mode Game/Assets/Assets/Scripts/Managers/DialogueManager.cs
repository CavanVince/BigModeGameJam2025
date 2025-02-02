using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField]
    private Transform wizardBox;

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
    #endregion

    private Vector3 oriPos;
    private Vector3 oriScale;
    private IEnumerator dialogueCoroutine;

    public bool InTopRight { get; private set; }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        oriPos = wizardBox.position;
        oriScale = wizardBox.localScale;
    }

    /// <summary>
    /// Dialogue with a random selection from the internal array
    /// </summary>
    public void StartDialogue()
    {
        int index = Random.Range(0, lines.Length);

        if (dialogueCoroutine != null) StopCoroutine(dialogueCoroutine);
        dialogueCoroutine = TypeLine(lines[index]);
        StartCoroutine(dialogueCoroutine);
    }

    /// <summary>
    /// Dialogue a random option from the given array
    /// </summary>
    /// <param name="randomLines">Array to pick random dialogue option from</param>
    public void StartDialogue(string[] randomLines)
    {
        int index = Random.Range(0, randomLines.Length);

        if (dialogueCoroutine != null) StopCoroutine(dialogueCoroutine);
        dialogueCoroutine = TypeLine(randomLines[index]);
        StartCoroutine(dialogueCoroutine);
    }

    /// <summary>
    /// Helper function to display the specified dialogue string
    /// </summary>
    /// <param name="dialogue">The dialogue to display</param>
    public void StartDialogue(string dialogue)
    {
        if (dialogueCoroutine != null) StopCoroutine(dialogueCoroutine);
        dialogueCoroutine = TypeLine(dialogue);
        StartCoroutine(dialogueCoroutine);
    }

    /// <summary>
    /// Helper function to enable wizard speaking
    /// </summary>
    public void EnableWizardSpeak()
    {
        wizardText.text = "";
        wizardText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        multText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Helper function to disable wizard speaking
    /// </summary>
    public void DisableWizardSpeak()
    {
        wizardText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        multText.gameObject.SetActive(true);
    }

    /// <summary>
    /// Helper function to move wizard to top right of screen
    /// </summary>
    public void MoveToTopRight()
    {
        wizardBox.DOScale(new Vector3(oriScale.x, 0, oriScale.z), 0.2f).SetDelay(0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            DisableWizardSpeak();
            wizardBox.position = oriPos;
            wizardBox.DOScale(oriScale, 0.5f).SetEase(Ease.Linear);
            InTopRight = true;
        });
    }

    /// <summary>
    /// Helper function to move wizard to bottom center of screen
    /// </summary>
    /// <param name="dialogue">Optional dialogue to display after the wizard has moved down</param>
    public void MoveToBottomCenter(string dialogue = "")
    {
        wizardBox.DOScale(new Vector3(oriScale.x, 0, oriScale.z), 0.2f).SetDelay(0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            EnableWizardSpeak();
            wizardBox.localPosition = new Vector3(-461, -327, 0);
            wizardBox.DOScale(oriScale, 0.5f).SetEase(Ease.Linear);
            InTopRight = false;
            if (dialogue != "")
            {
                StartDialogue(dialogue);
            }
        });
    }

    IEnumerator TypeLine(string dialogue)
    {
        wizardText.text = "";
        foreach (char c in dialogue.ToCharArray())
        {
            wizardText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    /// <summary>
    /// Helper function to display text without typing
    /// </summary>
    /// <param name="textToDisplay">The text to display</param>
    public void DisplayPlainText(string textToDisplay) 
    {
        wizardText.text = textToDisplay;
    }
}

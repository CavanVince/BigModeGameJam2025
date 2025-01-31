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

    private void Update()
    {
        if (scoreText.gameObject.activeInHierarchy) AnimateText(scoreText);
        if (multText.gameObject.activeInHierarchy) AnimateText(multText);
        if (wizardText.gameObject.activeInHierarchy) AnimateText(wizardText);
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            wizardText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }


    private void AnimateText(TextMeshProUGUI textToAnim)
    {
        textToAnim.ForceMeshUpdate();
        Mesh textMesh = textToAnim.mesh;
        Vector3[] vertices = textMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 offset = WobbleText(Time.time + i);
            vertices[i] += offset;
        }
        textMesh.vertices = vertices;
        textToAnim.canvasRenderer.SetMesh(textMesh);
    }

    private Vector2 WobbleText(float time)
    {
        return new Vector2(Mathf.Sin(time * 4f), Mathf.Cos(time * 3f));
    }

}

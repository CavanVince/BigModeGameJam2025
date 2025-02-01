using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimateFloatText : MonoBehaviour
{
    TextMeshProUGUI textToAnim;


    void Start()
    {
         textToAnim = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        AnimateText();
    }

    /// <summary>
    /// Animates the given text to wobble
    /// </summary>
    private void AnimateText()
    {
        if (textToAnim.text == "") return;
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

    /// <summary>
    /// Helper function to wobble text
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private Vector2 WobbleText(float time)
    {
        return new Vector2(Mathf.Sin(time * 4f), Mathf.Cos(time * 3f));
    }
}

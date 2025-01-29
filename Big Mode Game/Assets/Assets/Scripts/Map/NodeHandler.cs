using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NodeHandler : MonoBehaviour
{
    public Node node;
    public Map map;
    void Start()
    {
        if(node.row <= 0)
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
        }

    }
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        foreach(Node childNode in node.childNodes)
        {
            map.nodePrefabs[childNode.row, childNode.column].GetComponent<BoxCollider2D>().enabled = true;
        }
        if (node.nodeType == NodeTypes.ENEMY)
        {
            Debug.Log("Scene 1 loaded");
            SceneManager.LoadScene(sceneName: "Act 1 Template");
        }
        else if (node.nodeType == NodeTypes.SHOP)
        {
            Debug.Log("Shop scene loaded");
            SceneManager.LoadScene(sceneName: "Shop Scene");
        }
    }
}

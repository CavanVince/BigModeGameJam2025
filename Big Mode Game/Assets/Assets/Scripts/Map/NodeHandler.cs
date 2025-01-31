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
        if (node.row <= 0)
        {
            this.GetComponent<BoxCollider2D>().enabled = true;

        }
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
            //SceneManager.LoadScene(sceneName: "Act 1 Template");
            BasicLevelManager.Instance.LoadEnemyLevel();
        }
        else if (node.nodeType == NodeTypes.SHOP)
        {
            //SceneManager.LoadScene(sceneName: "Shop Scene");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NodeHandler : MonoBehaviour
{
    public Node node;
    public Map map;

    [SerializeField]
    private Sprite check;

    [SerializeField]
    private Sprite cross;

    private bool initialSpawn = true;

    void Start()
    {
        if (node.row <= 0 && initialSpawn)
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
        initialSpawn = false;
    }

    private void OnMouseDown()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        // Enable the child nodes
        foreach (Node childNode in node.childNodes)
        {
            map.nodePrefabs[childNode.row, childNode.column].GetComponent<BoxCollider2D>().enabled = true;
        }

        // Change the sprites and disable the box colliders
        for (int i = 0; i < map.nodePrefabs.GetLength(1); i++)
        {
            if (i != node.column && map.nodePrefabs[node.row, i] != null)
            {
                map.nodePrefabs[node.row, i].GetComponent<SpriteRenderer>().sprite = cross;
                map.nodePrefabs[node.row, i].GetComponent<BoxCollider2D>().enabled = false;
            }
            else if(map.nodePrefabs[node.row, i] != null)
            {
                map.nodePrefabs[node.row, i].GetComponent<SpriteRenderer>().sprite = check;
            }
        }

        if (node.nodeType == NodeTypes.ENEMY)
        {
            //SceneManager.LoadScene(sceneName: "Act 1 Template");
            BasicLevelManager.Instance.LoadEnemyLevel();
        }
        else if (node.nodeType == NodeTypes.SHOP)
        {
            //SceneManager.LoadScene(sceneName: "Shop Scene");
            BasicLevelManager.Instance.LoadShop();
        }
        else if (node.nodeType == NodeTypes.BOSS) 
        {
            BasicLevelManager.Instance.LoadBoss();
        }
        else if (node.nodeType == NodeTypes.MYSTERY)
        {
            BasicLevelManager.Instance.LoadEvent();
        }
    }
}

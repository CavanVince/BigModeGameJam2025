using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Map : MonoBehaviour
{
    public GameObject NodePrefab;

    public Sprite enemy;
    public Sprite mystery;
    public Sprite shop;
    Node[,] emptyMap = new Node[9, 7];
    GameObject[,] nodePrefabs = new GameObject[9, 7];
    Node previousNode;



    void Start()
    {

        generatePath(0, 1);
        generatePath(0, 3);
        generatePath(0, 5);
        drawMap();
    }



    void drawMap() 
    {
        Stack<Node> nodeStack = new Stack<Node>();

        for (int c = 0; c < emptyMap.GetLength(1); c++)
        {
            if (emptyMap[0, c] != null)
            {
                Node currentNode = emptyMap[0, c];
                nodeStack.Push(currentNode);
            }
        }
        List<Node> seenNodes = new List<Node>();
        while (nodeStack.Count > 0)

        {
            Node currentNode = nodeStack.Pop();

            foreach (Node childNode in currentNode.childNodes)
            {
                GameObject nestedLineRenderer = new GameObject();
                LineRenderer lr = nestedLineRenderer.AddComponent<LineRenderer>();
                Transform currentNodeTransform = nodePrefabs[currentNode.row, currentNode.column].transform;

                Instantiate(nestedLineRenderer, currentNodeTransform.position, Quaternion.identity, currentNodeTransform);
                lr.SetPositions(new Vector3[] { new Vector3(currentNode.row * 15, currentNode.column * 15, 0), nodePrefabs[childNode.row, childNode.column].transform.position });
                if (seenNodes.Contains(childNode))
                {
                    continue;
                }

                nodeStack.Push(childNode);
            }
            seenNodes.Add(currentNode);
        }
    }

    void generatePath(int startingRow, int startingColumn)
    {
        int column = startingColumn;
        int row = startingRow;
        previousNode = null;


        while (row < emptyMap.GetLength(0))
        {
            GameObject newNode;
            if (emptyMap[row, column] == null)
            {
                emptyMap[row, column] = new Node(getRandomNode(), row, column);
                newNode = Instantiate(NodePrefab, new Vector3(row * 15, column * 15, 0), Quaternion.identity);
                nodePrefabs[row, column] = newNode;
                emptyMap[row, column].row = row;
                emptyMap[row, column].column = column;
                switch (emptyMap[row, column].nodeType)
                {
                    case NodeTypes.ENEMY:
                        newNode.GetComponent<SpriteRenderer>().sprite = enemy;
                        break;
                    case NodeTypes.MYSTERY:
                        newNode.GetComponent<SpriteRenderer>().sprite = mystery;
                        break;
                    case NodeTypes.SHOP:
                        newNode.GetComponent<SpriteRenderer>().sprite = shop;
                        break;
                }
            }


            if (previousNode != null)
            {
                previousNode.childNodes.Add(emptyMap[row, column]);
            }
            previousNode = emptyMap[row, column];


            int[] directions = { -1, 0, 1 };
            int offset = directions[Random.Range(0, directions.Length)];

            if (column + offset < 0)
            {
                offset = 0;
            }
            else if (column + offset >= emptyMap.GetLength(1))
            {
                offset = 0;
            }
            row = row + 1;
            column = column + offset;
        }

    }

    NodeTypes getRandomNode()
    {

        NodeTypes[] possibleNodes = { NodeTypes.ENEMY, NodeTypes.BOSS, NodeTypes.CHEST, NodeTypes.MYSTERY, NodeTypes.REST, NodeTypes.SHOP };
        return possibleNodes[Random.Range(0, possibleNodes.Length)];
    }
}


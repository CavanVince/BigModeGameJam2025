using DG.Tweening.Core.Easing;
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
    public Sprite boss;
    Node[,] emptyMap = new Node[5, 4];
    public GameObject[,] nodePrefabs = new GameObject[5, 4];
    Node previousNode;
    public Material lineMaterial;

    private Transform mapBackground;
    private Vector3 mapOffset = new Vector3(4, 3);

    // Has a map been generated?
    private bool madeMap = false;


    void Start()
    {
        if (madeMap) return;

        mapBackground = transform.Find("Map Background");

        generatePath(0, 1);
        generatePath(0, 2);
        generatePath(0, 3);


        drawMap();
        madeMap = true;
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
                Transform currentNodeTransform = nodePrefabs[currentNode.row, currentNode.column].transform;
                GameObject nestedLineRenderer = new GameObject();
                nestedLineRenderer.transform.position = currentNodeTransform.position;
                nestedLineRenderer.transform.SetParent(currentNodeTransform);
                LineRenderer lr = nestedLineRenderer.AddComponent<LineRenderer>();
                lr.sortingOrder = 4;
                lr.textureMode = LineTextureMode.Tile;
                lr.textureScale = new Vector2(0.25f, 1);
                lr.useWorldSpace = false;
                lr.SetPositions(new Vector3[] { Vector3.up * 0.4f, nestedLineRenderer.transform.InverseTransformPoint(nodePrefabs[childNode.row, childNode.column].transform.position) - Vector3.up * 0.4f });
                lr.material = lineMaterial;
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


        while (row < emptyMap.GetLength(0) - 1)
        {
            GameObject newNode;
            if (emptyMap[row, column] == null)
            {
                emptyMap[row, column] = row == 0 ? new Node(NodeTypes.BOSS, row, column) : new Node(getRandomNode(), row, column);
                newNode = Instantiate(NodePrefab, new Vector3(column * 3, row * 3, 0) + mapBackground.position - mapBackground.GetComponent<SpriteRenderer>().bounds.size / 2 + mapOffset, Quaternion.identity);
                newNode.transform.SetParent(mapBackground, true);
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
                NodeHandler nodeHandler = newNode.GetComponent<NodeHandler>();
                nodeHandler.node = emptyMap[row, column];
                nodeHandler.map = this;
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
        column = emptyMap.GetLength(1) / 2;
        if (emptyMap[row, column] == null)
        {
            emptyMap[row, column] = new Node(NodeTypes.BOSS, row, column);
            emptyMap[row, column].row = row;
            emptyMap[row, column].column = column;
            nodePrefabs[row, column] = Instantiate(NodePrefab, new Vector3((column * 3 + .5f), row * 3, 0) + mapBackground.position - mapBackground.GetComponent<SpriteRenderer>().bounds.size / 2 + mapOffset, Quaternion.identity);
            nodePrefabs[row, column].transform.SetParent(mapBackground, true);
            nodePrefabs[row, column].GetComponent<SpriteRenderer>().sprite = boss;
            NodeHandler nodeHandler = nodePrefabs[row, column].GetComponent<NodeHandler>();
            nodeHandler.node = emptyMap[row, column];
            nodeHandler.map = this;
        }
        if (!previousNode.childNodes.Contains(emptyMap[row, column]))
        {
            previousNode.childNodes.Add(emptyMap[row, column]);
        }

    }

    NodeTypes getRandomNode()
    {
        int randomNodeGen = Random.Range(0, 101);
        NodeTypes[] possibleNodes = { NodeTypes.ENEMY, NodeTypes.MYSTERY, NodeTypes.SHOP };

        if (randomNodeGen <= 20)
        {
            return possibleNodes[2];
        }
        else if (randomNodeGen <= 75)
        {
            return possibleNodes[0];
        }
        else
        {
            return possibleNodes[1];
        }
    }
}


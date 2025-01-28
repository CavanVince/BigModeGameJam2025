using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{
    public List<Node> childNodes;
    public NodeTypes nodeType;
    public int row;
    public int column;

    public Node(NodeTypes nodeType, int row, int column)
    {
        childNodes = new List<Node>();
        this.nodeType = nodeType;
        this.row = row;
        this.column = column;
    }
}
public enum NodeTypes
{
    ENEMY,
    BOSS,
    CHEST,
    REST,
    SHOP,
    MYSTERY
}
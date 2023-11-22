using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANode : MonoBehaviour
{
    public bool isWalkAble;
    public Vector3 worldPos;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public ANode parentNode;

    public ANode(bool walkable, Vector3 worldPos, int nGridX, int nGridY)
    {
        isWalkAble = walkable;
        this.worldPos = worldPos;
        gridX = nGridX;
        gridY = nGridY;
    }

    public int fCost
    {
        get { return gCost + hCost; }
    }
}

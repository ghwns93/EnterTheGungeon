using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANode : MonoBehaviour
{
    public bool isWalkAble;
    public Vector3 worldPos;

    public ANode(bool walkable, Vector3 worldPos)
    {
        isWalkAble = walkable;
        this.worldPos = worldPos;
    }
}

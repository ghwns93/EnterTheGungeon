using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AGrid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public List<ANode> path;
    ANode[,] grid;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;


    // Start is called before the first frame update
    void Start()
    {
        nodeDiameter = nodeRadius * 2; // 설정한 반지름으로 지름을 구함
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); //그리드 가로
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter); //그리드 세로

        CreateGrid();
    }

    void CreateGrid()
    {
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        Vector3 worldPoint;

        grid = new ANode[gridSizeX, gridSizeY];

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapBox(worldPoint, new Vector2(nodeDiameter - 0.1f, nodeDiameter - 0.1f), 90, unwalkableMask));

                grid[x, y] = new ANode(walkable, worldPoint, x, y);
            }
        }
    }

    // 노드의 주변 노드(8방면)를 찾는 함수
    public List<ANode> GetNeighbours(ANode node)
    {
        List<ANode> neighbours = new List<ANode>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue; //자기자신인 경우 스킵

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                // X,Y의 값이 Grid범위안에 있을 경우
                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX,checkY]);
                }
            }
        }

        return neighbours;
    }

    // 유니티의 WorldPosition으로 부터 그리드상의 노드를 찾는 함수
    public ANode GetNodeFromWorldPoint(Vector3 worldPos)
    {
        float percentX = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPos.y + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        if(grid != null)
        {
            foreach(ANode n in grid)
            {
                Gizmos.color = (n.isWalkAble) ? Color.white : Color.red;

                //탐색된 path의 노드표시
                if (path != null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
}

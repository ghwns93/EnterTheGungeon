using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    PathRequestManager requestManager;
    AGrid grid;

    private void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<AGrid>();
    }

    //PathRequestManager에서의 현재 길찾기 요청을 시작하는 함수
    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        ANode startNode = grid.GetNodeFromWorldPoint(startPos);
        ANode targetNode = grid.GetNodeFromWorldPoint(targetPos);

        if (startNode.isWalkAble && targetNode.isWalkAble)
        {
            List<ANode> openList = new List<ANode>();
            HashSet<ANode> closedList = new HashSet<ANode>();
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                ANode currentNode = openList[0];

                // 열린목록에 F cost 가 가장 작은 노드를 찾는다. 만약에 F cost가 같다면 H cost가 작은 노드를 찾는다.
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].fCost < currentNode.fCost ||
                        (openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost))
                    {
                        currentNode = openList[i];
                    }
                }
                //탐색된 노드는 열린목록에서 제거하고 끝난목록에 추가한다.
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                //탐색된 노드가 목표 노드라면 탐색 종료
                if (currentNode == targetNode)
                {
                    pathSuccess = true;

                    break;
                }

                //계속탐색(이웃 노드)
                foreach (ANode n in grid.GetNeighbours(currentNode))
                {
                    //이동불가 노드이거나 끝난목록에 있는 경우는 스킵
                    if (!n.isWalkAble || closedList.Contains(n)) continue;

                    //이웃 노드들의 G cost와 H cost를 계산하여 열린목록에 추가한다.
                    int newCurrentToNeighbourCost = currentNode.gCost + GetDistanceCost(currentNode, n);
                    if (newCurrentToNeighbourCost < n.gCost || !openList.Contains(n))
                    {
                        n.gCost = newCurrentToNeighbourCost;
                        n.hCost = GetDistanceCost(n, targetNode);
                        n.parentNode = currentNode;

                        if (!openList.Contains(n)) openList.Add(n);
                    }
                }
            }
        }
        yield return null;
        if(pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }

        //노드들의 WorldPosition을 담은 waypoints와 성공여부를 매니저함수에게 알려준다
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);

    }

    //탐색종료 후 최종 노드의 ParentNode를 추적하며 리스트에 담는다.
    //최종 경로에 있는 노드들의 WorldPosition을 순차적으로 담아 리턴
    Vector3[] RetracePath(ANode startNode, ANode endNode) 
    {
        List<ANode> path = new List<ANode>();
        ANode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }

        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    //Path 리스트에 있는 노드들의 WorldPosition을 Vector3[] 배열에 담아 리턴
    Vector3[] SimplifyPath(List<ANode> path) 
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for(int i = 1; i < path.Count; i++) 
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if(directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPos);
            }
            directionOld = directionNew;
        }

        return waypoints.ToArray();
    }

    //두 노드간의 거리로 Cost를 계산
    int GetDistanceCost(ANode nodeA, ANode nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if(distX > distY) 
            return 14 * distY + 10 * (distX - distY);

        return 14 * distX + 10 * (distY - distX);
    }
}

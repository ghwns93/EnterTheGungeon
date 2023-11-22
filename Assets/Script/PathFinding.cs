using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    AGrid grid;

    public Transform StartObject;
    public Transform TargetObject;

    private void Awake()
    {
        grid = GetComponent<AGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StartObject != null && TargetObject != null)
            FindPath(StartObject.position, TargetObject.position);
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        ANode startNode = grid.GetNodeFromWorldPoint(startPos);
        ANode targetNode = grid.GetNodeFromWorldPoint(targetPos);

        List<ANode> openList = new List<ANode>();
        HashSet<ANode> closedList = new HashSet<ANode>();
        openList.Add(startNode);

        while(openList.Count > 0) 
        {
            ANode currentNode = openList[0];

            // 열린목록에 F cost 가 가장 작은 노드를 찾는다. 만약에 F cost가 같다면 H cost가 작은 노드를 찾는다.
            for(int i = 1; i < openList.Count; i++) 
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
            if(currentNode == targetNode) 
            {
                RetracePath(startNode, targetNode);
                return;
            }

            //계속탐색(이웃 노드)
            foreach(ANode n in grid.GetNeighbours(currentNode))
            {
                //이동불가 노드이거나 끝난목록에 있는 경우는 스킵
                if (!n.isWalkAble || closedList.Contains(n)) continue;

                //이웃 노드들의 G cost와 H cost를 계산하여 열린목록에 추가한다.
                int newCurrentToNeighbourCost = currentNode.gCost + GetDistanceCost(currentNode, n);
                if(newCurrentToNeighbourCost < n.gCost || !openList.Contains(n))
                {
                    n.gCost = newCurrentToNeighbourCost;
                    n.hCost = GetDistanceCost(n, targetNode);
                    n.parentNode = currentNode;

                    if (!openList.Contains(n)) openList.Add(n);
                }
            }
        }
    }

    //탐색종료 후 최종 노드의 ParentNode를 추적하며 리스트에 담는다.
    void RetracePath(ANode startNode, ANode endNode)
    {
        List<ANode> path = new List<ANode>();
        ANode currentNode = endNode;

        int i = 0;

        while (currentNode != startNode) 
        {
            Debug.Log("i = " + i);
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
            i++;
        }

        path.Reverse();
        grid.path = path; //grid에 순차정렬된 path리스트를 담는다.
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

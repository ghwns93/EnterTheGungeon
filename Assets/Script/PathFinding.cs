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

            // ������Ͽ� F cost �� ���� ���� ��带 ã�´�. ���࿡ F cost�� ���ٸ� H cost�� ���� ��带 ã�´�.
            for(int i = 1; i < openList.Count; i++) 
            {
                if (openList[i].fCost < currentNode.fCost || 
                    (openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost))
                {
                    currentNode = openList[i];
                }
            }
            //Ž���� ���� ������Ͽ��� �����ϰ� ������Ͽ� �߰��Ѵ�.
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //Ž���� ��尡 ��ǥ ����� Ž�� ����
            if(currentNode == targetNode) 
            {
                RetracePath(startNode, targetNode);
                return;
            }

            //���Ž��(�̿� ���)
            foreach(ANode n in grid.GetNeighbours(currentNode))
            {
                //�̵��Ұ� ����̰ų� ������Ͽ� �ִ� ���� ��ŵ
                if (!n.isWalkAble || closedList.Contains(n)) continue;

                //�̿� ������ G cost�� H cost�� ����Ͽ� ������Ͽ� �߰��Ѵ�.
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

    //Ž������ �� ���� ����� ParentNode�� �����ϸ� ����Ʈ�� ��´�.
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
        grid.path = path; //grid�� �������ĵ� path����Ʈ�� ��´�.
    }

    //�� ��尣�� �Ÿ��� Cost�� ���
    int GetDistanceCost(ANode nodeA, ANode nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if(distX > distY) 
            return 14 * distY + 10 * (distX - distY);

        return 14 * distX + 10 * (distY - distX);
    }
}

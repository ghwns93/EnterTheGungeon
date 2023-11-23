using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PathRequestManager : MonoBehaviour
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>(); // 여러 오브젝트들의 길찾기 요청을 순서에 따라 큐에 담음
    PathRequest currentPathRequest; //현재 처리할 길찾기 요청

    static PathRequestManager instance;
    PathFinding pathFinding;

    bool isProcessingPath;

    void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }

    //오브젝트들이 요청하는 함수
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, UnityAction<Vector3[], bool> callback)
    {
        Debug.Log("1");
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    //큐순서대로 길찾기 요청을 꺼내서 PathFinding 알고리즘 시작함수
    void TryProcessNext()
    {
        Debug.Log("2");

        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathFinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    //길찾기가 완료된 요청을 처리하고 오브젝트에게 이동시작명령 콜백함수를 실행하는 함수
    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        Debug.Log("9");

        currentPathRequest.callback(path, success);
        isProcessingPath=false;
        TryProcessNext();
    }
}

// 오브젝트들의 길찾기 요청정보를 담을 구조체
struct PathRequest
{
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public UnityAction<Vector3[], bool> callback;

    public PathRequest(Vector3 nStart, Vector3 nEnd, UnityAction<Vector3[], bool> nCallback)
    {
        pathStart = nStart;
        pathEnd = nEnd;
        callback = nCallback;
    }
}
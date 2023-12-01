using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    Transform target;
    float speed = 1;
    Vector3[] path;
    int targetIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void StartPathFind()
    {
        Debug.Log("transform.position : " + transform.position);
        Debug.Log("transform.position : " + target.position);

        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void StopRoutine()
    {
        StopCoroutine("FollowPath");
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuceessful)
    {
        if(pathSuceessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length > 0)
        {
            Vector3 currenWayPoint = path[0];

            while (true)
            {
                if (transform.position == currenWayPoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currenWayPoint = path[targetIndex];
                }

                transform.position = Vector2.MoveTowards(transform.position, currenWayPoint, speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}

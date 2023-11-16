using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 11/16 수정중
        //Cursor.lockState = CursorLockMode.Locked;
        //GameObject player = GameObject.FindGameObjectWithTag("Player");

        //if (player != null)
        //{
        //    Vector2 pos = Vector2.Lerp(player.transform.position, Input.mousePosition, 0.5f);
        //    // 플레이어의 좌표와 연동
        //    transform.position = new Vector3(pos.x, pos.y, -10);
        //}
    }
}

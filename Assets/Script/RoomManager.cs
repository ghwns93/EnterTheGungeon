using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static int doorNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 출입구 정보를 배열로 전달받기
        GameObject[] enters = GameObject.FindGameObjectsWithTag("Exit");
        for (int i = 0; i < enters.Length; i++)
        {
            GameObject doorObj = enters[i];     // 배열의 n번째 요소를 전달받아
            Exit exit = doorObj.GetComponent<Exit>();   // 해당 출입구의 Exit 클래스 정보 획득
            if (doorNumber == exit.doorNumber)
            {
                // 플레이어 캐릭터를 출입구로 이동
                float x = doorObj.transform.position.x;
                float y = doorObj.transform.position.y;

                if (exit.direction == ExitDirection.up)
                    y += 1;
                else if (exit.direction == ExitDirection.right)
                    x += 1;
                else if (exit.direction == ExitDirection.down)
                    y -= 1;
                else if (exit.direction == ExitDirection.left)
                    x -= 1;

                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = new Vector3(x, y);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Scene 이동
    public static void ChangeScene(string sceneName, int doorNum)
    {
            doorNumber = doorNum;   // 문 번호를 static 변수에 저장

            SceneManager.LoadScene(sceneName);  // 이동 실시
    }
}

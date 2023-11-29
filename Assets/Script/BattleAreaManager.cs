using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class BattleAreaManager : MonoBehaviour
{
    [SerializeField] private Animator _door = null;
    public static bool enemyCheck = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && DoorManager.isOpening)
        {
            enemyCheck = true;
            CloseDoor();
            Debug.Log("적이 있어서 지나갈수 읎다");

            if (collision.gameObject.tag != "Enemy" || collision.gameObject == null)
            {
                enemyCheck = false;
                Debug.Log("적이 읎어졌누");
            }
        }

        if (collision.gameObject.tag != "Enemy" && !enemyCheck)
        {
            if(!DoorManager.isOpening)
            {
                OpenDoor();
                DoorManager.firstOpen = false;
                Debug.Log("적없으니 열립니당.");
            }
        }
    }

    // 문을 닫기 위한 함수
    private void CloseDoor()
    {
        if (DoorManager.isOpening) // 이미 열려있는 경우에만 닫기
        {
             Debug.Log("문이 닫힙니다.");
            _door.Play("UD_Close");
            DoorManager.isOpening = false;
        }
    }

    // 문을 열기 위한 함수
    private void OpenDoor()
    {
        if (!DoorManager.isOpening) // 이미 열려있지 않은 경우에만 열기
        {
             Debug.Log("문을 여시오~");
            _door.Play("UD_Open");
            DoorManager.isOpening = true;
        }
    }
}



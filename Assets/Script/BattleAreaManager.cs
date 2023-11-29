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
            Debug.Log("���� �־ �������� ����");

            if (collision.gameObject.tag != "Enemy" || collision.gameObject == null)
            {
                enemyCheck = false;
                Debug.Log("���� ��������");
            }
        }

        if (collision.gameObject.tag != "Enemy" && !enemyCheck)
        {
            if(!DoorManager.isOpening)
            {
                OpenDoor();
                DoorManager.firstOpen = false;
                Debug.Log("�������� �����ϴ�.");
            }
        }
    }

    // ���� �ݱ� ���� �Լ�
    private void CloseDoor()
    {
        if (DoorManager.isOpening) // �̹� �����ִ� ��쿡�� �ݱ�
        {
             Debug.Log("���� �����ϴ�.");
            _door.Play("UD_Close");
            DoorManager.isOpening = false;
        }
    }

    // ���� ���� ���� �Լ�
    private void OpenDoor()
    {
        if (!DoorManager.isOpening) // �̹� �������� ���� ��쿡�� ����
        {
             Debug.Log("���� ���ÿ�~");
            _door.Play("UD_Open");
            DoorManager.isOpening = true;
        }
    }
}



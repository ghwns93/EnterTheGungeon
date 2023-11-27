using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public Animator ulDoorAnimator;
    public Animator urDoorAnimator;

    private bool doorsOpen = false;

    void Start()
    {
        ulDoorAnimator = transform.Find("ULDoor").GetComponent<Animator>();
        urDoorAnimator = transform.Find("URDoor").GetComponent<Animator>();
            Debug.Log("0");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && doorsOpen == false)
        {
            OpenDoors();

            Debug.Log("1");
        }
    }

    void OpenDoors()
    {
        ulDoorAnimator.SetTrigger("ULD_Open");
        urDoorAnimator.SetTrigger("URD_Open");
            Debug.Log("2");

        // �浹 üũ�� �����ϵ��� Collider�� ��Ȱ��ȭ
        GetComponent<BoxCollider2D>().enabled = false;

        doorsOpen = true;
            Debug.Log("3");

    }
}
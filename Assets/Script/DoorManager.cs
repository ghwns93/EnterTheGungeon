using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerInside = false;

    public string openAnimationName; // ���� ���� �ִϸ��̼� �̸�
    public string closeAnimationName; // ���� ���� �ִϸ��̼� �̸�

    void Start()
    {
        Debug.Log("�ִϸ����� �غ����");
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isPlayerInside)
        {
            Debug.Log("�� ���� �Ǵ�");
            isPlayerInside = true;
            animator.Play(openAnimationName);
        }
    }

    public void CloseDoor()
    {
        if (isPlayerInside)
        {
            Debug.Log("�� �ݴ� �޼���");
            animator.Play(closeAnimationName);
        }
    }

    public void OpenDoor()
    {
        if (isPlayerInside)
        {
            Debug.Log("�� ���� �޼���");
            animator.Play(openAnimationName);
        }
    }
}
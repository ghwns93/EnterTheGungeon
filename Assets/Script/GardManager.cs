using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardManager : MonoBehaviour
{
    private Animator animator;

    public string openGardName; // ���� ���� �ִϸ��̼� �̸�
    public string closeGardName; // ���� ���� �ִϸ��̼� �̸�

    void Start()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
        Debug.Log("���� ���� ������");
    }

    public void CloseGard()
    {
            gameObject.SetActive(true);
        Debug.Log("���� ���� ������");
            animator.Play(closeGardName);
    }

    public void OpenGard()
    {
            animator.Play(openGardName);
            gameObject.SetActive(false);
    }
}

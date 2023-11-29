using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerInside = false;

    public string openAnimationName; // 열릴 때의 애니메이션 이름
    public string closeAnimationName; // 닫힐 때의 애니메이션 이름

    void Start()
    {
        Debug.Log("애니메이터 준비상태");
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isPlayerInside)
        {
            Debug.Log("문 열림 판단");
            isPlayerInside = true;
            animator.Play(openAnimationName);
        }
    }

    public void CloseDoor()
    {
        if (isPlayerInside)
        {
            Debug.Log("문 닫는 메서드");
            animator.Play(closeAnimationName);
        }
    }

    public void OpenDoor()
    {
        if (isPlayerInside)
        {
            Debug.Log("문 여는 메서드");
            animator.Play(openAnimationName);
        }
    }
}
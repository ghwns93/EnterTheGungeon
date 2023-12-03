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
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isPlayerInside)
        {
            isPlayerInside = true;
            animator.Play(openAnimationName);
        }
    }

    public void CloseDoor()
    {
        if (isPlayerInside)
        {
            animator.Play(closeAnimationName);
        }
    }

    public void OpenDoor()
    {
        if (isPlayerInside)
        {
            animator.Play(openAnimationName);
        }
    }
}

/*
// 게임에서 문과 도어 가드를 관리합니다.
public class DoorManager : MonoBehaviour
{
    public Animator doorAnimator;
    public Animator guardAnimator; // 도어 가드의 애니메이터
    bool isDoorOpen = false;
    bool isGuardActive = false; // 도어 가드의 활성 상태
    public string openDoorName; // 열릴 때의 애니메이션 이름
    public string openGardName; // 열릴 때의 애니메이션 이름
    public string closeDoorName; // 닫힐 때의 애니메이션 이름
    public string closeGardName; // 닫힐 때의 애니메이션 이름

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
        // 가드 애니메이터를 가져옴. 도어 가드 오브젝트의 애니메이터 컴포넌트를 연결해야 함.
        //guardAnimator = transform.Find("Gard").GetComponent<Animator>();

        // 문이 시작할 때 닫혀 있는 상태를 보장
        doorAnimator.Play(closeDoorName);

    }
        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDoorOpen)
        {
            OpenDoor();
            if (isGuardActive) // 도어 가드가 활성화되어 있다면, 내려가는 애니메이션을 중단
            {
                LowerGuard(false);
            }
        }
    }

    public void OpenDoor()
    {
        doorAnimator.Play(openDoorName);
        isDoorOpen = true;
    }

    public void CloseDoor()
    {
        if (isDoorOpen)
        {
            doorAnimator.Play(closeDoorName);
            isDoorOpen = false;
            if (!isGuardActive) // 문이 닫히면 도어 가드를 활성화
            {
                LowerGuard(true);
            }
        }
    }

    // 도어 가드를 내리거나 올리는 메서드
    void LowerGuard(bool lower)
    {
        if (lower)
        {
            guardAnimator.Play(closeGardName);
            isGuardActive = true;
        }
        else
        {
            guardAnimator.Play(openGardName);
            isGuardActive = false;
        }
    }
}

 */
/*

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
        //Debug.Log("애니메이터 준비상태");
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isPlayerInside)
        {
            //Debug.Log("문 열림 판단");
            isPlayerInside = true;
            animator.Play(openAnimationName);
        }
    }

    public void CloseDoor()
    {
        if (isPlayerInside)
        {
            //Debug.Log("문 닫는 메서드");
            animator.Play(closeAnimationName);
        }
    }

    public void OpenDoor()
    {
        if (isPlayerInside)
        {
            //Debug.Log("문 여는 메서드");
            animator.Play(openAnimationName);
        }
    }
}
 */
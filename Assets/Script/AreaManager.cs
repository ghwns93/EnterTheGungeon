using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AreaManager : MonoBehaviour
{
    public List<DoorManager> doorManagers; // 문 매니저들을 리스트로 관리합니다.
    public Animator UDGardAnimator; // 위 문 가드의 애니메이터
    public Animator DDGardAnimator; // 아래 문 가드의 애니메이터
    public Animator LTGardAnimator; // 왼쪽 문 가드의 애니메이터
    public Animator RTGardAnimator; // 오른쪽 문 가드의 애니메이터

    void Start()
    {
        // 게임 시작 시 모든 가드를 비활성화합니다.
        UDGardAnimator.gameObject.SetActive(false);
        DDGardAnimator.gameObject.SetActive(false);
        LTGardAnimator.gameObject.SetActive(false);
        RTGardAnimator.gameObject.SetActive(false);
    }

    private void CloseAllGuards()
    {
        // 각 가드를 활성화하고 'Close' 애니메이션을 재생합니다.
        UDGardAnimator.gameObject.SetActive(true);
        UDGardAnimator.Play("UDGard_Down");

        DDGardAnimator.gameObject.SetActive(true);
        DDGardAnimator.Play("DDGard_Down");

        LTGardAnimator.gameObject.SetActive(true);
        LTGardAnimator.Play("LTGard_Down");

        RTGardAnimator.gameObject.SetActive(true);
        RTGardAnimator.Play("RTGard_Down");
    }

    private void OpenAllGuards()
    {
        // 'Up' 애니메이션 재생 후 가드를 비활성화합니다.
        StartCoroutine(DeactivateGuardAfterAnimation(UDGardAnimator, "UDGard_Up"));
        StartCoroutine(DeactivateGuardAfterAnimation(DDGardAnimator, "DDGard_Up"));
        StartCoroutine(DeactivateGuardAfterAnimation(LTGardAnimator, "LTGard_Up"));
        StartCoroutine(DeactivateGuardAfterAnimation(RTGardAnimator, "RTGard_Up"));
    }

    private IEnumerator DeactivateGuardAfterAnimation(Animator animator, string animationName)
    {
        // 애니메이션 재생
        animator.Play(animationName);

        // 애니메이션이 끝날 때까지 기다립니다.
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // 애니메이션이 끝나면 해당 가드를 비활성화합니다.
        animator.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // BattleArea 내의 모든 콜라이더들을 검사합니다.
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0);

            foreach (var collider in colliders)
            {
                // "Enemy" 태그인 오브젝트가 하나라도 있으면 문을 닫습니다.
                if (collider.gameObject.tag == "Enemy")
                {
                    //Debug.Log("닫힘 체크");
                    foreach (var doorManager in doorManagers)
                    {
                        doorManager.CloseDoor();
                    }
                    CloseAllGuards(); // 모든 가드를 활성화합니다.
                    break; // Enemy를 찾았으니 더 이상 반복할 필요가 없습니다.
                }
            }
        }
    }

    void Update()
    {
        // BattleArea 내에 Enemy 태그를 가진 오브젝트가 없다면 문을 엽니다.
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            //Debug.Log("열림 체크");
            foreach (var doorManager in doorManagers)
            {
                doorManager.OpenDoor();
            }
            OpenAllGuards(); // 모든 가드를 비활성화합니다.
        }
    }
}
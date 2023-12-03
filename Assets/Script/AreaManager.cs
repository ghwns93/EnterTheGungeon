using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Linq를 사용하기 위해 추가

public class AreaManager : MonoBehaviour
{
    public List<DoorManager> doorManagers; // 문 매니저 리스트
    public List<GardManager> gardManagers; // 가드 매니저 리스트
    public GameObject roomEnterManager; // 에디터에서 설정

    private bool isEnemyPresent;

    void Update()
    {
        // 현재 Enemy 태그를 가진 자식 개체가 있는지 확인
        bool enemyCurrentlyPresent = roomEnterManager.transform.Cast<Transform>().Any(child => child.tag == "Enemy");

        // Enemy 태그를 가진 개체가 사라졌다면 문을 열고 가드를 비활성화
        if (isEnemyPresent && !enemyCurrentlyPresent)
        {
            Debug.Log("열림 체크");
            foreach (var doorManager in doorManagers)
            {
                doorManager.OpenDoor();
            }
            foreach (var gardManager in gardManagers)
            {
                gardManager.OpenGard();
            }
        }

        // Enemy 태그를 가진 개체의 현재 상태를 추적
        isEnemyPresent = enemyCurrentlyPresent;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // RoomEnterManager 오브젝트 안에서 Enemy 태그를 가진 자식 개체가 하나라도 있는지 확인
            isEnemyPresent = roomEnterManager.transform.Cast<Transform>().Any(child => child.tag == "Enemy");

            // Enemy 태그를 가진 자식 개체가 하나라도 있으면 문을 닫고 가드를 작동시킴
            if (isEnemyPresent)
            {
                Debug.Log("닫힘 체크");
                foreach (var doorManager in doorManagers)
                {
                    doorManager.CloseDoor();
                }
                foreach (var gardManager in gardManagers)
                {
                    gardManager.CloseGard();
                }
            }
        }
    }
}

/*
public class AreaManager : MonoBehaviour
{
    public List<DoorManager> doorManagers; // 문 매니저들을 리스트로 관리합니다.
    public List<GardManager> gardManagers; // 문 매니저들을 리스트로 관리합니다.

    void Start()
    {
    }

    private void CloseAllGuards()
    {
        // 각 가드를 활성화하고 'Close' 애니메이션을 재생합니다.
        r.gameObject.SetActive(true);
        UDGardAnimator.Play("UDGard_Close");

        DDGardAnimator.gameObject.SetActive(true);
        DDGardAnimator.Play("DDGard_Close");

        LTGardAnimator.gameObject.SetActive(true);
        LTGardAnimator.Play("LTGard_Close");

        RTGardAnimator.gameObject.SetActive(true);
        RTGardAnimator.Play("RTGard_Close");
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
                    Debug.Log("닫힘 체크");
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
            Debug.Log("열림 체크");
            foreach (var doorManager in doorManagers)
            {
                doorManager.OpenDoor();
            }
            OpenAllGuards(); // 모든 가드를 비활성화합니다.
        }
    }
}
 */
/*
using UnityEngine;
// 게임에서 전투 영역을 관리합니다.
public class AreaManager : MonoBehaviour
{
    DoorManager doorManager;
    GameObject[] enemies;
    GameObject[] doors;

    void Start()
    {
        doorManager = FindObjectOfType<DoorManager>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        doors = GameObject.FindGameObjectsWithTag("Door");
    }

    // 영역 내의 적을 확인하고 적이 있으면 문을 닫습니다.
    void Update()
    {
        CheckForEnemies();
    }

    void CheckForEnemies()
    {
        bool enemiesPresent = false;
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemiesPresent = true;
                break;
            }
        }

        foreach (var door in doors)
        {
            DoorManager dm = door.GetComponent<DoorManager>();
            if (enemiesPresent)
            {
                dm.CloseDoor();
            }
            else
            {
                dm.OpenDoor();
            }
        }
    }
}
 */

/*
 



 */
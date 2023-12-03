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

/*
 

using System.Collections;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    private Animator[] guardAnimators; // 가드 애니메이터들의 배열입니다.
    private DoorManager[] doorManagers; // 문 매니저들의 배열입니다.

    private bool isInitialized = false;

    private void InitializeArea()
    {
        // 초기화 로직을 여기에 구현합니다.
        isInitialized = true;
        SetGuardsActive(false); // 초기화 시 모든 가드를 비활성화합니다.
    }

    void Start()
    {
        // 자식 오브젝트들에서 문 매니저와 가드 애니메이터들을 찾습니다.
        doorManagers = GetComponentsInChildren<DoorManager>(true);
        guardAnimators = GetComponentsInChildren<Animator>(true);

        InitializeArea(); // 영역을 초기화합니다.
    }

    private void SetGuardsActive(bool isActive)
    {
        // 모든 가드 애니메이터를 주어진 활성화 상태로 설정합니다.
        foreach (var animator in guardAnimators)
        {
            animator.gameObject.SetActive(isActive);
        }
    }

    private void CloseAllGuards()
    {
        // 각 가드 애니메이터를 활성화하고 'Close' 애니메이션을 재생합니다.
        foreach (var animator in guardAnimators)
        {
            animator.gameObject.SetActive(true);
            animator.Play("Gard_Down"); // 모든 가드에 대한 일반화된 애니메이션 이름을 사용합니다.
        }
    }

    private void OpenAllGuards()
    {
        // 'Up' 애니메이션 재생 후 각 가드 애니메이터를 비활성화합니다.
        foreach (var animator in guardAnimators)
        {
            StartCoroutine(DeactivateGuardAfterAnimation(animator, "Gard_Up"));
        }
    }

    private IEnumerator DeactivateGuardAfterAnimation(Animator animator, string animationName)
    {
        // 애니메이션을 재생합니다.
        animator.Play(animationName);

        // 애니메이션이 끝날 때까지 기다립니다.
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // 애니메이션이 끝나면 가드 애니메이터를 비활성화합니다.
        animator.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isInitialized)
            {
                InitializeArea(); // 영역을 초기화합니다.
            }

            // BattleArea 내의 모든 콜라이더들을 검사합니다.
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0);

            foreach (var collider in colliders)
            {
                // "Enemy" 태그인 오브젝트가 하나라도 있으면 문을 닫습니다.
                if (collider.gameObject.tag == "Enemy")
                {
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // BattleArea를 떠날 때 영역을 초기화합니다.
            isInitialized = false;
            SetGuardsActive(false); // 모든 가드를 비활성화합니다.
        }
    }

    void Update()
    {
        // BattleArea 내에 Enemy 태그를 가진 오브젝트가 없다면 문을 엽니다.
        if (isInitialized)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0);
            bool enemyFound = false;

            foreach (var collider in colliders)
            {
                if (collider.gameObject.tag == "Enemy")
                {
                    enemyFound = true;
                    break; // Enemy를 찾았으니 더 이상 반복할 필요가 없습니다.
                }
            }

            if (!enemyFound)
            {
                foreach (var doorManager in doorManagers)
                {
                    doorManager.OpenDoor();
                }
                OpenAllGuards(); // 모든 가드를 비활성화합니다.
            }
        }
    }
}
 */
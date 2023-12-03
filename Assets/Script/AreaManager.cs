using UnityEngine;

// ���ӿ��� ���� ������ �����մϴ�.
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

    // ���� ���� ���� Ȯ���ϰ� ���� ������ ���� �ݽ��ϴ�.
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
    private Animator[] guardAnimators; // ���� �ִϸ����͵��� �迭�Դϴ�.
    private DoorManager[] doorManagers; // �� �Ŵ������� �迭�Դϴ�.

    private bool isInitialized = false;

    private void InitializeArea()
    {
        // �ʱ�ȭ ������ ���⿡ �����մϴ�.
        isInitialized = true;
        SetGuardsActive(false); // �ʱ�ȭ �� ��� ���带 ��Ȱ��ȭ�մϴ�.
    }

    void Start()
    {
        // �ڽ� ������Ʈ�鿡�� �� �Ŵ����� ���� �ִϸ����͵��� ã���ϴ�.
        doorManagers = GetComponentsInChildren<DoorManager>(true);
        guardAnimators = GetComponentsInChildren<Animator>(true);

        InitializeArea(); // ������ �ʱ�ȭ�մϴ�.
    }

    private void SetGuardsActive(bool isActive)
    {
        // ��� ���� �ִϸ����͸� �־��� Ȱ��ȭ ���·� �����մϴ�.
        foreach (var animator in guardAnimators)
        {
            animator.gameObject.SetActive(isActive);
        }
    }

    private void CloseAllGuards()
    {
        // �� ���� �ִϸ����͸� Ȱ��ȭ�ϰ� 'Close' �ִϸ��̼��� ����մϴ�.
        foreach (var animator in guardAnimators)
        {
            animator.gameObject.SetActive(true);
            animator.Play("Gard_Down"); // ��� ���忡 ���� �Ϲ�ȭ�� �ִϸ��̼� �̸��� ����մϴ�.
        }
    }

    private void OpenAllGuards()
    {
        // 'Up' �ִϸ��̼� ��� �� �� ���� �ִϸ����͸� ��Ȱ��ȭ�մϴ�.
        foreach (var animator in guardAnimators)
        {
            StartCoroutine(DeactivateGuardAfterAnimation(animator, "Gard_Up"));
        }
    }

    private IEnumerator DeactivateGuardAfterAnimation(Animator animator, string animationName)
    {
        // �ִϸ��̼��� ����մϴ�.
        animator.Play(animationName);

        // �ִϸ��̼��� ���� ������ ��ٸ��ϴ�.
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // �ִϸ��̼��� ������ ���� �ִϸ����͸� ��Ȱ��ȭ�մϴ�.
        animator.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isInitialized)
            {
                InitializeArea(); // ������ �ʱ�ȭ�մϴ�.
            }

            // BattleArea ���� ��� �ݶ��̴����� �˻��մϴ�.
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0);

            foreach (var collider in colliders)
            {
                // "Enemy" �±��� ������Ʈ�� �ϳ��� ������ ���� �ݽ��ϴ�.
                if (collider.gameObject.tag == "Enemy")
                {
                    foreach (var doorManager in doorManagers)
                    {
                        doorManager.CloseDoor();
                    }
                    CloseAllGuards(); // ��� ���带 Ȱ��ȭ�մϴ�.
                    break; // Enemy�� ã������ �� �̻� �ݺ��� �ʿ䰡 �����ϴ�.
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // BattleArea�� ���� �� ������ �ʱ�ȭ�մϴ�.
            isInitialized = false;
            SetGuardsActive(false); // ��� ���带 ��Ȱ��ȭ�մϴ�.
        }
    }

    void Update()
    {
        // BattleArea ���� Enemy �±׸� ���� ������Ʈ�� ���ٸ� ���� ���ϴ�.
        if (isInitialized)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0);
            bool enemyFound = false;

            foreach (var collider in colliders)
            {
                if (collider.gameObject.tag == "Enemy")
                {
                    enemyFound = true;
                    break; // Enemy�� ã������ �� �̻� �ݺ��� �ʿ䰡 �����ϴ�.
                }
            }

            if (!enemyFound)
            {
                foreach (var doorManager in doorManagers)
                {
                    doorManager.OpenDoor();
                }
                OpenAllGuards(); // ��� ���带 ��Ȱ��ȭ�մϴ�.
            }
        }
    }
}
 */
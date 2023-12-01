using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AreaManager : MonoBehaviour
{
    public List<DoorManager> doorManagers; // �� �Ŵ������� ����Ʈ�� �����մϴ�.
    public Animator UDGardAnimator; // �� �� ������ �ִϸ�����
    public Animator DDGardAnimator; // �Ʒ� �� ������ �ִϸ�����
    public Animator LTGardAnimator; // ���� �� ������ �ִϸ�����
    public Animator RTGardAnimator; // ������ �� ������ �ִϸ�����

    void Start()
    {
        // ���� ���� �� ��� ���带 ��Ȱ��ȭ�մϴ�.
        UDGardAnimator.gameObject.SetActive(false);
        DDGardAnimator.gameObject.SetActive(false);
        LTGardAnimator.gameObject.SetActive(false);
        RTGardAnimator.gameObject.SetActive(false);
    }

    private void CloseAllGuards()
    {
        // �� ���带 Ȱ��ȭ�ϰ� 'Close' �ִϸ��̼��� ����մϴ�.
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
        // 'Up' �ִϸ��̼� ��� �� ���带 ��Ȱ��ȭ�մϴ�.
        StartCoroutine(DeactivateGuardAfterAnimation(UDGardAnimator, "UDGard_Up"));
        StartCoroutine(DeactivateGuardAfterAnimation(DDGardAnimator, "DDGard_Up"));
        StartCoroutine(DeactivateGuardAfterAnimation(LTGardAnimator, "LTGard_Up"));
        StartCoroutine(DeactivateGuardAfterAnimation(RTGardAnimator, "RTGard_Up"));
    }

    private IEnumerator DeactivateGuardAfterAnimation(Animator animator, string animationName)
    {
        // �ִϸ��̼� ���
        animator.Play(animationName);

        // �ִϸ��̼��� ���� ������ ��ٸ��ϴ�.
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // �ִϸ��̼��� ������ �ش� ���带 ��Ȱ��ȭ�մϴ�.
        animator.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // BattleArea ���� ��� �ݶ��̴����� �˻��մϴ�.
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0);

            foreach (var collider in colliders)
            {
                // "Enemy" �±��� ������Ʈ�� �ϳ��� ������ ���� �ݽ��ϴ�.
                if (collider.gameObject.tag == "Enemy")
                {
                    //Debug.Log("���� üũ");
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

    void Update()
    {
        // BattleArea ���� Enemy �±׸� ���� ������Ʈ�� ���ٸ� ���� ���ϴ�.
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            //Debug.Log("���� üũ");
            foreach (var doorManager in doorManagers)
            {
                doorManager.OpenDoor();
            }
            OpenAllGuards(); // ��� ���带 ��Ȱ��ȭ�մϴ�.
        }
    }
}
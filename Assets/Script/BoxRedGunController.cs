using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRedGunController : MonoBehaviour
{

    // �� �ִϸ�����
    private Animator gunAnimator;

    // �ִϸ��̼�
    private string redGunHold = "RedGunHold";
    private string redGunFire = "RedGunFire";
    private string redGunReload = "RedGunReload";


    // �÷��̾��� �� �ִϸ�����
    private Animator PlayerGunAnimator;

    private int redGunBulletCount;           //�Ѿ� ����

    private bool canAttack;     //���� ������ �Ҷ� ���
    private bool isReloading;  //�����ϴ��� ���� �ȵǰ�
    private bool isAttack;     //���ݴ����������� �����ȵǰ�
    private bool inlobby;              //PlayerController�� ���� �����ͼ� ������ ���� //�ϴ� false


    // Start is called before the first frame update
    void Start()
    {
        gunAnimator = GetComponent<Animator>();

        // �⺻ �ִϸ��̼� ����
        gunAnimator.Play(redGunHold);

        redGunBulletCount = 20;
        canAttack = true;
        isReloading = false;
        isAttack = false;
        inlobby = false;
    }

    // Update is called once per frame
    void Update()
    {
        // gunNumber ���� ��������
        GameObject player = GameObject.FindWithTag("Player");
        int gunNumber = player.GetComponent<GunController>().gunNumber;

        // �÷��̾� ���� �ѿ� ���� �ڽ� ui �� ���̰ų� �Ⱥ��̰�
        if (gunNumber == 2)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else 
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        // ���콺 ��Ŭ���� ����
        if (Input.GetMouseButton(0) && canAttack && !inlobby && redGunBulletCount > 0 && !isReloading)
        {
            isAttack = true;

            // ���� Ű �Է� �� ������ ����
            StartCoroutine(AttackWithDelay());
        }

        // ���콺 ��Ŭ���� ����
        if (Input.GetMouseButtonUp(0) && !inlobby && !isReloading)
        {
            gunAnimator.Play(redGunHold);
            isAttack = false;
        }

        // �Ѿ� �ٽ��� �� ���콺 ��Ŭ���� ����
        if (Input.GetMouseButtonDown(0) && canAttack && !inlobby && redGunBulletCount == 0)
        {
            isReloading = true;
            canAttack = false;
            gunAnimator.Play(redGunReload, 0, 0f);  // �ִϸ��̼� Ű����Ʈ ó������ �̵��� ����            
            Invoke("ChangeVariable", 1.3f);  //1.3�ʵ� isReloading=false ,canAttack =true�� �ٲٴ��Լ� ����
            redGunBulletCount = 20;
        }

        // RŰ������ ����
        if (Input.GetKeyDown(KeyCode.R) && !inlobby && !isReloading && redGunBulletCount < 20)
        {
            isReloading = true;
            canAttack = false;
            gunAnimator.Play(redGunReload, 0, 0f);  // �ִϸ��̼� Ű����Ʈ ó������ �̵��� ����
            Invoke("ChangeVariable", 1.3f);  //1.3�ʵ� isReloading=false ,canAttack =true�� �ٲٴ��Լ� ����
            redGunBulletCount = 20;
        }
    }

    IEnumerator AttackWithDelay()
    {
        // ���� ����
        Attack();
        // ������ ���� 
        canAttack = false;
        yield return new WaitForSeconds(0.2f);
        // ������ �Ŀ� �ٽ� ���� �������� ����
        canAttack = true;
    }
    private void Attack()
    {
        redGunBulletCount--;
        gunAnimator.Play(redGunFire, 0, 0f);  // �ִϸ��̼� Ű����Ʈ ó������ �̵��� ����
    }
    private void ChangeVariable()
    {
        isReloading = false;
        canAttack = true;
    }
}

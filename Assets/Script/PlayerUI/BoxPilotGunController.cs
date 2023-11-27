using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPilotGunController : MonoBehaviour
{
    // �� �ִϸ�����
    private Animator gunAnimator;

    // �ִϸ��̼�
    private string pilotGunHold = "PilotGunHold";
    private string BoxPilotGunFire = "BoxPilotGunFire";
    private string pilotGunReturn = "PilotGunReturn";
    private string pilotGunReload = "PilotGunReload";

    
    // �÷��̾��� �� �ִϸ�����
    private Animator PlayerGunAnimator;

    private int pilotGunBulletCount;           //�Ѿ� ����

    private bool canAttack;     //���� ������ �Ҷ� ���
    private bool isReloading;  //�����ϴ��� ���� �ȵǰ�
    private bool isAttack;     //���ݴ����������� �����ȵǰ�
    private bool inlobby;              //PlayerController�� ���� �����ͼ� ������ ���� //�ϴ� false


    // Start is called before the first frame update
    void Start()
    {
        pilotGunBulletCount = 8;

        gunAnimator = GetComponent<Animator>();
        
        // �⺻ �ִϸ��̼� ����
        gunAnimator.Play(pilotGunHold);

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
        if(gunNumber == 1)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else 
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        // ���콺 ��Ŭ���� ����
        if (Input.GetMouseButton(0) && canAttack && !inlobby && pilotGunBulletCount > 0 && !isReloading)
        {
            isAttack = true;

            // ���� Ű �Է� �� ������ ����
            StartCoroutine(AttackWithDelay());
        }

        // ���콺 ��Ŭ���� ����
        if (Input.GetMouseButtonUp(0) && !inlobby && !isReloading)
        {
            gunAnimator.Play(pilotGunReturn);
            isAttack = false;
        }

        // �Ѿ� �ٽ��� �� ���콺 ��Ŭ���� ����
        if (Input.GetMouseButtonDown(0) && canAttack && !inlobby && pilotGunBulletCount == 0)
        {
            isReloading = true;
            canAttack = false;
            gunAnimator.Play(pilotGunReload, 0, 0f);  // �ִϸ��̼� Ű����Ʈ ó������ �̵��� ����            
            Invoke("ChangeVariable", 1.3f);  //1.3�ʵ� isReloading=false ,canAttack =true�� �ٲٴ��Լ� ����
            pilotGunBulletCount = 8;
        }        

        // RŰ������ ����
        if (Input.GetKeyDown(KeyCode.R) && !inlobby && !isReloading && pilotGunBulletCount < 8)
        {
            isReloading = true;
            canAttack = false;            
            gunAnimator.Play(pilotGunReload, 0, 0f);  // �ִϸ��̼� Ű����Ʈ ó������ �̵��� ����
            Invoke("ChangeVariable", 1.3f);  //1.3�ʵ� isReloading=false ,canAttack =true�� �ٲٴ��Լ� ����
            pilotGunBulletCount = 8;
        }
    }

    IEnumerator AttackWithDelay()
    {
        // ���� ����
        Attack();
        // ������ ���� 
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        // ������ �Ŀ� �ٽ� ���� �������� ����
        canAttack = true;
    }
    private void Attack()
    {
        pilotGunBulletCount--;
        gunAnimator.Play(BoxPilotGunFire, 0, 0f);  // �ִϸ��̼� Ű����Ʈ ó������ �̵��� ����
    }
    private void ChangeVariable()
    {
        isReloading = false;
        canAttack = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : PlayerController
{

    // �ִϸ��̼�
    public string gunHoldAnime = "PilotGunHold";

    // ���� �ִϸ��̼�
    string nowGunAnimation = "";
    // ���� �ִϸ��̼�
    string oldGunAnimation = "";

    // �ִϸ�����
    private Animator gunanimator;


    // Start is called before the first frame update
    void Start()
    {
        // (�⺻) �ִϸ��̼� ����
        oldGunAnimation = gunHoldAnime;

        // �ִϸ����� ��������
        gunanimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // ����꿡 ������ ���¿��� �����ϼż� �ٸ������ ���尡 �ȵ�
        // �����ϼž� �մϴ�.


        //if(isDodging)
        //{

        //}

        //if (nowAnimation == walkRightDownAnime)                     //����, �����Ʒ�
        //{
            
        //}
        //else if (nowAnimation == walkRightUpAnime)                 // ������
        //{
            
        //}
        //else if (nowAnimation == walkUpAnime)                // ��
        //{
            
        //}
        //else if (nowAnimation == walkLeftUpAnime)               // ����
        //{
            
        //}
        //else if (nowAnimation == walkLeftDownAnime)   // ��, �޹�
        //{
            
        //}
        //else if (nowAnimation == walkDownAnime)              // �Ʒ�
        //{
            
        //}

        //if (nowAnimation == stopRightDownAnime)     //����, �����Ʒ�
        //{
            
        //}
        //else if (nowAnimation == stopRightUpAnime)         // ������
        //{
            
        //}
        //else if (nowAnimation == stopUpAnime)       // ��
        //{
            
        //}
        //else if (nowAnimation == stopLeftUpAnime)       // ����
        //{
            
        //}
        //else if (nowAnimation == stopLeftDownAnime) // ��, �޹�
        //{
            
        //}
        //else if (nowAnimation == stopDownAnime)     // �Ʒ�
        //{
            
        //}

        //// �������� �̵��� �� X�� �ø�
        //if (axisH < 0)
        //{
        //    // SpriteRenderer�� flipX�� ����ϴ� ���
        //    GetComponent<SpriteRenderer>().flipX = true;

        //    // Transform�� Rotation�� ����ϴ� ���
        //    //transform.rotation = Quaternion.Euler(0, 180, 0);
        //}
        //else if (axisH > 0) // ���������� �̵��� �� X�� �ø� ����
        //{
        //    // SpriteRenderer�� flipX�� ����ϴ� ���
        //    GetComponent<SpriteRenderer>().flipX = false;

        //    // Transform�� Rotation�� ����ϴ� ���
        //    //transform.rotation = Quaternion.Euler(0, 0, 0);
        //}

        //// �ִϸ��̼� ����
        //if (nowAnimation != oldAnimation)
        //{
        //    oldAnimation = nowAnimation;
        //    GetComponent<Animator>().Play(nowAnimation);
        //}

        //if ((Input.GetButtonDown("Fire2"))) // ���콺 ������ �Է½� ȸ��
        //{
            

        //}
    }

    // (����Ƽ �ʱ� ���� ����) 0.02�ʸ��� ȣ��Ǹ�, 1�ʿ� �� 50�� ȣ��Ǵ� �Լ�
    void FixedUpdate()
    {
        
    }


}

// Ű �Է� ���� �Լ� ���
/*
    // Ű������ Ư�� Ű �Է¿� ���� �˻�
    bool down = Input.GetKeyDown(KeyCode.Space);
    bool press = Input.GetKey(KeyCode.Space);
    bool up = Input.GetKeyUp(KeyCode.Space);

    // ���콺 ��ư �Է� �� ��ġ �̺�Ʈ�� ���� �˻�
    // 0 : ���콺 ���� ��ư
    // 1 : ���콺 ������ ��ư
    // 2 : ���콺 �� ��ư
    bool down = Input.GetMouseButtonDown(0);
    bool press = Input.GetMouseButton(0);
    bool up = Input.GetMouseButtonUp(0);

    // Input Manager���� ������ ���ڿ��� ������� �ϴ� Ű �Է� �˻�
    bool down = Input.GetButtonDown("Jump");
    bool press = Input.GetButton("Jump");
    bool up = Input.GetButtonUp("Jump");

    // ������ �࿡ ���� Ű �Է� �˻�
    float axisH = Input.GetAxis("Horizontal");
    float axisV = Input.GetAxisRaw("Vertical");
*/
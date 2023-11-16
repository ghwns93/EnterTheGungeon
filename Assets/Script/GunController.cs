using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    // �ִϸ��̼�
    public string gunHoldAnime = "PilotGunHold";

    // ���� �ִϸ��̼�
    string nowGunAnimation = "";
    // ���� �ִϸ��̼�
    string oldGunAnimation = "";

    // �ִϸ�����
    private Animator gunanimator;

    public float shootSpeed = 12.0f;    //ȭ�� �ӵ�
    public float shootDelay = 0.25f;    //�߻� ����

    public GameObject gunPrefab;        //Ȱ
    public GameObject bulletPrefab;     //ȭ��

    private bool canAttack = true;

    bool inAttack = false;  //���� ���� �Ǵ�
    GameObject gunObj;      //��


    // Start is called before the first frame update
    void Start()
    {
        // (�⺻) �ִϸ��̼� ����
        oldGunAnimation = gunHoldAnime;

        // �ִϸ����� ��������
        gunanimator = GetComponent<Animator>();

        // Ȱ�� �÷��̾� ��ġ�� ��ġ
        Vector3 pos = transform.position;
        gunObj = Instantiate(gunPrefab, pos, Quaternion.identity);
        gunObj.transform.SetParent(transform);  //�÷��̾� ��ü�� �� ��ü�� �θ�� ����

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

        if (Input.GetButton("Fire1") && canAttack)
        {
            // ���� Ű �Է� �� ������ ����
            StartCoroutine(AttackWithDelay());
        }

        // Ȱ ȸ���� �켱���� ����
        SpriteRenderer gunSpr = gunObj.GetComponent<SpriteRenderer>();          //���� SpriteRenderer
        Transform childTransform = gunObj.transform.Find("PilotHand");          //�ڽĿ� �����ϱ�����
        SpriteRenderer handSpr = childTransform.GetComponent<SpriteRenderer>(); //���� SpriteRenderer
        PlayerController plmv = GetComponent<PlayerController>();               //player�� SpriteRenderer

        if (plmv.angleZ > 30 && plmv.angleZ < 150)  // ������
        {
            // ��,Ȱ �켱����
            gunSpr.sortingOrder = 0;
            handSpr.sortingOrder = 0;
        }
        else 
        {
            gunSpr.sortingOrder = 5;    //ĳ���� OrderInLayer == 4
            handSpr.sortingOrder = 6;   //���� �Ѻ�����            
        }
        
        // �� ȸ��
        gunObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ);        
        
    }

    void FixedUpdate()
    {        
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

    public void Attack()
    {
        inAttack = true;            //���� ���� ����

        // ȭ�� �߻�
        PlayerController playerCnt = GetComponent<PlayerController>();
        // ȸ���� ����� ����
        float angleZ = playerCnt.angleZ;
        // ȭ�� ������Ʈ ���� (ĳ���� ���� �������� ȸ��)
        Quaternion r = Quaternion.Euler(0, 0, angleZ + 90);
        GameObject bulletObj = Instantiate(bulletPrefab, transform.position, r);

        // ȭ���� �߻��ϱ� ���� ���� ����
        float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);
        float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);
        Vector3 v = new Vector3(x, y) * shootSpeed;

        // ������ ������ �������� ȭ���� �߻�
        Rigidbody2D body = bulletObj.GetComponent<Rigidbody2D>();
        body.AddForce(v, ForceMode2D.Impulse);

        // ������ ����
        Invoke("StopAttack", shootDelay);
    }

    // ���� ����
    public void StopAttack()
    {
        inAttack = false;
    }

}

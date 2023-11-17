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

    public GameObject gunPrefab;        //��
    public GameObject bulletPrefab;     //�Ѿ�
    public GameObject OtherHandPrefab;  //�ѵ�� �ݴ��� ��
    GameObject gunGateObj;              //����
    GameObject LeftHandObj;             //�޼�
    GameObject RightHandObj;            //������

    private bool canAttack = true;      //���� ������ �Ҷ� ���

    bool inAttack = false;  //���� ���� �Ǵ�
    GameObject gunObj;      //��
    bool islefthand;        //�޼��ִ���
    bool isrighthand;       //�������ִ���
    bool iscalcul = false;  //update���� ����ѹ�

    // Start is called before the first frame update
    void Start()
    {
        // (�⺻) �ִϸ��̼� ����
        oldGunAnimation = gunHoldAnime;

        // �ִϸ����� ��������
        gunanimator = GetComponent<Animator>();

        // ���� �÷��̾� ��ġ�� ��ġ
        Vector3 pos = transform.position;
        gunObj = Instantiate(gunPrefab, pos, Quaternion.identity);
        gunObj.transform.SetParent(transform);  //�÷��̾� ��ü�� �� ��ü�� �θ�� ����

        // ������ ��ġ�� ������Ʈ ��������
        Transform tr = gunObj.transform.Find("GunGate");
        gunGateObj = tr.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // ����꿡 ������ ���¿��� �����ϼż� �ٸ������ ���尡 �ȵ�
        // �����ϼž� �մϴ�.



        //// �ִϸ��̼� ����
        //if (nowAnimation != oldAnimation)
        //{
        //    oldAnimation = nowAnimation;
        //    GetComponent<Animator>().Play(nowAnimation);
        //}

        //if ((Input.GetButtonDown("Fire2"))) // ���콺 ������ �Է½� �ѾȺ��̰�
        //{

        //}
        //if(isDodging)
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

        // playerController ���� ��������
        Vector3 mousePosition = FindObjectOfType<PlayerController>().mousePosition; 

        // ��,��,�ѱ� ��ġ,ȸ��
        if (mousePosition .x> transform.position.x)
        {
            gunObj.GetComponent<SpriteRenderer>().flipY = false;                                    //�������
            gunObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ-90);                     //���콺�� ���� ��ȸ��
            gunObj.transform.position = transform.position + new Vector3(0.2f, -0.15f, 0);          //����ġ ĳ���� ����������

            gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad), 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad)+0.2f);//�ѱ� ��ġ
            if (plmv.angleZ < -45 && plmv.angleZ > -135)
                gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad) +0.15f, 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad));//�ؿ� ���� �ѱ���ġ ����

            childTransform.position = transform.position + new Vector3(0.2f, -0.15f, 0);            //����ġ
            childTransform.rotation = Quaternion.Euler(0, 0, 0);                                    //��ȸ�� x                        
            if(!islefthand)
            {
                LeftHandObj = Instantiate(OtherHandPrefab,
                    transform.position + new Vector3(-0.2f, -0.15f, 0), Quaternion.Euler(0, 0, 0)); //�޼� ����
                islefthand = true;
            }
            LeftHandObj.transform.position = transform.position + new Vector3(-0.2f, -0.15f, 0);    //�� ĳ���Ϳ� �پ�ٴϰ�
            Destroy(RightHandObj);                                                                  //������ ����
            isrighthand = false;
        }      
        else
        {
            gunObj.GetComponent<SpriteRenderer>().flipY = true;                                     //����           
            gunObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ+90);                     //���콺�� ���� ��ȸ��
            gunObj.transform.position = transform.position + new Vector3(-0.2f, -0.15f, 0);         //����ġ ĳ���� ��������

            gunGateObj.transform.position = gunObj.transform.position + 
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad), 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad)+0.2f);//�ѱ� ��ġ
            if(plmv.angleZ<-45 && plmv.angleZ>-135)
                gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad) -0.15f, 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad) );//�ؿ� ���� �ѱ���ġ ����

            childTransform.position = transform.position + new Vector3(-0.2f, -0.15f, 0);           //����ġ
            childTransform.rotation = Quaternion.Euler(0, 0, 0);                                    //��ȸ�� x
            if (!isrighthand)                                                               
            {
                RightHandObj = Instantiate(OtherHandPrefab,
                    transform.position + new Vector3(0.2f, -0.15f, 0), Quaternion.Euler(0, 0, 0));  //������ ����
                isrighthand = true;
            }
            RightHandObj.transform.position = transform.position + new Vector3(+0.2f, -0.15f, 0);   //�� ĳ���Ϳ� �پ�ٴϰ�
            Destroy(LeftHandObj);                                                                   //�޼� ����
            islefthand = false;
        }        
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

    // �Ѿ� �߻�
    public void Attack()
    {
        inAttack = true;            //���� ���� ����                
        PlayerController playerCnt = GetComponent<PlayerController>();
        // ȸ���� ����� ����
        float angleZ = playerCnt.angleZ;
        // �Ѿ� ���� ��ġ
        Vector3 pos = new Vector3(gunGateObj.transform.position.x,
                                          gunGateObj.transform.position.y,
                                          transform.position.z);

        // �Ѿ� ������Ʈ ���� (ĳ���� ���� �������� ȸ��)
        Quaternion r = Quaternion.Euler(0, 0, angleZ + 90);
        GameObject bulletObj = Instantiate(bulletPrefab, pos, r);        

        // ȭ���� �߻��ϱ� ���� ���� ����
        float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);
        float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);
        Vector3 v = new Vector3(x, y) * shootSpeed;

        // ������ ������ �������� ȭ���� �߻�
        Rigidbody2D body = bulletObj.GetComponent<Rigidbody2D>();
        body.AddForce(v, ForceMode2D.Impulse);

    }

}

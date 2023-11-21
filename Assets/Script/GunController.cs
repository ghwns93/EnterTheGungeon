using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // �� �ִϸ�����
    private Animator gunAnimator;

    // �ִϸ��̼�
    public string pilotGunHold = "PilotGunHold";
    public string pilotGunReady = "PilotGunReady";
    public string pilotGunFire = "PilotGunFire";
    public string pilotGunReturn = "PilotGunReturn";
    public string pilotGunReload = "PilotGunReload";

    public float shootSpeed = 12.0f;    //ȭ�� �ӵ�
    public float shootDelay = 1.0f;    //�߻� ����

    public GameObject gunPrefab;        //�� ������
    public GameObject bulletPrefab;     //�Ѿ� ������
    public GameObject OtherHandPrefab;  //�ѵ�� �ݴ��� �� ������
    GameObject gunGateObj;              //����
    GameObject LeftHandObj;             //�޼�
    GameObject RightHandObj;            //������

    private bool canAttack = true;      //���� ������ �Ҷ� ���

    GameObject gunObj;      //�ѿ�����Ʈ
    bool isLeftHand;        //�޼��ִ���
    bool isRightHand;       //�������ִ���

    PlayerController playerController;  //PlayerController�� �Լ�,���� ����ϱ����� �����ص�

    Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        // ���� �÷��̾� ��ġ�� ��ġ
        Vector3 pos = transform.position;
        gunObj = Instantiate(gunPrefab, pos, Quaternion.identity);
        gunObj.transform.SetParent(transform);  //�÷��̾� ��ü�� �� ��ü�� �θ�� ����

        // ������ ��ġ�� ������Ʈ ��������
        Transform tr = gunObj.transform.Find("GunGate");
        gunGateObj = tr.gameObject;

        // �ִϸ����� ��������
        gunAnimator = gunObj.GetComponent<Animator>();

        // (�⺻) �ִϸ��̼� ����
        gunAnimator.Play(pilotGunHold);

    }

    // Update is called once per frame
    void Update()
    {        
        SpriteRenderer gunSpr = gunObj.GetComponent<SpriteRenderer>();          //���� SpriteRenderer
        Transform childTransform = gunObj.transform.Find("PilotHand");          //�ڽĿ� �����ϱ�����
        SpriteRenderer handSpr = childTransform.GetComponent<SpriteRenderer>(); //���� SpriteRenderer
        PlayerController plmv = GetComponent<PlayerController>();               //player�� SpriteRenderer
        
        // playerController�� ���콺 ������ ���� ��������
        mousePosition = FindObjectOfType<PlayerController>().mousePosition; 

        // ��,��,�ѱ� ��ġ,ȸ��
        if (mousePosition .x> transform.position.x) // ���콺�� ĳ���� �����ʿ� ������
        {
            gunObj.GetComponent<SpriteRenderer>().flipY = false;                                    //�������
            gunObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ-90);                     //���콺�� ���� ��ȸ��
            gunObj.transform.position = transform.position + new Vector3(0.2f, -0.15f, 0);          //����ġ ĳ���� ����������

            gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad), 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad)+0.1f);//�ѱ� ��ġ
            if (plmv.angleZ < -45 && plmv.angleZ > -135)
                gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad) + 0.15f, 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad));//�ؿ� ���� �ѱ���ġ ����

            childTransform.position = transform.position + new Vector3(0.2f, -0.15f, 0);            //����ġ
            childTransform.rotation = Quaternion.Euler(0, 0, 0);                                    //��ȸ�� x                        
            if(!isLeftHand)
            {
                isLeftHand = true;
                LeftHandObj = Instantiate(OtherHandPrefab,
                    transform.position + new Vector3(-0.2f, -0.15f, 0), Quaternion.Euler(0, 0, 0)); //�޼� ����                
            }
            LeftHandObj.transform.position = transform.position + new Vector3(-0.2f, -0.15f, 0);    //�޼� ĳ���Ϳ� �پ�ٴϰ�
            Destroy(RightHandObj);  //�ѿ� ���� �־ ���� �ִ� �� ����
            isRightHand = false;
            if (GetComponent<PlayerController>().inlobby)   //�κ񿡼� ��2��
            {
                if (!isRightHand)
                {
                    isRightHand = true;
                    RightHandObj = Instantiate(OtherHandPrefab,
                        transform.position + new Vector3(0.2f, -0.15f, 0), Quaternion.Euler(0, 0, 0));  //������ ����
                }
                RightHandObj.transform.position = transform.position + new Vector3(+0.2f, -0.15f, 0);   //������ ĳ���Ϳ� �پ�ٴϰ�
            }
        }
        else // ���콺�� ĳ���� ���ʿ� ������
        {
            gunObj.GetComponent<SpriteRenderer>().flipY = true;                                     //����           
            gunObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ+90);                     //���콺�� ���� ��ȸ��
            gunObj.transform.position = transform.position + new Vector3(-0.2f, -0.15f, 0);         //����ġ ĳ���� ��������

            gunGateObj.transform.position = gunObj.transform.position + 
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad), 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad)+0.1f);//�ѱ� ��ġ
            if(plmv.angleZ<-45 && plmv.angleZ>-135)
                gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad) -0.15f, 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad) );//�ؿ� ���� �ѱ���ġ ����

            childTransform.position = transform.position + new Vector3(-0.2f, -0.15f, 0);           //����ġ
            childTransform.rotation = Quaternion.Euler(0, 0, 0);                                    //��ȸ�� x
            if (!isRightHand)                                                               
            {
                RightHandObj = Instantiate(OtherHandPrefab,
                    transform.position + new Vector3(0.2f, -0.15f, 0), Quaternion.Euler(0, 0, 0));  //������ ����
                isRightHand = true;
            }
            RightHandObj.transform.position = transform.position + new Vector3(+0.2f, -0.15f, 0);   //������ ĳ���Ϳ� �پ�ٴϰ�
            Destroy(LeftHandObj);   //�ѿ� ���� �־ ���� �ִ� �� ����
            isLeftHand = false;

            if (GetComponent<PlayerController>().inlobby)   //�κ񿡼� ��2�� ����
            {
                if (!isLeftHand)
                {
                    LeftHandObj = Instantiate(OtherHandPrefab,
                        transform.position + new Vector3(-0.2f, -0.15f, 0), Quaternion.Euler(0, 0, 0)); //�޼� ����
                    isLeftHand = true;
                }
                LeftHandObj.transform.position = transform.position + new Vector3(-0.2f, -0.15f, 0);    //�޼� ĳ���Ϳ� �پ�ٴϰ�
            }                                                
        }

        // �켱���� ����
        if (plmv.angleZ > 30 && plmv.angleZ < 150)  // ������
        {
            // ��,Ȱ �켱����
            gunSpr.sortingOrder = 1;
            handSpr.sortingOrder = 1;
            if(RightHandObj)
                RightHandObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
            if (LeftHandObj)
                LeftHandObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else
        {
            gunSpr.sortingOrder = 5;    //ĳ���� OrderInLayer == 4
            handSpr.sortingOrder = 6;   //���� �Ѻ�����
            if (RightHandObj)
                RightHandObj.GetComponent<SpriteRenderer>().sortingOrder = 6;
            if (LeftHandObj)
                LeftHandObj.GetComponent<SpriteRenderer>().sortingOrder = 6;
        }
        if (GetComponent<PlayerController>().gameState == "falling")    //�������� ������
        {
            Destroy(RightHandObj);                                                                  //������ ����
            isRightHand = false;
            Destroy(LeftHandObj);                                                                   //�޼� ����
            isLeftHand = false;
        }
                
        if (GetComponent<PlayerController>().isDodging)// ���콺 ������ �Է½� �ѾȺ��̰�
        {
            Destroy(RightHandObj);                                                                  //������ ����
            isRightHand = false;
            Destroy(LeftHandObj);                                                                   //�޼� ����
            isLeftHand = false;
        }
                    

        if (Input.GetButton("Fire1") && canAttack&&!GetComponent<PlayerController>().inlobby)
        {
            
            gunAnimator.Play(pilotGunReady);

            // ���� Ű �Է� �� ������ ����
            StartCoroutine(AttackWithDelay());
        }

        if( Input.GetButtonUp("Fire1") && !GetComponent<PlayerController>().inlobby)
        {
            gunAnimator.Play(pilotGunReturn);
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
        yield return new WaitForSeconds(shootDelay);

        // ������ �Ŀ� �ٽ� ���� �������� ����
        canAttack = true;
    }

    // �Ѿ� �߻�
    public void Attack()
    {
        gunAnimator.StopPlayback();             // ���ݾִϸ��̼� �������
        gunAnimator.Play(pilotGunFire, -1, 0f); // �ִϸ��̼� Ű����Ʈ ó������ �̵�
        gunAnimator.Play(pilotGunFire);         // �ִϸ��̼� ����

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


/*

�� �߻��ϸ� ������� 10��������? ���� ���� �ణ������ �ؾߵɵ�?

������� �Ÿ��̵��ϸ�� �Ѿ� ���ֱ�

�Ѿ� ���� �پ��� ������ �Ӹ����� �߰� �Ѿ˹߻� ���ϰ�
�����ϸ� �ٻ���� �� �ִϸ��̼�,�� �ִϸ��̼�

�ѽ� ����Ʈ
�Ѿ� �ε����� ���ְ� ����Ʈ
���콺 ũ�ν����

�ѽ�� ȭ�� ��鸮��
(��鸲 ���� ���� �ɼ�)

������ Ű��

//// �ѱ� ��ġ
        //Vector2 fromPos = new Vector2(gunGateObj.transform.position.x, gunGateObj.transform.position.y);
        //// ���콺 ��ġ
        //Vector2 toPos = new Vector2(mousePosition.x, mousePosition.y);
        //// ȭ��߻� ����
        //float angleCharacterwithMouse;
        //float dx = toPos.x - fromPos.x;
        //float dy = toPos.y - fromPos.y;

        //// ��ũź��Ʈ �Լ��� ����(����) ���ϱ�
        //float rad = Mathf.Atan2(dy, dx);

        //// �������� ��ȯ
        //angleCharacterwithMouse = rad * Mathf.Rad2Deg;
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKinScript : MonoBehaviour
{
    // �̵��ӵ�
    public float speed = 1.0f;

    // �ִϸ��̼�
    public string stopUpAnime = "bulletKinStopUp";
    public string stopDownAnime = "bulletKinStopDown";
    public string stopLeftAnime = "bulletKinStopLeft";

    public string walkUpAnime = "bulletKinWalkUp";
    public string walkDownAnime = "bulletKinWalkDown";
    public string walkLeftAnime = "bulletKinWalkSide";

    public string deadAnime = "bulletKinDead";
    public string fallAnime = "bulletKinFalling";

    // ���� �ִϸ��̼�
    string nowAnimation = "";
    // ���� �ִϸ��̼�
    string oldAnimation = "";

    bool isActive = true;
    bool isAttack = false;  //���� ����
    bool isAct = true;
    bool isHit = false;
    bool isDead = false;

    Rigidbody2D rbody;              // RigidBody 2D ������Ʈ
    float axisH;
    float axisV;

    public static int hp = 3;       // ���� HP

    public float attackDistance = 10;    //���� �Ÿ�

    public GameObject EnemyHand;
    public GameObject EnemyGun;

    // �ִϸ�����
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D ��������
        rbody = GetComponent<Rigidbody2D>();

        // �ִϸ����� ��������
        animator = GetComponent<Animator>();

        // (�⺻) �ִϸ��̼� ����
        oldAnimation = stopDownAnime;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (isActive)
        {
            if (player != null)
            {
                isActive = true;

                #region [ �÷��̾� ���� �� ���� �ִϸ��̼� ���� ]

                float dis = Vector2.Distance(player.transform.position, gameObject.transform.position);

                float angleZ = GetAngle(gameObject.transform.position, player.transform.position);

                GetComponent<SpriteRenderer>().flipX = false;

                // �̵� ������ �������� ����� �ִϸ��̼��� �����Ѵ�
                if (dis > attackDistance) // ��Ÿ� ���� �÷��̾���� �Ÿ��� �� ��� �̵�
                {
                    if (-45 <= angleZ && angleZ < 45)                     //������
                    {
                        nowAnimation = walkLeftAnime;
                        GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else if (45 <= angleZ && angleZ < 135)                // ��
                    {
                        nowAnimation = walkUpAnime;
                    }
                    else if (135 <= angleZ && angleZ < 180 || -180 <= angleZ && angleZ < -135)     // ����
                    {
                        nowAnimation = walkLeftAnime;
                    }
                    else if (-135 <= angleZ && angleZ < -45)              // �Ʒ�
                    {
                        nowAnimation = walkDownAnime;
                    }

                    isAttack = false;

                    //�÷��̾���� �Ÿ��� �������� ������ ���ϱ�
                    float dx = player.transform.position.x - transform.position.x;
                    float dy = player.transform.position.y - transform.position.y;
                    float rad = Mathf.Atan2(dy, dx);

                    //�̵� ����
                    axisH = Mathf.Cos(rad) * speed;
                    axisV = Mathf.Sin(rad) * speed;
                }
                else // �����߿� ����
                {
                    if (-45 <= angleZ && angleZ < 45)                     //������
                    {
                        nowAnimation = stopLeftAnime;
                        GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else if (45 <= angleZ && angleZ < 135)                // ��
                    {
                        nowAnimation = stopUpAnime;
                    }
                    else if (135 <= angleZ && angleZ < 180 || -180 <= angleZ && angleZ < -135)     // ����
                    {
                        nowAnimation = stopLeftAnime;
                    }
                    else if (-135 <= angleZ && angleZ < -45)              // �Ʒ�
                    {
                        nowAnimation = stopDownAnime;
                    }

                    isAttack = true;

                    axisH = 0.0f;
                    axisV = 0.0f;
                }

                // �ִϸ��̼� ����
                if (nowAnimation != oldAnimation)
                {
                    oldAnimation = nowAnimation;
                    GetComponent<Animator>().Play(nowAnimation);
                }
                #endregion

                #region [ �÷��̾� ���⿡ ���� �� ��ġ ���� ]

                if (EnemyHand != null)
                {
                    if (-90 <= angleZ && angleZ < 90)                     //������
                    {
                        EnemyHand.transform.position = new Vector2(transform.position.x + 0.2f, EnemyHand.transform.position.y);
                        EnemyGun.transform.position = new Vector2(EnemyHand.transform.position.x + 0.1f, EnemyGun.transform.position.y);
                        EnemyGun.transform.rotation = Quaternion.Euler(0, 0, angleZ);
                        EnemyGun.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else    // ����
                    {
                        float reverseAngle = 0.0f;

                        if (angleZ >= 90) reverseAngle = (180 - angleZ) * -1;
                        else reverseAngle = (angleZ + 180);

                        EnemyHand.transform.position = new Vector2(transform.position.x - 0.2f, EnemyHand.transform.position.y);
                        EnemyGun.transform.position = new Vector2(EnemyHand.transform.position.x - 0.1f, EnemyGun.transform.position.y);
                        EnemyGun.transform.rotation = Quaternion.Euler(0, 0, reverseAngle);
                        EnemyGun.GetComponent<SpriteRenderer>().flipX = true;
                    }
                }

                #endregion
            }
            else
            {
                rbody.velocity = Vector2.zero;
                isActive = false;
            }
        }
    }

    // (����Ƽ �ʱ� ���� ����) 0.02�ʸ��� ȣ��Ǹ�, 1�ʿ� �� 50�� ȣ��Ǵ� �Լ�
    void FixedUpdate()
    {
        Debug.Log(hp);

        if (isActive && hp > 0)
        {
            Debug.Log("Move1");
            if (!isAttack)
            {
                Debug.Log("Move");
                //���� �̵���Ű��
                rbody.velocity = new Vector2(axisH, axisV);
            }
            else
            {
                rbody.velocity = Vector2.zero;
            }
        }
    }

    // p1���� p2������ ������ ����Ѵ�
    float GetAngle(Vector2 p1, Vector2 p2)
    {
        float angle;

        // p1�� p2�� ���� ���ϱ� (������ 0���� �ϱ� ����)
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;

        // ��ũź��Ʈ �Լ��� ����(����) ���ϱ�
        float rad = Mathf.Atan2(dy, dx);

        // �������� ��ȯ
        angle = rad * Mathf.Rad2Deg;

        //// �� ���⿡ ������� ĳ���Ͱ� �����̰� ���� ��� ���� ����
        //if (axisH != 0 || axisV != 0 )
        
        return angle;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {   
        if (collision.gameObject.tag == "FallDown")
        {
            isActive = false;

            // �̵� ����
            rbody.velocity = new Vector2(0, 0);

            // �߶� �ִϸ��̼� ���
            nowAnimation = fallAnime;
            animator.Play(fallAnime);
        }
    }

    // ������ ���
    void GetDamage(GameObject enemy)
    {
        //if (gameState == "playing")
        //{
        //    hp--;   // HP����
        //    PlayerPrefs.SetInt("PlayerHP", hp); // ���� HP ����
        //    if (hp > 0)
        //    {
        //        // �̵� ����
        //        rbody.velocity = new Vector2(0, 0);
        //        // ��Ʈ�� (���� ������ ������ �ݴ��)
        //        Vector3 toPos = (transform.position - enemy.transform.position).normalized;
        //        rbody.AddForce(new Vector2(toPos.x * 4, toPos.y * 4), ForceMode2D.Impulse);
        //        // ���� ���ݹް� ����
        //        inDamage = true;
        //        Invoke("DamageEnd", 0.25f);
        //    }
        //    else
        //    {
        //        // ü���� ������ ���ӿ���
        //        GameOver();
        //    }
        //}
    }

    // ������ ó�� ����
    void DamageEnd()
    {
        //inDamage = false;
        //gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    // ���ӿ��� ó��
    void GameOver()
    {
        Destroy(gameObject);
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
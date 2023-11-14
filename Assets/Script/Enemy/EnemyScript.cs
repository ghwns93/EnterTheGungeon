using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletStat
{
    public GameObject bullet;
    public Vector3 v;
}

public class EnemyScript : MonoBehaviour
{
    public int hp = 3;                      //�� ü��
    public float speed = 1.0f;              //�� �ӵ�
    public int roomNumber = 1;    //��ġ�� ��ġ
    public float attackDistance = 10;    //���� �Ÿ�

    public GameObject bulletPrefab;         //�Ѿ�
    public float shootSpeed = 5.0f;         //�Ѿ� �ӵ�

    //�ִϸ��̼� ���
    public string idleAnime = "EnemyIdle";
    public string attackAnime = "EnemyLeft";
    public string attackFinishAnime = "EnemyLeft";
    public string deadAnime = "EnemyDead";
    public string mjscAnime = "EnemyMjsc";

    //���� & ���� �ִϸ��̼�
    string nowAnimation = "";
    string oldAnimation = "";

    //�Էµ� �̵� ��
    float axisH;
    float axisV;
    Rigidbody2D rbody;

    //Ȱ��ȭ ����
    bool isActive = false;
    bool inAttack = false;  //���� ����

    BulletStat[] bulletStats;

    int count = 0;

    bool isAct = false;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        bulletStats = new BulletStat[30];
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            isActive = true;

            //�÷��̾���� �Ÿ� Ȯ��
            Vector3 plpos = player.transform.position;
            float dist = Vector2.Distance(transform.position, plpos);
            
            if (!inAttack)
            {
                //�÷��̾ ���� �ȿ� �ְ� ���� ���� �ƴ� ���
                if (dist <= attackDistance)
                {
                    inAttack = true;

                    axisH = 0.0f;
                    axisV = 0.0f;
                    nowAnimation = attackAnime;
                }
                //�÷��̾ �ν� ������ ��� ���
                else if (dist > attackDistance)
                {
                    //�÷��̾���� �Ÿ��� �������� ������ ���ϱ�
                    float dx = player.transform.position.x - transform.position.x;
                    float dy = player.transform.position.y - transform.position.y;
                    float rad = Mathf.Atan2(dy, dx);
                    float angleZ = rad * Mathf.Rad2Deg;

                    nowAnimation = idleAnime;

                    //�̵� ����
                    axisH = Mathf.Cos(rad) * speed;
                    axisV = Mathf.Sin(rad) * speed;
                }
            }
        }
        else
        {
            rbody.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (isActive && hp > 0)
        {
            if (inAttack)
            {
                rbody.velocity = Vector2.zero;

                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player != null)
                {
                    if (count < 30)
                    {
                        if (!isAct)
                        {
                            isAct = true;

                            float objX = 0, objY = 0;

                            if (count < 10)
                            {
                                objX = transform.position.x - 1;
                                objY = transform.position.y + (float)(-1 + (0.2 * (count % 10)));
                            }
                            else if (count < 20)
                            {
                                objX = transform.position.x - (float)(-1 + (0.2 * (count % 10)));
                                objY = transform.position.y - (float)(1 - (0.2 * (count % 10)));
                            }
                            else
                            {
                                objX = transform.position.x + 1;
                                objY = transform.position.y + (float)(-1 + (0.2 * (count % 10)));
                            }

                            Vector3 bulletTran = new Vector3(objX, objY);

                            float dx = player.transform.position.x - objX;
                            float dy = player.transform.position.y - objY;

                            //��ũź��Ʈ2 �Լ��� ����(ȣ����) ���ϱ�
                            float rad = Mathf.Atan2(dy, dx);

                            //������ ����(���ʺй�)�� ��ȯ
                            float angle = rad * Mathf.Rad2Deg;

                            //�������� �̿��Ͽ� �Ѿ� ������Ʈ ����� (���� �������� ȸ��)
                            Quaternion r = Quaternion.Euler(0, 0, angle);
                            GameObject bullet = Instantiate(bulletPrefab, bulletTran, r);
                            float x = Mathf.Cos(rad);
                            float y = Mathf.Sin(rad);
                            Vector3 v = new Vector3(x, y) * shootSpeed;

                            bulletStats[count].bullet = bullet;
                            bulletStats[count].v = v;

                            count++;

                            isAct = false;
                        }
                    }
                    else
                    {
                        BulletShoot(bulletStats);
                        Array.Clear(bulletStats, 0, bulletStats.Length);
                        count = 0;
                    }
                }
            }
            else
            {
                //���� �̵���Ű��
                rbody.velocity = new Vector2(axisH, axisV);
            }

            //�ִϸ��̼� �����ϱ�
            if (nowAnimation != oldAnimation)
            {
                oldAnimation = nowAnimation;
                Animator animator = GetComponent<Animator>();
                animator.Play(nowAnimation);
            }
        }
    }

    //����
    void Attack()
    {
        //�÷��̾� ������ ��������
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            #region [ ���� �߻� �ּ� ]
            ////�� �� ����� �Ÿ��� ���ϱ�
            //float dx = player.transform.position.x - transform.position.x;
            //float dy = player.transform.position.y - transform.position.y;

            ////��ũź��Ʈ2 �Լ��� ����(ȣ����) ���ϱ�
            //float rad = Mathf.Atan2(dy, dx);

            ////������ ����(���ʺй�)�� ��ȯ
            //float angle = rad * Mathf.Rad2Deg;

            ////�������� �̿��Ͽ� �Ѿ� ������Ʈ ����� (���� �������� ȸ��)
            //Quaternion r = Quaternion.Euler(0, 0, angle);
            //GameObject bullet = Instantiate(bulletPrefab, transform.position, r);
            //float x = Mathf.Cos(rad);
            //float y = Mathf.Sin(rad);
            //Vector3 v = new Vector3(x, y) * shootSpeed;

            ////�Ѿ� �߻�
            //Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
            //rbody.AddForce(v, ForceMode2D.Impulse);
            #endregion

        }

        nowAnimation = attackFinishAnime;
    }

    void AttackAnimationEnd()
    {
        inAttack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //������ ȭ���� �¾��� ���
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("Enter!");
            hp--; //ü�� ����

            //ü���� 0 ���ϰ� �Ǵ� ���� ���ó��
            if (hp <= 0)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Animator>().Play(deadAnime);
                Destroy(gameObject, 1);
            }
            else
            {
                Blink();
            }
        }
    }

    private IEnumerator Blink()
    {
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();

        Color defaultColor = playerSprite.color;

        playerSprite.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(0.05f);

        playerSprite.color = defaultColor;
    }

    private void BulletShoot(BulletStat[] bulletStats)
    {
        for (int i = 0; i < bulletStats.Length; i++)
        {
            if (bulletStats[i].bullet != null)
            {
                Rigidbody2D rbody = bulletStats[i].bullet.GetComponent<Rigidbody2D>();
                rbody.AddForce(bulletStats[i].v, ForceMode2D.Impulse);
            }
        }
    }
}

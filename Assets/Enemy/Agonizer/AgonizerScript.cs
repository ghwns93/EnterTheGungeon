using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AgonizerScript : MonoBehaviour
{
    public int hp = 3;                      //�� ü��
    public float speed = 1.0f;              //�� �ӵ�
    public int roomNumber = 1;              //��ġ�� ��ġ
    public float attackDistance = 10;       //���� �Ÿ�
    public float shootWatingTime = 3.0f;    //�Ѿ� �߻� ����
    public float minAngle = 45.0f;          //�ּ� ���� (���� 0 , �Ʒ��� 90 , ������ 180, ���� 270)
    public float maxAngle = 135.0f;         //�ִ� ���� (���� 0 , �Ʒ��� 90 , ������ 180, ���� 270)
    public int MaxBullet = 10;              //1��ƾ�� �Ѿ� ����
    public bool bulletTwoWay = true;         //�Ѿ� �Դ� ���� ����.

    //�ִϸ��̼� ���
    public string idleAnime = "EnemyIdle";
    public string attackAnime = "EnemyLeft";
    public string attackingAnime = "EnemyAttaking";
    public string attackFinishAnime = "EnemyLeft";
    public string deadAnime = "EnemyDead";

    public GameObject bulletPrefab;         //�Ѿ�
    public float shootSpeed = 5.0f;         //�Ѿ� �ӵ�

    //���� & ���� �ִϸ��̼�
    string nowAnimation = "";
    string oldAnimation = "";

    //�Էµ� �̵� ��
    float axisH;
    float axisV;
    Rigidbody2D rbody;

    //Ȱ��ȭ ����
    bool isActive = false;
    bool isAttack = false;  //���� ����
    bool isAct = true;
    bool isHit = false;

    List<GameObject> bulletStats; //�Ѿ��� ǥ���� �ѹ��� ��� ���� List�� ����
    int count = 0; //�Ѿ� ���� ī��Ʈ

    int bolletShape = 0;
    
    //R���� �Ѿ� ���󰡴� ����
    float rDelay = 0;

    bool RShoot = false;

    MonsterAwakeManager monsterAwake;
    bool awakeOnce = true;

    public AudioClip audioShot;
    public AudioClip audioDeath;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        bulletStats = new List<GameObject>();
        monsterAwake = GetComponent<MonsterAwakeManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isActive)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                isActive = true;

                //�÷��̾���� �Ÿ� Ȯ��
                Vector3 plpos = player.transform.position;
                float dist = Vector2.Distance(transform.position, plpos);

                //Debug.Log("dist :" + dist);
                if (dist <= attackDistance)
                {
                    if (!isAttack)
                    {
                        //�÷��̾ ���� �ȿ� �ְ� ���� ���� �ƴ� ���
                        isAttack = true;
                        nowAnimation = attackAnime;
                    }
                }
                //�÷��̾ �ν� ������ ��� ���
                else if (dist > attackDistance)
                {
                    if (nowAnimation == attackingAnime || nowAnimation == attackAnime) nowAnimation = attackFinishAnime;
                }
            }
            else
            {
                rbody.velocity = Vector2.zero;
            }
        }
        else if (awakeOnce)
        {
            Debug.Log(monsterAwake.isAwake);
            isActive = monsterAwake.isAwake;
            if (isActive) awakeOnce = false;
        }
    }

    private void FixedUpdate()
    {
        if (isActive && hp > 0)
        {
            if (isAttack)
            {
                rbody.velocity = Vector2.zero;

                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player != null)
                {
                    if (!isAct)
                    {
                        Debug.Log("rDelay : " + rDelay);

                        if (count < MaxBullet + 1 && 0 <= count && rDelay == 0)
                        {
                            isAct = true;
                            BulletShoot();
                            rDelay++;
                            isAct = false;
                        }
                        else if(count >= MaxBullet + 1)
                        {
                            //isAct = true;
                            //nowAnimation = attackFinishAnime;
                            count = 0;
                            rDelay++;
                        }
                        else
                        {
                            if (rDelay >= shootWatingTime) rDelay = 0;
                            else rDelay++;
                        }
                    }
                }
            }

            //�ִϸ��̼� �����ϱ�
            if (nowAnimation != oldAnimation)
            {
                //Debug.Log("nowAnime : " + nowAnimation);
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
            Debug.Log("AttackIn!");
            isAct = false;
        }
    }

    void AttackAnimationEnd()
    {
        isAct = true;
        isAttack = false;

        nowAnimation = idleAnime;
    }

    void InBulletCharge()
    {
        nowAnimation = attackingAnime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //������ ȭ���� �¾��� ���
        if (collision.gameObject.tag == "PlayerBullet")
        {
            hp--; //ü�� ����
            Destroy(collision.gameObject);

            //ü���� 0 ���ϰ� �Ǵ� ���� ���ó��
            if (hp <= 0)
            {
                audioSource.PlayOneShot(audioDeath);

                rbody.velocity = Vector2.zero;
                GetComponent<CapsuleCollider2D>().enabled = false;
                GetComponent<Animator>().Play(deadAnime);

                foreach (var bs in bulletStats)
                {
                    Destroy(bs);
                }

                Destroy(gameObject, 1);
            }
            else
            {
                if(!isHit)StartCoroutine(Blink());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private IEnumerator Blink()
    {
        isHit = true;

        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();

        Color defaultColor = new Color(1, 1, 1, 1);

        playerSprite.color = new Color(1, 0, 0, 1);

        yield return new WaitForSeconds(0.2f);

        playerSprite.color = defaultColor;

        isHit = false;
    }

    private void BulletShoot()
    {
        float objX = 0, objY = 0;

        float rad = 0.0f;
        float halfCount = MaxBullet / 2;
        float limit = 0.0f;

        if (bulletTwoWay)
        {
            if (halfCount < count)
            {
                limit = (float)(maxAngle - (((maxAngle - minAngle) / halfCount) * ((count - halfCount) - 0.5f)));

                limit = limit <= minAngle ? minAngle : limit;

                rad = limit * Mathf.Deg2Rad;
                objX = transform.position.x + ((float)Math.Cos(rad));
                objY = transform.position.y - ((float)Math.Sin(rad));
            }
            else
            {
                limit = (float)(minAngle + (((maxAngle - minAngle) / halfCount) * count));

                limit = limit >= maxAngle ? maxAngle : limit;

                rad =  limit * Mathf.Deg2Rad;
                objX = transform.position.x + ((float)Math.Cos(rad));
                objY = transform.position.y - ((float)Math.Sin(rad));
            }
        }
        else
        {
            limit = (float)(minAngle + (((maxAngle - minAngle) / MaxBullet) * count));

            limit = limit >= maxAngle ? maxAngle : limit;

            rad =  limit * Mathf.Deg2Rad;
            objX = transform.position.x + ((float)Math.Cos(rad));
            objY = transform.position.y - ((float)Math.Sin(rad));
        }

        //������ ����(���ʺй�)�� ��ȯ
        float angle = rad * Mathf.Rad2Deg;

        Vector3 v = new Vector3(objX, objY);

        Quaternion r = Quaternion.Euler(0, 0, angle);
        GameObject bullet = Instantiate(bulletPrefab, v, r);

        float dx = transform.position.x - bullet.transform.position.x;
        float dy = transform.position.y - bullet.transform.position.y;

        Vector2 v2 = new Vector3(dx * -1, dy * -1) * shootSpeed;

        Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
        rbody.AddForce(v2, ForceMode2D.Impulse);
        audioSource.PlayOneShot(audioShot);

        rDelay++;
        count++;
    }
}
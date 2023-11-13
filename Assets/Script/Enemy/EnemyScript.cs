using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
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

            //�÷��̾ ���� �ȿ� �ְ� ���� ���� �ƴ� ���
            if (dist <= attackDistance)
            {
                Debug.Log("attack");

                inAttack = true;
                nowAnimation = attackAnime;

                rbody.velocity = Vector2.zero;
            }
            //�÷��̾ �ν� ������ ��� ���
            else if (dist > attackDistance)
            {
                Debug.Log("move");

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int hp = 3;                      //적 체력
    public float speed = 1.0f;              //적 속도
    public int roomNumber = 1;    //배치된 위치
    public float attackDistance = 10;    //공격 거리

    public GameObject bulletPrefab;         //총알
    public float shootSpeed = 5.0f;         //총알 속도

    //애니메이션 목록
    public string idleAnime = "EnemyIdle";
    public string attackAnime = "EnemyLeft";
    public string attackFinishAnime = "EnemyLeft";
    public string deadAnime = "EnemyDead";
    public string mjscAnime = "EnemyMjsc";

    //현재 & 이전 애니메이션
    string nowAnimation = "";
    string oldAnimation = "";

    //입력된 이동 값
    float axisH;
    float axisV;
    Rigidbody2D rbody;

    //활성화 여부
    bool isActive = false;
    bool inAttack = false;  //공격 상태

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

            //플레이어와의 거리 확인
            Vector3 plpos = player.transform.position;
            float dist = Vector2.Distance(transform.position, plpos);

            //플레이어가 범위 안에 있고 공격 중이 아닌 경우
            if (dist <= attackDistance)
            {
                Debug.Log("attack");

                inAttack = true;
                nowAnimation = attackAnime;

                rbody.velocity = Vector2.zero;
            }
            //플레이어가 인식 범위를 벗어난 경우
            else if (dist > attackDistance)
            {
                Debug.Log("move");

                //플레이어와의 거리를 바탕으로 각도를 구하기
                float dx = player.transform.position.x - transform.position.x;
                float dy = player.transform.position.y - transform.position.y;
                float rad = Mathf.Atan2(dy, dx);
                float angleZ = rad * Mathf.Rad2Deg;

                nowAnimation = idleAnime;

                //이동 벡터
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
                //몬스터 이동시키기
                rbody.velocity = new Vector2(axisH, axisV);
            }

            //애니메이션 변경하기
            if (nowAnimation != oldAnimation)
            {
                oldAnimation = nowAnimation;
                Animator animator = GetComponent<Animator>();
                animator.Play(nowAnimation);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TableController : MonoBehaviour
{
    // 테이블 애니메이션
    public string TableIdle = "TableIdle";
    public string TableRight = "TableRight";
    public string TableLeft = "TableLeft";
    public string TableUp = "TableUp";
    public string TableDown = "TableDown";

    // 움직일 수 잇는 상태
    public bool canMove = false;

    private Animator animator;
    private Rigidbody2D rbody;

    private GameObject player;

    // 방향벡터를 구하기 위한 벡터
    private Vector2 Vec;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();

        // rigidbody의 타입을 Static으로 테이블이 움직일 수 없게.
        rbody.bodyType = RigidbodyType2D.Static;
        
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어
        player = GameObject.FindGameObjectWithTag("Player");

        // 플레이어와 테이블의 위치를 받아와서 벡터를 구하기.
        Vector2 playerPos = player.transform.position;
        Vector2 tablePos = transform.position;

        Vec = playerPos - tablePos;
        float distance = Vec.magnitude;
        // 방향 벡터 구하기.
        direction = Vec / distance;

        if (canMove)
        {
            rbody = GetComponent<Rigidbody2D>();
            // 테이블을 움직일 수 있게 변경
            rbody.bodyType = RigidbodyType2D.Dynamic;

            gameObject.layer = 14;

            // 테이블과 총알이 충돌이 가능하게 변경. (막기)
            //Physics2D.IgnoreLayerCollision(9, 10, true);
            // 플레이어 총알
            //Physics2D.IgnoreLayerCollision(9, 12, true);
        }

    }

    /* 테이블 처음 상태:
        rigidbody2d static
        총알과 충돌하지 않음. Layer조정
        
        테이블 e를 누른 후
        rigidbody2d dynamic
        움직일 수 있게 됨
        총알을 막을 수 있게 됨.
        
        상태 순서
        플레이어와 충돌상태에 있는지 체크 후
        플레이어와 각도를 받아와서 
        각도에 따라 애니메이션 재생
        재생 후 옮기거나 총알을 막을 수 있게
        그 후에는 e를 눌러도 아무런 작동이 되지 않게
    */

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetButton("E") && canMove == false)
            {
                if (direction.x > 0.5 && direction.x <= 1)
                {
                    //Debug.Log("좌로밀기");
                    animator.Play("TableLeft");
                    canMove = true;
                }
                else if (direction.x < -0.5 && direction.x >= -1)
                {
                    //Debug.Log("우로밀기");
                    animator.Play("TableRight");
                    canMove = true;
                }
                else if (direction.y > 0.5 && direction.y <= 1 )
                {
                    //Debug.Log("아래로밀기");
                    animator.Play("TableDown");
                    canMove = true;
                }
                else if (direction.y < -0.5 && direction.y >= -1)
                {
                    //Debug.Log("위로 밀기");
                    animator.Play("TableUp");
                    canMove = true;
                }
                else
                {
                    return;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    public string TableIdle = "TableIdle";
    public string TableRight = "TableRight";
    public string TableLeft = "TableLeft";
    public string TableUp = "TableUp";
    public string TableDown = "TableDown";
    public bool guardOn = false;

    private Animator animator;
    private Rigidbody2D rbody;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어와의 각도 계산
        player = GameObject.FindGameObjectWithTag("Player");

        Vector2 playerPos = player.transform.position;
        Vector2 tablePos = transform.position;



        Guard();
        
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

    void Guard()
    {
        if (guardOn)
        {
            rbody = GetComponent<Rigidbody2D>();
            rbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetButton("E"))
            {
                animator.Play("TableRight");
                guardOn = true;
            }
        }
    }
}

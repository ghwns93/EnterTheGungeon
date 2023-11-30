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
        // �÷��̾���� ���� ���
        player = GameObject.FindGameObjectWithTag("Player");

        Vector2 playerPos = player.transform.position;
        Vector2 tablePos = transform.position;



        Guard();
        
    }
    
    /* ���̺� ó�� ����:
        rigidbody2d static
        �Ѿ˰� �浹���� ����. Layer����
        
        ���̺� e�� ���� ��
        rigidbody2d dynamic
        ������ �� �ְ� ��
        �Ѿ��� ���� �� �ְ� ��.
        
        ���� ����
        �÷��̾�� �浹���¿� �ִ��� üũ ��
        �÷��̾�� ������ �޾ƿͼ� 
        ������ ���� �ִϸ��̼� ���
        ��� �� �ű�ų� �Ѿ��� ���� �� �ְ�
        �� �Ŀ��� e�� ������ �ƹ��� �۵��� ���� �ʰ�
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

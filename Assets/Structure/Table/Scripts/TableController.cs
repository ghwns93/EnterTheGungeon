using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TableController : MonoBehaviour
{
    // ���̺� �ִϸ��̼�
    public string TableIdle = "TableIdle";
    public string TableRight = "TableRight";
    public string TableLeft = "TableLeft";
    public string TableUp = "TableUp";
    public string TableDown = "TableDown";

    // ������ �� �մ� ����
    public bool canMove = false;

    private Animator animator;
    private Rigidbody2D rbody;

    private GameObject player;

    // ���⺤�͸� ���ϱ� ���� ����
    private Vector2 Vec;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();

        // rigidbody�� Ÿ���� Static���� ���̺��� ������ �� ����.
        rbody.bodyType = RigidbodyType2D.Static;
        
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾�
        player = GameObject.FindGameObjectWithTag("Player");

        // �÷��̾�� ���̺��� ��ġ�� �޾ƿͼ� ���͸� ���ϱ�.
        Vector2 playerPos = player.transform.position;
        Vector2 tablePos = transform.position;

        Vec = playerPos - tablePos;
        float distance = Vec.magnitude;
        // ���� ���� ���ϱ�.
        direction = Vec / distance;

        if (canMove)
        {
            rbody = GetComponent<Rigidbody2D>();
            // ���̺��� ������ �� �ְ� ����
            rbody.bodyType = RigidbodyType2D.Dynamic;

            gameObject.layer = 14;

            // ���̺�� �Ѿ��� �浹�� �����ϰ� ����. (����)
            //Physics2D.IgnoreLayerCollision(9, 10, true);
            // �÷��̾� �Ѿ�
            //Physics2D.IgnoreLayerCollision(9, 12, true);
        }

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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetButton("E") && canMove == false)
            {
                if (direction.x > 0.5 && direction.x <= 1)
                {
                    //Debug.Log("�·ιб�");
                    animator.Play("TableLeft");
                    canMove = true;
                }
                else if (direction.x < -0.5 && direction.x >= -1)
                {
                    //Debug.Log("��ιб�");
                    animator.Play("TableRight");
                    canMove = true;
                }
                else if (direction.y > 0.5 && direction.y <= 1 )
                {
                    //Debug.Log("�Ʒ��ιб�");
                    animator.Play("TableDown");
                    canMove = true;
                }
                else if (direction.y < -0.5 && direction.y >= -1)
                {
                    //Debug.Log("���� �б�");
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

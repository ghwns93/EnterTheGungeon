using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �̵��ӵ�
    public float speed = 3.0f;

    // �ִϸ��̼�
    public string stopUpAnime = "PilotStopUp";
    public string stopDownAnime = "PilotStopDown";
    public string stopLeftUpAnime = "PilotStopRightUp";
    public string stopLeftDownAnime = "PilotStopRightDown";
    public string stopRightUpAnime = "PilotStopRightUp";
    public string stopRightDownAnime = "PilotStopRightDown";

    public string walkUpAnime = "PilotWalkUp";
    public string walkDownAnime = "PilotWalkDown";
    public string walkLeftUpAnime = "PilotWalkRightUp";
    public string walkLeftDownAnime = "PilotWalkRightDown";
    public string walkRightUpAnime = "PilotWalkRightUp";
    public string walkRightDownAnime = "PilotWalkRightDown";

    public string dodgeUpAnime = "PilotDodgeUp";
    public string dodgeDownAnime = "PilotDodgeDown";
    public string dodgeLeftUpAnime = "PilotDodgeRightUp";
    public string dodgeLeftDownAnime = "PilotDodgeRightDown";
    public string dodgeRightUpAnime = "PilotDodgeRightUp";
    public string dodgeRightDownAnime = "PilotDodgeRightDown";

    public string deadAnime = "PlayerDead";
    public string fallAnime = "PilotFall";
    public string pilotOpenAnime = "PilotOpenItem";

    // ���� �ִϸ��̼�
    string nowAnimation = "";
    // ���� �ִϸ��̼�
    string oldAnimation = "";

    // �ִϸ�����
    private Animator animator;

    float axisH = 0.0f;                     // ���� �Է� (-1.0 ~ 1.0)
    float axisV = 0.0f;                     // ���� �Է� (-1.0 ~ 1.0)
    public float angleZ = -90.0f; // ȸ��

    Rigidbody2D rbody;              // RigidBody 2D ������Ʈ
    bool isMoving = false;          // �̵� ��
    bool isDodging = false;         // ȸ�� ��

    public static int hp = 3;                   // �÷��̾��� HP
    public static string gameState;    // ���� ����
    bool inDamage = false;                   // �ǰ� ����



    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D ��������
        rbody = GetComponent<Rigidbody2D>();

        // (�⺻) �ִϸ��̼� ����
        oldAnimation = stopDownAnime;

        // �ִϸ����� ��������
        animator = GetComponent<Animator>();
                
        // ���� ���� ����
        gameState = "playing";

        // HP �ҷ�����
        hp = PlayerPrefs.GetInt("PlayerHP");
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���� �ƴϰų� ���ݹް� ���� ��쿡�� �ƹ� �͵� ���� ����
        if (gameState != "playing" || inDamage)
        {
            return;
        }

        if (isMoving == false)
        {
            axisH = Input.GetAxisRaw("Horizontal"); // �¿�
            axisV = Input.GetAxisRaw("Vertical"); // ����
        }

        // Ű �Է��� ���Ͽ� �̵� ���� ���ϱ�
        Vector2 fromPt = transform.position;
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
        angleZ = GetAngle(fromPt, toPt);

        // �̵� ������ �������� ����� �ִϸ��̼��� �����Ѵ�
        if ( (axisH != 0 || axisV != 0) && !isDodging) // Ű �Է��� �ִ� ��쿡�� Walk �ִϸ��̼��� ���
        {
            if (angleZ > -60 && angleZ < 15)                     //����, �����Ʒ�
            {
                nowAnimation = walkRightDownAnime;
            }
            else if (angleZ > 30 && angleZ < 60)                 // ������
            {
                nowAnimation = walkRightUpAnime;
            }
            else if (angleZ > 75 && angleZ < 105)                // ��
            {
                nowAnimation = walkUpAnime;
            }
            else if (angleZ > 120 && angleZ < 150)               // ����
            {
                nowAnimation = walkLeftUpAnime;
            }
            else if (angleZ > 145 && angleZ < 240 || angleZ < -105 && angleZ > -200)   // ��, �޹�
            {
                nowAnimation = walkLeftDownAnime;
            }
            else if (angleZ < -80 && angleZ > -100)              // �Ʒ�
            {
                nowAnimation = walkDownAnime;
            }
        }
        else if(axisH == 0.0f && axisV == 0.0f && !isDodging)// Ű �Է��� ���� ��쿡�� Stop �ִϸ��̼��� ���
        {            
            if (nowAnimation == walkRightDownAnime)     //����, �����Ʒ�
            {
                nowAnimation = stopRightDownAnime;
            }
            else if (angleZ > 0 && angleZ < 90)         // ������
            {
                nowAnimation = stopRightUpAnime;
            }
            else if (nowAnimation == walkUpAnime)       // ��
            {
                nowAnimation = stopUpAnime;
            }
            else if (angleZ > 90 && angleZ < 180)       // ����
            {
                nowAnimation = stopLeftUpAnime;
            }
            else if (nowAnimation == walkLeftDownAnime) // ��, �޹�
            {
                nowAnimation = stopLeftDownAnime;
            }
            else if (nowAnimation == walkDownAnime)     // �Ʒ�
            {
                nowAnimation = stopDownAnime;
            }
        }

        // �������� �̵��� �� X�� �ø�
        if (axisH < 0)
        {
            // SpriteRenderer�� flipX�� ����ϴ� ���
            GetComponent<SpriteRenderer>().flipX = true;

            // Transform�� Rotation�� ����ϴ� ���
            //transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (axisH > 0) // ���������� �̵��� �� X�� �ø� ����
        {
            // SpriteRenderer�� flipX�� ����ϴ� ���
            GetComponent<SpriteRenderer>().flipX = false;

            // Transform�� Rotation�� ����ϴ� ���
            //transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // �ִϸ��̼� ����
        if (nowAnimation != oldAnimation)
        {
            oldAnimation = nowAnimation;
            GetComponent<Animator>().Play(nowAnimation);
        }

        if ((Input.GetButtonDown("Fire2"))) // ���콺 ������ �Է½� ȸ��
        {
            if (nowAnimation == walkUpAnime || nowAnimation == stopUpAnime)
            {
                nowAnimation = dodgeUpAnime;
                animator.Play("PilotDodgeUp");
                isDodging = true;
            }
            if (nowAnimation == walkRightUpAnime || nowAnimation == stopRightUpAnime)
            {
                nowAnimation = dodgeRightUpAnime;
                animator.Play("PilotDodgeRightUp");
                isDodging = true;
            }
            if (nowAnimation == walkRightDownAnime || nowAnimation == stopRightDownAnime)
            {
                nowAnimation = dodgeRightDownAnime;
                animator.Play("PilotDodgeRightDown");
                isDodging = true;
            }
            if (nowAnimation == walkDownAnime || nowAnimation == stopDownAnime)
            {
                nowAnimation = dodgeDownAnime;
                animator.Play("PilotDodgeDown");
                isDodging = true;
            }
            if (nowAnimation == walkLeftUpAnime || nowAnimation == stopLeftUpAnime)
            {
                nowAnimation = dodgeLeftUpAnime;
                animator.Play("PilotDodgeLeftUp");
                isDodging = true;
            }
            if (nowAnimation == walkLeftDownAnime || nowAnimation == stopLeftDownAnime)
            {
                nowAnimation = dodgeLeftDownAnime;
                animator.Play("PilotDodgeLeftDown");
                isDodging = true;
            }

        }
    }

    // (����Ƽ �ʱ� ���� ����) 0.02�ʸ��� ȣ��Ǹ�, 1�ʿ� �� 50�� ȣ��Ǵ� �Լ�
    void FixedUpdate()
    {
        // ���� ���� �ƴϸ� �ƹ� �͵� ���� ����
        if (gameState != "playing")
        {
            return;
        }

        // ���ݹ޴� ���߿� ĳ���͸� �����Ų��
        if (inDamage)
        {
            // Time.time : ���� ���ۺ��� ��������� ����ð� (�ʴ���)
            // Sin �Լ��� ���������� �����ϴ� ���� �����ϸ� 0~1~0~-1~0... ������ ��ȭ
            float value = Mathf.Sin(Time.time * 50);
            Debug.Log(value);
            if (value > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            return;
        }

        // �̵� �ӵ��� ���Ͽ� ĳ���͸� �������ش�
        rbody.velocity = new Vector2(axisH, axisV) * speed;

        
    }

    public void DodgeUpAnimationEnd()
    {
        nowAnimation = stopUpAnime;
        animator.Play("PilotStopUp");
        isDodging = false;
    }
    public void DodgeRightUpAnimationEnd()
    {
        nowAnimation = stopRightUpAnime;
        animator.Play("PilotStopRightUp");
        isDodging = false;
    }
    public void DodgeRightDownAnimationEnd()
    {
        nowAnimation = stopRightDownAnime;
        animator.Play("PilotStopRightDown");
        isDodging = false;
    }
    public void DodgeDownAnimationEnd()
    {
        nowAnimation = stopDownAnime;
        animator.Play("PilotStopDown");
        isDodging = false;
    }
    

    // p1���� p2������ ������ ����Ѵ�
    float GetAngle(Vector2 p1, Vector2 p2)
    {
        float angle;

        // �� ���⿡ ������� ĳ���Ͱ� �����̰� ���� ��� ���� ����
        if (axisH != 0 || axisV != 0)
        {
            // p1�� p2�� ���� ���ϱ� (������ 0���� �ϱ� ����)
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;

            // ��ũź��Ʈ �Լ��� ����(����) ���ϱ�
            float rad = Mathf.Atan2(dy, dx);

            // �������� ��ȯ
            angle = rad * Mathf.Rad2Deg;
        }
        else
        {
            // ĳ���Ͱ� ���� ���̸� ���� ����
            angle = angleZ;
        }
        return angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Enemy�� ���������� �浹 �߻�
        if (collision.gameObject.tag == "Enemy")
        {
            // ������ ���
            GetDamage(collision.gameObject);
        }
    }

    // ������ ���
    void GetDamage(GameObject enemy)
    {
        if (gameState == "playing")
        {
            hp--;   // HP����
            PlayerPrefs.SetInt("PlayerHP", hp); // ���� HP ����
            if (hp > 0)
            {
                // �̵� ����
                rbody.velocity = new Vector2(0, 0);
                // ��Ʈ�� (���� ������ ������ �ݴ��)
                Vector3 toPos = (transform.position - enemy.transform.position).normalized;
                rbody.AddForce(new Vector2(toPos.x * 4, toPos.y * 4), ForceMode2D.Impulse);
                // ���� ���ݹް� ����
                inDamage = true;
                Invoke("DamageEnd", 0.25f);
            }
            else
            {
                // ü���� ������ ���ӿ���
                GameOver();
            }
        }
    }

    // ������ ó�� ����
    void DamageEnd()
    {
        inDamage = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    // ���ӿ��� ó��
    void GameOver()
    {
        Debug.Log("���ӿ���!");
        gameState = "gameover";

        // ���ӿ��� ����
        // �浹 ���� ��Ȱ��ȭ
        GetComponent<CircleCollider2D>().enabled = false;
        // �̵� ����
        rbody.velocity = new Vector2(0, 0);
        // �߷��� ���� �÷��̾ ���� ��¦ ����
        rbody.gravityScale = 1;
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        // �ִϸ��̼� ����
        GetComponent<Animator>().Play(deadAnime);
        // 1�� �� ĳ���� ����
        Destroy(gameObject, 1.0f);

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
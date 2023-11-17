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

    float axisH = 0.0f;             // ���� �Է� (-1.0 ~ 1.0)
    float axisV = 0.0f;             // ���� �Է� (-1.0 ~ 1.0)
    public float angleZ = -90.0f;   // ȸ��
    float angleDodge = -90.0f;      // ������

    Rigidbody2D rbody;              // RigidBody 2D ������Ʈ
    bool isMoving = false;          // �̵� ��
    public bool isDodging = false;  // ȸ�� ��

    public bool inlobby = false;    // �κ� �ִ���

    public static int hp = 3;       // �÷��̾��� HP
    public static string gameState; // ���� ����
    bool inDamage = false;          // �ǰ� ����

    Vector2 beforePos = new Vector2(0, 0);

    public Vector3 mousePosition;

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
            axisV = Input.GetAxisRaw("Vertical");   // ����
        }

        // ���콺 ��ġ �޾ƿ���
        mousePosition = Input.mousePosition;

        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // ���콺 �Է��� ���Ͽ� �̵� ���� ���ϱ�
        Vector2 characterPt = transform.position;
        Vector2 mousePt = new Vector2(characterPt.x + mousePosition.x, characterPt.y + mousePosition.y);                      
        // Ű �Է��� ���Ͽ� �̵� ���� ���ϱ�
        Vector2 fromPt = transform.position;
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
        
        if(inlobby)
        {
            angleZ = GetAngle(fromPt, toPt);
            // �������� �̵��� �� X�� �ø�
            if (axisH < 0)
            {
                // SpriteRenderer�� flipX�� ����ϴ� ���
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (axisH >= 0) // ���������� �̵��� �� X�� �ø� ����
            {
                // SpriteRenderer�� flipX�� ����ϴ� ���
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            angleZ = GetAngle(characterPt, mousePt);

            // �������� �̵��� �� X�� �ø�
            if (mousePosition.x < 0)
            {
                // SpriteRenderer�� flipX�� ����ϴ� ���
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (mousePosition.x >= 0) // ���������� �̵��� �� X�� �ø� ����
            {
                // SpriteRenderer�� flipX�� ����ϴ� ���
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

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
            else if (angleZ > 90 && angleZ < 180)                // ����
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
            if (angleZ > -60 && angleZ < 15)     //����, �����Ʒ�
            {
                nowAnimation = stopRightDownAnime;
            }
            else if (angleZ > 0 && angleZ < 90)         // ������
            {
                nowAnimation = stopRightUpAnime;
            }
            else if (angleZ > 75 && angleZ < 105)       // ��
            {
                nowAnimation = stopUpAnime;
            }
            else if (angleZ > 90 && angleZ < 180)       // ����
            {
                nowAnimation = stopLeftUpAnime;
            }
            else if (angleZ > 145 && angleZ < 240 || angleZ < -105 && angleZ > -200)  // ��, �޹�
            {
                nowAnimation = stopLeftDownAnime;
            }
            else if (angleZ < -80 && angleZ > -100)    // �Ʒ�
            {
                nowAnimation = stopDownAnime;
            }
        }              

        // �ִϸ��̼� ����
        if (nowAnimation != oldAnimation)
        {
            oldAnimation = nowAnimation;
            GetComponent<Animator>().Play(nowAnimation);
        }

        // 8���� ���� ���ϱ�
        angleDodge = GetAngleDodge(fromPt, toPt);
        Vector2 dodgePos = new Vector2(Mathf.Cos(angleDodge), Mathf.Sin(angleDodge));
        
        // Ű�Է��� ���� ���¿��� ���콺 ��Ŭ���� ���� ���� ������
        if ((axisH != 0 || axisV != 0) && Input.GetButtonDown("Fire2"))
        {
            // ������, ������ �Ʒ��� ������
            if (angleZ > -60 && angleZ < 15)
            {
                // ȸ����
                gameState = "dodging";
                // �Էµ� �������� ������
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                // �ִϸ��̼� ����
                nowAnimation = dodgeRightDownAnime;
                animator.Play("PilotDodgeRightDown");
            }
            // ������ ���� ������
            else if (angleZ > 30 && angleZ < 60)               
            {
                // ȸ����
                gameState = "dodging";
                // �Է��� �������� ������
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                // �ִϸ��̼� ����
                nowAnimation = dodgeRightUpAnime;
                animator.Play("PilotDodgeRightUp");
            }
            // ���� ������
            else if (angleZ > 75 && angleZ < 105)             
            {
                // ȸ����
                gameState = "dodging";
                // �Է��� �������� ������
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                // �ִϸ��̼� ����
                nowAnimation = dodgeUpAnime;
                animator.Play("PilotDodgeUp");
            }
            // ���� ���� ������
            else if (angleZ > 120 && angleZ < 150)             
            {
                // ȸ����
                gameState = "dodging";
                // �Է��� �������� ������
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                // �ִϸ��̼� ����
                nowAnimation = dodgeLeftUpAnime;
                animator.Play("PilotDodgeRightUp");
            }
            // ����, ���� �Ʒ� ������
            else if (angleZ > 165 && angleZ < 240 || angleZ < -105 && angleZ > -200) 
            {
                // ȸ����
                gameState = "dodging";
                // �Է��� �������� ������
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                // �ִϸ��̼� ����
                nowAnimation = dodgeLeftDownAnime;
                animator.Play("PilotDodgeRightDown");
            }
            // �Ʒ��� ������
            else if (angleZ < -80 && angleZ > -100)         
            {
                // ȸ����
                gameState = "dodging";
                // �Է��� �������� ������
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                // �ִϸ��̼� ����
                nowAnimation = dodgeDownAnime;
                animator.Play("PilotDodgeDown");
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
        gameState = "playing";
        nowAnimation = stopUpAnime;
        animator.Play("PilotStopUp");
        isDodging = false;
    }
    public void DodgeRightUpAnimationEnd()
    {
        gameState = "playing";
        nowAnimation = stopRightUpAnime;
        animator.Play("PilotStopRightUp");
        isDodging = false;
    }
    public void DodgeRightDownAnimationEnd()
    {
        gameState = "playing";
        nowAnimation = stopRightDownAnime;
        animator.Play("PilotStopRightDown");
        isDodging = false;
    }
    public void DodgeDownAnimationEnd()
    {
        gameState = "playing";
        nowAnimation = stopDownAnime;
        animator.Play("PilotStopDown");
        isDodging = false;
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

    // p1���� p2������ ������ ����Ѵ�
    float GetAngleDodge(Vector2 p1, Vector2 p2)
    {
        float rad;

        // �� ���⿡ ������� ĳ���Ͱ� �����̰� ���� ��� ���� ����
        if (axisH != 0 || axisV != 0)
        {
            // p1�� p2�� ���� ���ϱ� (������ 0���� �ϱ� ����)
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;

            // ��ũź��Ʈ �Լ��� ����(����) ���ϱ�
            rad = Mathf.Atan2(dy, dx);
        }
        else
        {
            // ĳ���Ͱ� ���� ���̸� ���� ����
            rad = angleDodge;
        }
        return rad;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        // FallDown ���� ĳ���� ����� ��ġ ����
        if (collision.gameObject.tag == "BeforePos")
        {
            // ������� ��ġ �����ϱ�
            beforePos = gameObject.transform.position;
        }
        // FallDown �ִϸ��̼�
        if (collision.gameObject.tag == "FallDown" && !isDodging)
        {
            // ���� ���¸� �߶������� ����
            gameState = "falling";
            // �̵� ����
            rbody.velocity = new Vector2(0, 0);
            // �߶� �ִϸ��̼� ���
            animator.Play("PilotFall");

            // ������ ���
            //GetDamage(collision.gameObject);

            // �߶� �ִϸ��̼��� ����� �Ŀ� �������� �� ��ġ�� �̵��ϱ� ���� 1�� ���
            Invoke("BeforePos", 1.0f);
        }
    }

    // �߶� �ִϸ��̼� ����
    void BeforePos()
    {
        // �÷��̾��� ��ġ�� �߶� �� ����� ��ġ�� �̵�
        gameObject.transform.position = beforePos;

        // ���ӻ��¸� �ٽ� ���������� ����
        gameState = "playing";

        nowAnimation = stopDownAnime;
        // �ִϸ��̼��� �ٽ� ���
        animator.Play("PilotStopDown");
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
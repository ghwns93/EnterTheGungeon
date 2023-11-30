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

    public string deadAnime1 = "PilotDead1";
    public string deadAnime2 = "PilotDead2";
    public string fallAnime = "PilotFall";
    public string pilotOpenAnime = "PilotOpenItem";

    public GameObject deadSquareUp;     //������ ������ �������� �׸�
    public GameObject deadSquareDown;
    public GameObject deadShadow;       //������ �ؿ� �׸���
    public GameObject watch1;           //�ð�1������
    public GameObject watch2;           //�ð�2������
    public GameObject bulletBombPrefab; //�Ѿ� ������ �ִϸ��̼ǰ��� ������

    GameObject deadSquareUpObj;         //�����Լ����� �����ְ� �����ص�
    GameObject deadSquareDownObj;
    GameObject deadShadowObj;
    GameObject watch1Obj;
    GameObject watch2Obj;
    GameObject bulletBombObj;

    string nowAnimation = "";       // ���� �ִϸ��̼�
    string oldAnimation = "";       // ���� �ִϸ��̼�       

    // �ִϸ�����
    private Animator animator;

    float axisH = 0.0f;             // ���� �Է� (-1.0 ~ 1.0)
    float axisV = 0.0f;             // ���� �Է� (-1.0 ~ 1.0)
    public float angleZ = -90.0f;   // ȸ�� ����
    float angleDodge = -90.0f;      // ������
    int gunNumber;                  // GunController�� ������ ���� ������ �����Ұ�

    Rigidbody2D rbody;              // RigidBody 2D ������Ʈ
    bool isMoving = false;          // �̵� ��
    public bool isDodging = false;  // ȸ�� ��
    public bool inlobby = false;    // �κ� �ִ���

    public static int hp ;          // �÷��̾��� HP
    public static int maxHp;        // maxHp

    public string gameState;        // ���� ���� (playing, dodging, gameover)
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

        maxHp = 1;      //��� 1���ص�
        // HP �ҷ�����
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        // gameover �϶��� �ƹ� �͵� ���� ����
        if (gameState == "gameover")
        {
            return;
        }

        if (isMoving == false)
        {
            axisH = Input.GetAxisRaw("Horizontal"); // �¿�
            axisV = Input.GetAxisRaw("Vertical");   // ����
        }

        // ������ �޾ƿ���
        gunNumber = GetComponent<GunController>().gunNumber;

        // ���콺 ��ġ �޾ƿ���
        mousePosition = Input.mousePosition;

        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // ���콺 �Է��� ���Ͽ� �̵� ���� ���ϱ�
        Vector2 characterPt = transform.position;         
        
        // Ű �Է��� ���Ͽ� �̵� ���� ���ϱ�
        Vector2 fromPt = transform.position;
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);

        // �κ� ������
        if (inlobby)
        {
            angleZ = GetAngleInLobby(characterPt, toPt);
            // �������� �̵��� �� X�� �ø�
            if (axisH < 0)
            {
                // SpriteRenderer�� flipX�� ����ϴ� ���
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (axisH > 0) // ���������� �̵��� �� X�� �ø� ����
            {
                // SpriteRenderer�� flipX�� ����ϴ� ���
                GetComponent<SpriteRenderer>().flipX = false;
            }
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            angleZ = GetAngle(characterPt, mousePosition);

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
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
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
            else if (angleZ > 30 && angleZ < 90)         // ������
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
                gameState = "dodging";
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                nowAnimation = dodgeRightUpAnime;
                animator.Play("PilotDodgeRightUp");
            }
            // ���� ������
            else if (angleZ > 75 && angleZ < 105)             
            {
                gameState = "dodging";
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                nowAnimation = dodgeUpAnime;
                animator.Play("PilotDodgeUp");
            }
            // ���� ���� ������
            else if (angleZ > 120 && angleZ < 150)             
            {
                gameState = "dodging";
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                nowAnimation = dodgeLeftUpAnime;
                animator.Play("PilotDodgeRightUp");
            }
            // ����, ���� �Ʒ� ������
            else if (angleZ > 165 && angleZ < 240 || angleZ < -105 && angleZ > -200) 
            {
                gameState = "dodging";
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                nowAnimation = dodgeLeftDownAnime;
                animator.Play("PilotDodgeRightDown");
            }
            // �Ʒ��� ������
            else if (angleZ < -80 && angleZ > -100)         
            {
                gameState = "dodging";
                rbody.AddForce(dodgePos, ForceMode2D.Impulse);
                nowAnimation = dodgeDownAnime;
                animator.Play("PilotDodgeDown");
            }
        }

        // �ִϸ��̼� ����
        if (nowAnimation != oldAnimation)
        {
            oldAnimation = nowAnimation;
            GetComponent<Animator>().Play(nowAnimation);
        }
    }

    // (����Ƽ �ʱ� ���� ����) 0.02�ʸ��� ȣ��Ǹ�, 1�ʿ� �� 50�� ȣ��Ǵ� �Լ�
    void FixedUpdate()
    {
        // gameover �϶��� �ƹ� �͵� ���� ����
        if (gameState == "gameover")
        {
            return;
        }

        // ���ݹ޴� ���߿� ĳ���͸� �����Ų��
        if (inDamage)
        {
            // Time.time : ���� ���ۺ��� ��������� ����ð� (�ʴ���)
            // Sin �Լ��� ���������� �����ϴ� ���� �����ϸ� 0~1~0~-1~0... ������ ��ȭ
            float value = Mathf.Sin(Time.time * 30);
            
            if (value > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                GameObject[] handObjects = GameObject.FindGameObjectsWithTag("Hand");   //"Hand"�±��ִ� ������Ʈ�� ã��
                foreach (GameObject handObject in handObjects)      //�迭�� �ϳ�����
                {
                    // handObject�� SpriteRenderer�� ������
                    SpriteRenderer handSpriteRenderer = handObject.GetComponent<SpriteRenderer>();

                    if (handSpriteRenderer != null)
                    {
                        Color handColor = handSpriteRenderer.color;
                        handColor.a = 1;    // �������ϰ�
                        handSpriteRenderer.color = handColor;   //���� ����
                    }
                }
                // �ѿ� ���� ������ �ٲٱ�
                if (gunNumber == 1)
                    transform.Find("PilotGun(Clone)").GetComponent<SpriteRenderer>().enabled = true;
                if (gunNumber == 2)
                    transform.Find("RedGun(Clone)").GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                GameObject[] handObjects = GameObject.FindGameObjectsWithTag("Hand");   //"Hand"�±��ִ� ������Ʈ�� ã��
                foreach (GameObject handObject in handObjects)
                {
                    // handObject�� SpriteRenderer�� ������
                    SpriteRenderer handSpriteRenderer = handObject.GetComponent<SpriteRenderer>();

                    if (handSpriteRenderer != null)
                    {
                        Color handColor = handSpriteRenderer.color;
                        handColor.a = 0;    // �����ϰ�
                        handSpriteRenderer.color = handColor;
                    }
                }
                if(gunNumber ==1)
                    transform.Find("PilotGun(Clone)").GetComponent<SpriteRenderer>().enabled = false;
                if (gunNumber == 2)
                    transform.Find("RedGun(Clone)").GetComponent<SpriteRenderer>().enabled = false;
            }
            
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
    public float GetAngle(Vector2 p1, Vector2 p2)
    {
        float angle;

        // p1�� p2�� ���� ���ϱ� (������ 0���� �ϱ� ����)
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;

        // ��ũź��Ʈ �Լ��� ����(����) ���ϱ�
        float rad = Mathf.Atan2(dy, dx);

        // �������� ��ȯ
        angle = rad * Mathf.Rad2Deg;

        return angle;
    }

    float GetAngleInLobby(Vector2 p1, Vector2 p2)
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy�� ���������� �浹 �߻�
        if ((collision.gameObject.tag == "Enemy"|| collision.gameObject.tag == "EnemyBullet") && gameState =="playing")
        {
            // ������ ���
            GetDamage(collision.gameObject);

            // �Ѿ˿� �¾������ �Ѿ� ����
            if(collision.gameObject.tag == "EnemyBullet")
            {
                bulletBombObj = Instantiate(bulletBombPrefab,
                    collision.gameObject.transform.position,collision.gameObject.transform.rotation);   //�Ǵ��� Ȯ�ξ��غ�
                Destroy(collision.gameObject);
            }
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

            // �κ� �ƴҰ�� ������ ���
            if(!inlobby)
            {
                hp--;
                if (hp <= 0)
                {
                    // ü���� ������ ���ӿ���
                    GameOver();
                }
            }
                

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
        hp--;   // HP����
        gameObject.GetComponent<Collider2D>().enabled = false;  // �ݶ��̴� ��Ȱ��ȭ�ؼ� ��������ȿ��

        if (gameState=="playing")
        {
            if (hp > 0)
            {               
                // ���� ���ݹް� ����
                inDamage = true;    //inDamage == true �ϰ�� FixedUpdate���� if(inDamage)���ǹ� ����
                Invoke("DamageEnd", 2.0f);
            }
            else
            {
                // ü���� ������ ���ӿ���
                GameOver();     //�������� ������ �����̿��� �ð��Ѿȸ����� ���ľ���
            }
        }
    }

    // ������ ó�� ����
    void DamageEnd()
    {
        inDamage = false;
        gameObject.GetComponent<Collider2D>().enabled = true;  // �ݶ��̴� ���ܼ� ����Ǯ��

        // �÷��̾� �����ϸ� ����
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        GameObject[] handObjects = GameObject.FindGameObjectsWithTag("Hand");
        foreach (GameObject handObject in handObjects)
        {
            // handObject�� SpriteRenderer�� ������
            SpriteRenderer handSpriteRenderer = handObject.GetComponent<SpriteRenderer>();
                        
            if (handSpriteRenderer != null)
            {
                Color handColor = handSpriteRenderer.color;
                handColor.a = 1;    // �� ���̰�
                handSpriteRenderer.color = handColor;
            }
        }
        // �ѿ����� ������ �ٲٱ�
        if (gunNumber == 1)
            transform.Find("PilotGun(Clone)").GetComponent<SpriteRenderer>().enabled = true;
        if (gunNumber == 2)
            transform.Find("RedGun(Clone)").GetComponent<SpriteRenderer>().enabled = true;
    }

    //���ӿ��� ó��
    void GameOver()
    {
        gameState = "gameover";
        // ���ӿ��� ����

        // �̵� ����
        rbody.velocity = new Vector2(0, 0);
        // �ִϸ��̼� ����        
        animator.Play(deadAnime1);
        Invoke("AfterDead", 3.0f);

        // ������ �������� �����ڽ�
        deadSquareUpObj = Instantiate(deadSquareUp);
        deadSquareUpObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -5.0f), ForceMode2D.Impulse);
        // �Ʒ����� �ö���� �����ڽ�
        deadSquareDownObj = Instantiate(deadSquareUp,new Vector3(0,-7f,0),new Quaternion(0,0,0,0));
        deadSquareDownObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, +5.0f),ForceMode2D.Impulse);
        Invoke("StopSquare", 0.5f);

        Destroy(transform.Find("PilotShadow").gameObject);  // �÷��̾� �׸��� ����
        // �÷��̾� �ؿ� ū �׸��� ����
        deadShadowObj = Instantiate(deadShadow,transform.position+new Vector3(0,0.2f,0),transform.rotation);
                
    }
    void StopSquare()
    {
        //�ö���ų� �������°� ����
        deadSquareUpObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        deadSquareDownObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
    void AfterDead()
    {
        //�ð質����
        watch1Obj = Instantiate(watch1, transform.position, transform.rotation);
        Invoke("WatchShot", 1.5f);
    }

    //�̺�Ʈ�Լ�
    void WatchShot()
    {
        Destroy(watch1Obj);
        watch2Obj = Instantiate(watch2, transform.position , transform.rotation); 
        animator.Play(deadAnime2);      //�ð��� �°� �������� �ִϸ��̼�
        Destroy(watch2Obj, 1.0f);
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // 변경할 스프라이트를 Inspector에서 설정
    //public Sprite PilotGun; 
    //public Sprite PilotGunReady;
    //public Sprite PilotGunFire;
    //public Sprite PilotGunReload;
    //public Sprite PilotGunReturn1;
    //public Sprite PilotGunReturn2;

    // 현재 애니메이션
    string nowGunAnimation = "";
    // 이전 애니메이션
    string oldGunAnimation = "";

    // 애니메이터
    private Animator gunAnimator;

    public string pilotGunHold = "PilotGunHold";
    public string pilotGunReady = "PilotGunReady";
    public string pilotGunFire = "PilotGunFire";
    public string pilotGunReturn = "PilotGunReturn";
    public string PilotGunReload = "PilotGunReload";

    public float shootSpeed = 12.0f;    //화살 속도
    public float shootDelay = 0.25f;    //발사 간격

    public GameObject gunPrefab;        //총
    public GameObject bulletPrefab;     //총알
    public GameObject OtherHandPrefab;  //총든손 반대편 손
    GameObject gunGateObj;              //포구
    GameObject LeftHandObj;             //왼손
    GameObject RightHandObj;            //오른손

    private bool canAttack = true;      //공격 딜레이 할때 사용

    GameObject gunObj;      //총
    bool isLeftHand;        //왼손있는지
    bool isRightHand;       //오른손있는지


    // Start is called before the first frame update
    void Start()
    {
        // 총을 플레이어 위치에 배치
        Vector3 pos = transform.position;
        gunObj = Instantiate(gunPrefab, pos, Quaternion.identity);
        gunObj.transform.SetParent(transform);  //플레이어 객체를 총 객체의 부모로 설정

        // 포구에 배치한 오브젝트 가져오기
        Transform tr = gunObj.transform.Find("GunGate");
        gunGateObj = tr.gameObject;

        // (기본) 애니메이션 설정
        oldGunAnimation = pilotGunHold;

        // 애니메이터 가져오기
        gunAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 총 회전과 우선순위 판정
        SpriteRenderer gunSpr = gunObj.GetComponent<SpriteRenderer>();          //총의 SpriteRenderer
        Transform childTransform = gunObj.transform.Find("PilotHand");          //자식에 접근하기위해
        SpriteRenderer handSpr = childTransform.GetComponent<SpriteRenderer>(); //손의 SpriteRenderer
        PlayerController plmv = GetComponent<PlayerController>();               //player의 SpriteRenderer

        if (plmv.angleZ > 30 && plmv.angleZ < 150)  // 윗방향
        {
            // 총,활 우선순위
            gunSpr.sortingOrder = 0;
            handSpr.sortingOrder = 0;
        }
        else 
        {
            gunSpr.sortingOrder = 5;    //캐릭터 OrderInLayer == 4
            handSpr.sortingOrder = 6;   //손이 총보다위            
        }

        // playerController 변수 가져오기
        Vector3 mousePosition = FindObjectOfType<PlayerController>().mousePosition; 

        // 총,손,총구 위치,회전
        if (mousePosition .x> transform.position.x) // 마우스가 캐릭터 오른쪽에 있을때
        {
            gunObj.GetComponent<SpriteRenderer>().flipY = false;                                    //반전취소
            gunObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ-90);                     //마우스에 따라 총회전
            gunObj.transform.position = transform.position + new Vector3(0.2f, -0.15f, 0);          //총위치 캐릭터 오른쪽으로

            gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad), 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad)+0.2f);//총구 위치
            if (plmv.angleZ < -45 && plmv.angleZ > -135)
                gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad) + 0.15f, 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad));//밑에 볼때 총구위치 조정

            childTransform.position = transform.position + new Vector3(0.2f, -0.15f, 0);            //손위치
            childTransform.rotation = Quaternion.Euler(0, 0, 0);                                    //손회전 x                        
            if(!isLeftHand)
            {
                LeftHandObj = Instantiate(OtherHandPrefab,
                    transform.position + new Vector3(-0.2f, -0.15f, 0), Quaternion.Euler(0, 0, 0)); //왼손 생성
                isLeftHand = true;
            }
            LeftHandObj.transform.position = transform.position + new Vector3(-0.2f, -0.15f, 0);    //왼손 캐릭터에 붙어다니게
            Destroy(RightHandObj);                                                                  //오른손 삭제
            isRightHand = false;
        }
        else // 마우스가 캐릭터 왼쪽에 있을때
        {
            gunObj.GetComponent<SpriteRenderer>().flipY = true;                                     //반전           
            gunObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ+90);                     //마우스에 따라 총회전
            gunObj.transform.position = transform.position + new Vector3(-0.2f, -0.15f, 0);         //총위치 캐릭터 왼쪽으로

            gunGateObj.transform.position = gunObj.transform.position + 
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad), 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad)+0.2f);//총구 위치
            if(plmv.angleZ<-45 && plmv.angleZ>-135)
                gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad) -0.15f, 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad) );//밑에 볼때 총구위치 조정

            childTransform.position = transform.position + new Vector3(-0.2f, -0.15f, 0);           //손위치
            childTransform.rotation = Quaternion.Euler(0, 0, 0);                                    //손회전 x
            if (!isRightHand)                                                               
            {
                RightHandObj = Instantiate(OtherHandPrefab,
                    transform.position + new Vector3(0.2f, -0.15f, 0), Quaternion.Euler(0, 0, 0));  //오른손 생성
                isRightHand = true;
            }
            RightHandObj.transform.position = transform.position + new Vector3(+0.2f, -0.15f, 0);   //오른손 캐릭터에 붙어다니게
            Destroy(LeftHandObj);                                                                   //왼손 삭제
            isLeftHand = false;


            gunObj.GetComponent<Animator>().Play("pilotGunReload");
        }

        //if(isDodging)// 마우스 오른쪽 입력시 총안보이게
        //{

        //}

        if (Input.GetButton("Fire1") && canAttack)
        {
            //gunSpr.sprite = PilotGunReady;
            //nowGunAnimation = pilotGunReady;

            //// 애니메이션 변경
            //if (nowGunAnimation != oldGunAnimation)
            //{
            //    oldGunAnimation = nowGunAnimation;
            //    gunAnimator.Play(nowGunAnimation);
            //}
            gunAnimator.Play(pilotGunReady);

            // 공격 키 입력 및 딜레이 시작
            StartCoroutine(AttackWithDelay());
        }

    }

    void FixedUpdate()
    {        
    }

    IEnumerator AttackWithDelay()
    {
        // 공격 수행
        Attack();
        //gunObj.GetComponent<SpriteRenderer>().sprite = PilotGunReady;

        // 딜레이 설정 
        canAttack = false;
        yield return new WaitForSeconds(shootDelay);

        // 딜레이 후에 다시 공격 가능으로 설정
        canAttack = true;
    }

    // 총알 발사
    public void Attack()
    {     
        PlayerController playerCnt = GetComponent<PlayerController>();
        // 회전에 사용할 각도
        float angleZ = playerCnt.angleZ;
        // 총알 생성 위치
        Vector3 pos = new Vector3(gunGateObj.transform.position.x,
                                          gunGateObj.transform.position.y,
                                          transform.position.z);

        // 총알 오브젝트 생성 (캐릭터 진행 방향으로 회전)
        Quaternion r = Quaternion.Euler(0, 0, angleZ + 90);
        GameObject bulletObj = Instantiate(bulletPrefab, pos, r);        

        // 화살을 발사하기 위한 벡터 생성
        float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);
        float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);
        Vector3 v = new Vector3(x, y) * shootSpeed;

        // 지정한 각도와 방향으로 화살을 발사
        Rigidbody2D body = bulletObj.GetComponent<Rigidbody2D>();
        body.AddForce(v, ForceMode2D.Impulse);

        //gunObj.GetComponent<SpriteRenderer>().sprite = PilotGunFire;        
        
    }

}


/*
클릭시 총색바뀌고-누르면 색 바뀌게하고
총쏘기- 딜레이에 총쏘는 애니메이션

총 꾹누르는 경우
총 단발쏠때

마우스 손때면 돌아가는 애니메이션
 */
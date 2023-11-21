using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // 총 애니메이터
    private Animator gunAnimator;

    // 애니메이션
    public string pilotGunHold = "PilotGunHold";
    public string pilotGunReady = "PilotGunReady";
    public string pilotGunFire = "PilotGunFire";
    public string pilotGunReturn = "PilotGunReturn";
    public string pilotGunReload = "PilotGunReload";

    public float shootSpeed = 12.0f;    //화살 속도
    public float shootDelay = 1.0f;    //발사 간격

    public GameObject gunPrefab;        //총 프리팹
    public GameObject bulletPrefab;     //총알 프리팹
    public GameObject OtherHandPrefab;  //총든손 반대편 손 프리팹
    GameObject gunGateObj;              //포구
    GameObject LeftHandObj;             //왼손
    GameObject RightHandObj;            //오른손

    private bool canAttack = true;      //공격 딜레이 할때 사용

    GameObject gunObj;      //총오브젝트
    bool isLeftHand;        //왼손있는지
    bool isRightHand;       //오른손있는지

    PlayerController playerController;  //PlayerController의 함수,변수 사용하기위해 선언해둠

    Vector3 mousePosition;

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

        // 애니메이터 가져오기
        gunAnimator = gunObj.GetComponent<Animator>();

        // (기본) 애니메이션 설정
        gunAnimator.Play(pilotGunHold);

    }

    // Update is called once per frame
    void Update()
    {        
        SpriteRenderer gunSpr = gunObj.GetComponent<SpriteRenderer>();          //총의 SpriteRenderer
        Transform childTransform = gunObj.transform.Find("PilotHand");          //자식에 접근하기위해
        SpriteRenderer handSpr = childTransform.GetComponent<SpriteRenderer>(); //손의 SpriteRenderer
        PlayerController plmv = GetComponent<PlayerController>();               //player의 SpriteRenderer
        
        // playerController의 마우스 포지션 변수 가져오기
        mousePosition = FindObjectOfType<PlayerController>().mousePosition; 

        // 총,손,총구 위치,회전
        if (mousePosition .x> transform.position.x) // 마우스가 캐릭터 오른쪽에 있을때
        {
            gunObj.GetComponent<SpriteRenderer>().flipY = false;                                    //반전취소
            gunObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ-90);                     //마우스에 따라 총회전
            gunObj.transform.position = transform.position + new Vector3(0.2f, -0.15f, 0);          //총위치 캐릭터 오른쪽으로

            gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad), 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad)+0.1f);//총구 위치
            if (plmv.angleZ < -45 && plmv.angleZ > -135)
                gunGateObj.transform.position = gunObj.transform.position +
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad) + 0.15f, 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad));//밑에 볼때 총구위치 조정

            childTransform.position = transform.position + new Vector3(0.2f, -0.15f, 0);            //손위치
            childTransform.rotation = Quaternion.Euler(0, 0, 0);                                    //손회전 x                        
            if(!isLeftHand)
            {
                isLeftHand = true;
                LeftHandObj = Instantiate(OtherHandPrefab,
                    transform.position + new Vector3(-0.2f, -0.15f, 0), Quaternion.Euler(0, 0, 0)); //왼손 생성                
            }
            LeftHandObj.transform.position = transform.position + new Vector3(-0.2f, -0.15f, 0);    //왼손 캐릭터에 붙어다니게
            Destroy(RightHandObj);  //총에 손이 있어서 원래 있던 손 삭제
            isRightHand = false;
            if (GetComponent<PlayerController>().inlobby)   //로비에서 손2개
            {
                if (!isRightHand)
                {
                    isRightHand = true;
                    RightHandObj = Instantiate(OtherHandPrefab,
                        transform.position + new Vector3(0.2f, -0.15f, 0), Quaternion.Euler(0, 0, 0));  //오른손 생성
                }
                RightHandObj.transform.position = transform.position + new Vector3(+0.2f, -0.15f, 0);   //오른손 캐릭터에 붙어다니게
            }
        }
        else // 마우스가 캐릭터 왼쪽에 있을때
        {
            gunObj.GetComponent<SpriteRenderer>().flipY = true;                                     //반전           
            gunObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ+90);                     //마우스에 따라 총회전
            gunObj.transform.position = transform.position + new Vector3(-0.2f, -0.15f, 0);         //총위치 캐릭터 왼쪽으로

            gunGateObj.transform.position = gunObj.transform.position + 
                new Vector3(0.5f * Mathf.Cos(plmv.angleZ * Mathf.Deg2Rad), 0.5f * Mathf.Sin(plmv.angleZ * Mathf.Deg2Rad)+0.1f);//총구 위치
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
            Destroy(LeftHandObj);   //총에 손이 있어서 원래 있던 손 삭제
            isLeftHand = false;

            if (GetComponent<PlayerController>().inlobby)   //로비에서 손2개 생성
            {
                if (!isLeftHand)
                {
                    LeftHandObj = Instantiate(OtherHandPrefab,
                        transform.position + new Vector3(-0.2f, -0.15f, 0), Quaternion.Euler(0, 0, 0)); //왼손 생성
                    isLeftHand = true;
                }
                LeftHandObj.transform.position = transform.position + new Vector3(-0.2f, -0.15f, 0);    //왼손 캐릭터에 붙어다니게
            }                                                
        }

        // 우선순위 판정
        if (plmv.angleZ > 30 && plmv.angleZ < 150)  // 윗방향
        {
            // 총,활 우선순위
            gunSpr.sortingOrder = 1;
            handSpr.sortingOrder = 1;
            if(RightHandObj)
                RightHandObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
            if (LeftHandObj)
                LeftHandObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else
        {
            gunSpr.sortingOrder = 5;    //캐릭터 OrderInLayer == 4
            handSpr.sortingOrder = 6;   //손이 총보다위
            if (RightHandObj)
                RightHandObj.GetComponent<SpriteRenderer>().sortingOrder = 6;
            if (LeftHandObj)
                LeftHandObj.GetComponent<SpriteRenderer>().sortingOrder = 6;
        }
        if (GetComponent<PlayerController>().gameState == "falling")    //떨어지고 있을때
        {
            Destroy(RightHandObj);                                                                  //오른손 삭제
            isRightHand = false;
            Destroy(LeftHandObj);                                                                   //왼손 삭제
            isLeftHand = false;
        }
                
        if (GetComponent<PlayerController>().isDodging)// 마우스 오른쪽 입력시 총안보이게
        {
            Destroy(RightHandObj);                                                                  //오른손 삭제
            isRightHand = false;
            Destroy(LeftHandObj);                                                                   //왼손 삭제
            isLeftHand = false;
        }
                    

        if (Input.GetButton("Fire1") && canAttack&&!GetComponent<PlayerController>().inlobby)
        {
            
            gunAnimator.Play(pilotGunReady);

            // 공격 키 입력 및 딜레이 시작
            StartCoroutine(AttackWithDelay());
        }

        if( Input.GetButtonUp("Fire1") && !GetComponent<PlayerController>().inlobby)
        {
            gunAnimator.Play(pilotGunReturn);
        }
    }

    void FixedUpdate()
    {        
    }

    IEnumerator AttackWithDelay()
    {
        // 공격 수행
        Attack();

        // 딜레이 설정 
        canAttack = false;
        yield return new WaitForSeconds(shootDelay);

        // 딜레이 후에 다시 공격 가능으로 설정
        canAttack = true;
    }

    // 총알 발사
    public void Attack()
    {
        gunAnimator.StopPlayback();             // 지금애니메이션 즉시중지
        gunAnimator.Play(pilotGunFire, -1, 0f); // 애니메이션 키포인트 처음으로 이동
        gunAnimator.Play(pilotGunFire);         // 애니메이션 실행

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

    }

}


/*

총 발사하면 순서대로 10도정도씩? 각도 변경 약간밑으로 해야될듯?

어느정도 거리이동하면면 총알 없애기

총알 개수 다쓰면 재장전 머리위에 뜨고 총알발사 못하게
장전하면 바생기고 바 애니메이션,총 애니메이션

총쏠때 이펙트
총알 부딛히면 없애고 이펙트
마우스 크로스헤어

총쏘면 화면 흔들리기
(흔들림 수준 설정 옵션)

아이템 키퍼

//// 총구 위치
        //Vector2 fromPos = new Vector2(gunGateObj.transform.position.x, gunGateObj.transform.position.y);
        //// 마우스 위치
        //Vector2 toPos = new Vector2(mousePosition.x, mousePosition.y);
        //// 화살발사 각도
        //float angleCharacterwithMouse;
        //float dx = toPos.x - fromPos.x;
        //float dy = toPos.y - fromPos.y;

        //// 아크탄젠트 함수로 각도(라디안) 구하기
        //float rad = Mathf.Atan2(dy, dx);

        //// 라디안으로 변환
        //angleCharacterwithMouse = rad * Mathf.Rad2Deg;
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    // 애니메이션
    public string gunHoldAnime = "PilotGunHold";

    // 현재 애니메이션
    string nowGunAnimation = "";
    // 이전 애니메이션
    string oldGunAnimation = "";

    // 애니메이터
    private Animator gunanimator;

    public float shootSpeed = 12.0f;    //화살 속도
    public float shootDelay = 0.25f;    //발사 간격

    public GameObject gunPrefab;        //활
    public GameObject bulletPrefab;     //화살

    private bool canAttack = true;

    bool inAttack = false;  //공격 상태 판단
    GameObject gunObj;      //총


    // Start is called before the first frame update
    void Start()
    {
        // (기본) 애니메이션 설정
        oldGunAnimation = gunHoldAnime;

        // 애니메이터 가져오기
        gunanimator = GetComponent<Animator>();

        // 활을 플레이어 위치에 배치
        Vector3 pos = transform.position;
        gunObj = Instantiate(gunPrefab, pos, Quaternion.identity);
        gunObj.transform.SetParent(transform);  //플레이어 객체를 총 객체의 부모로 설정

    }

    // Update is called once per frame
    void Update()
    {
        // 깃허브에 에러난 상태에서 저장하셔서 다른사람들 빌드가 안됨
        // 수정하셔야 합니다.


        //if(isDodging)
        //{

        //}

        //if (nowAnimation == walkRightDownAnime)                     //오른, 오른아래
        //{

        //}
        //else if (nowAnimation == walkRightUpAnime)                 // 오른위
        //{

        //}
        //else if (nowAnimation == walkUpAnime)                // 위
        //{

        //}
        //else if (nowAnimation == walkLeftUpAnime)               // 왼위
        //{

        //}
        //else if (nowAnimation == walkLeftDownAnime)   // 왼, 왼밑
        //{

        //}
        //else if (nowAnimation == walkDownAnime)              // 아래
        //{

        //}

        //if (nowAnimation == stopRightDownAnime)     //오른, 오른아래
        //{

        //}
        //else if (nowAnimation == stopRightUpAnime)         // 오른위
        //{

        //}
        //else if (nowAnimation == stopUpAnime)       // 위
        //{

        //}
        //else if (nowAnimation == stopLeftUpAnime)       // 왼위
        //{

        //}
        //else if (nowAnimation == stopLeftDownAnime) // 왼, 왼밑
        //{

        //}
        //else if (nowAnimation == stopDownAnime)     // 아래
        //{

        //}

        //// 왼쪽으로 이동할 때 X축 플립
        //if (axisH < 0)
        //{
        //    // SpriteRenderer의 flipX를 사용하는 경우
        //    GetComponent<SpriteRenderer>().flipX = true;

        //    // Transform의 Rotation을 사용하는 경우
        //    //transform.rotation = Quaternion.Euler(0, 180, 0);
        //}
        //else if (axisH > 0) // 오른쪽으로 이동할 때 X축 플립 해제
        //{
        //    // SpriteRenderer의 flipX를 사용하는 경우
        //    GetComponent<SpriteRenderer>().flipX = false;

        //    // Transform의 Rotation을 사용하는 경우
        //    //transform.rotation = Quaternion.Euler(0, 0, 0);
        //}

        //// 애니메이션 변경
        //if (nowAnimation != oldAnimation)
        //{
        //    oldAnimation = nowAnimation;
        //    GetComponent<Animator>().Play(nowAnimation);
        //}

        //if ((Input.GetButtonDown("Fire2"))) // 마우스 오른쪽 입력시 회피
        //{

        //}

        if (Input.GetButton("Fire1") && canAttack)
        {
            // 공격 키 입력 및 딜레이 시작
            StartCoroutine(AttackWithDelay());
        }

        // 활 회전과 우선순위 판정
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
        
        // 총 회전
        gunObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ);        
        
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
        yield return new WaitForSeconds(0.2f);

        // 딜레이 후에 다시 공격 가능으로 설정
        canAttack = true;
    }

    public void Attack()
    {
        inAttack = true;            //공격 상태 변경

        // 화살 발사
        PlayerController playerCnt = GetComponent<PlayerController>();
        // 회전에 사용할 각도
        float angleZ = playerCnt.angleZ;
        // 화살 오브젝트 생성 (캐릭터 진행 방향으로 회전)
        Quaternion r = Quaternion.Euler(0, 0, angleZ + 90);
        GameObject bulletObj = Instantiate(bulletPrefab, transform.position, r);

        // 화살을 발사하기 위한 벡터 생성
        float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);
        float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);
        Vector3 v = new Vector3(x, y) * shootSpeed;

        // 지정한 각도와 방향으로 화살을 발사
        Rigidbody2D body = bulletObj.GetComponent<Rigidbody2D>();
        body.AddForce(v, ForceMode2D.Impulse);

        // 딜레이 설정
        Invoke("StopAttack", shootDelay);
    }

    // 공격 중지
    public void StopAttack()
    {
        inAttack = false;
    }

}

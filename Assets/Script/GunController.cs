using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : PlayerController
{

    // 애니메이션
    public string gunHoldAnime = "PilotGunHold";

    // 현재 애니메이션
    string nowGunAnimation = "";
    // 이전 애니메이션
    string oldGunAnimation = "";

    // 애니메이터
    private Animator gunanimator;


    // Start is called before the first frame update
    void Start()
    {
        // (기본) 애니메이션 설정
        oldGunAnimation = gunHoldAnime;

        // 애니메이터 가져오기
        gunanimator = GetComponent<Animator>();

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
    }

    // (유니티 초기 설정 기준) 0.02초마다 호출되며, 1초에 총 50번 호출되는 함수
    void FixedUpdate()
    {
        
    }


}

// 키 입력 관련 함수 목록
/*
    // 키보드의 특정 키 입력에 대한 검사
    bool down = Input.GetKeyDown(KeyCode.Space);
    bool press = Input.GetKey(KeyCode.Space);
    bool up = Input.GetKeyUp(KeyCode.Space);

    // 마우스 버튼 입력 및 터치 이벤트에 대한 검사
    // 0 : 마우스 왼쪽 버튼
    // 1 : 마우스 오른쪽 버튼
    // 2 : 마우스 휠 버튼
    bool down = Input.GetMouseButtonDown(0);
    bool press = Input.GetMouseButton(0);
    bool up = Input.GetMouseButtonUp(0);

    // Input Manager에서 설정한 문자열을 기반으로 하는 키 입력 검사
    bool down = Input.GetButtonDown("Jump");
    bool press = Input.GetButton("Jump");
    bool up = Input.GetButtonUp("Jump");

    // 가상의 축에 대한 키 입력 검사
    float axisH = Input.GetAxis("Horizontal");
    float axisV = Input.GetAxisRaw("Vertical");
*/
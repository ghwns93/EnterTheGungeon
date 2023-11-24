using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPilotGunController : MonoBehaviour
{
    // 총 애니메이터
    private Animator gunAnimator;

    // 애니메이션
    private string pilotGunHold = "PilotGunHold";
    private string pilotGunReady = "PilotGunReady";
    private string BoxPilotGunFire = "BoxPilotGunFire";
    private string pilotGunReturn = "PilotGunReturn";
    private string pilotGunReload = "PilotGunReload";

    
    // 플레이어의 총 애니메이터
    private Animator PlayerGunAnimator;

    private int bulletCount;           //총알 개수

    private bool canAttack;     //공격 딜레이 할때 사용
    private bool isReloading;  //장전하는중 장전 안되게
    private bool isAttack;     //공격누르고있을때 장전안되게
    private bool inlobby;              //PlayerController의 변수 가져와서 저장할 변수 //일단 false

    // Start is called before the first frame update
    void Start()
    {
        gunAnimator = GetComponent<Animator>();
        // 기본 애니메이션 설정
        gunAnimator.Play(pilotGunReady);

        bulletCount = 8;
        canAttack = true;
        isReloading = false;
        isAttack = false;
        inlobby = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 왼클릭시 공격
        if (Input.GetMouseButton(0) && canAttack && !inlobby && bulletCount > 0 && !isReloading)
        {
            gunAnimator.Play(pilotGunHold);
            isAttack = true;

            // 공격 키 입력 및 딜레이 시작
            StartCoroutine(AttackWithDelay());
        }

        // 마우스 왼클릭을 땔때
        if (Input.GetMouseButtonUp(0) && !inlobby && !isReloading)
        {
            gunAnimator.Play(pilotGunReturn);
            isAttack = false;
        }

        // 총알 다썼을 때 마우스 왼클릭시 장전
        if (Input.GetMouseButtonDown(0) && canAttack && !inlobby && bulletCount == 0)
        {
            isReloading = true;
            canAttack = false;
            gunAnimator.Play(pilotGunReload, 0, 0f);  // 애니메이션 키포인트 처음으로 이동후 실행            
            Invoke("ChangeVariable", 1.3f);  //1.3초뒤 isReloading=false ,canAttack =true로 바꾸는함수 실행
            bulletCount = 8;
        }        

        // R키누르면 장전
        if (Input.GetKeyDown(KeyCode.R) && !inlobby && !isReloading && bulletCount < 8)
        {
            isReloading = true;
            canAttack = false;            
            gunAnimator.Play(pilotGunReload, 0, 0f);  // 애니메이션 키포인트 처음으로 이동후 실행
            Invoke("ChangeVariable", 1.3f);  //1.3초뒤 isReloading=false ,canAttack =true로 바꾸는함수 실행
            bulletCount = 8;
        }
    }

    IEnumerator AttackWithDelay()
    {
        // 공격 수행
        Attack();
        // 딜레이 설정 
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        // 딜레이 후에 다시 공격 가능으로 설정
        canAttack = true;
    }
    private void Attack()
    {
        bulletCount--;
        gunAnimator.Play(BoxPilotGunFire, 0, 0f);  // 애니메이션 키포인트 처음으로 이동후 실행
    }
    private void ChangeVariable()
    {
        isReloading = false;
        canAttack = true;
    }
}

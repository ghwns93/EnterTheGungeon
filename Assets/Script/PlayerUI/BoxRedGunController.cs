using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRedGunController : MonoBehaviour
{

    // 총 애니메이터
    private Animator gunAnimator;

    // 애니메이션
    private string redGunHold = "RedGunHold";
    private string redGunFire = "RedGunFire";
    private string redGunReload = "RedGunReload";


    // 플레이어의 총 애니메이터
    private Animator PlayerGunAnimator;

    private int redGunBulletCount;           //총알 개수

    private bool canAttack;     //공격 딜레이 할때 사용
    private bool isReloading;  //장전하는중 장전 안되게
    private bool isAttack;     //공격누르고있을때 장전안되게
    private bool inlobby;              //PlayerController의 변수 가져와서 저장할 변수 //일단 false


    // Start is called before the first frame update
    void Start()
    {
        gunAnimator = GetComponent<Animator>();

        // 기본 애니메이션 설정
        gunAnimator.Play(redGunHold);

        redGunBulletCount = 20;
        canAttack = true;
        isReloading = false;
        isAttack = false;
        inlobby = false;
    }

    // Update is called once per frame
    void Update()
    {
        // gunNumber 변수 가져오기
        GameObject player = GameObject.FindWithTag("Player");
        int gunNumber = player.GetComponent<GunController>().gunNumber;
        redGunBulletCount = player.GetComponent<GunController>().redGunBulletCount;
        string gameState = player.GetComponent<PlayerController>().gameState;

        // gameover 일때는 아무 것도 하지 않음
        if (gameState == "gameover")
        {
            return;
        }

        // 플레이어 가진 총에 따라 박스 ui 총 보이거나 안보이게
        if (gunNumber == 2)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else 
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        // 마우스 왼클릭시 공격
        if (Input.GetMouseButton(0) && canAttack && !inlobby && redGunBulletCount > 0 && !isReloading)
        {
            isAttack = true;

            // 공격 키 입력 및 딜레이 시작
            StartCoroutine(AttackWithDelay());
        }

        // 마우스 왼클릭을 땔때
        if (Input.GetMouseButtonUp(0) && !inlobby && !isReloading)
        {
            gunAnimator.Play(redGunHold);
            isAttack = false;
        }

        // 총알 다썼을 때 마우스 왼클릭시 장전
        if (Input.GetMouseButtonDown(0) && canAttack && !inlobby && redGunBulletCount == 0)
        {
            isReloading = true;
            canAttack = false;
            gunAnimator.Play(redGunReload, 0, 0f);  // 애니메이션 키포인트 처음으로 이동후 실행            
            Invoke("ChangeVariable", 1.3f);  //1.3초뒤 isReloading=false ,canAttack =true로 바꾸는함수 실행
        }

        // R키누르면 장전
        if (Input.GetKeyDown(KeyCode.R) && !inlobby && !isReloading && redGunBulletCount < 20)
        {
            isReloading = true;
            canAttack = false;
            gunAnimator.Play(redGunReload, 0, 0f);  // 애니메이션 키포인트 처음으로 이동후 실행
            Invoke("ChangeVariable", 1.3f);  //1.3초뒤 isReloading=false ,canAttack =true로 바꾸는함수 실행
        }
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
    private void Attack()
    {
        gunAnimator.Play(redGunFire, 0, 0f);  // 애니메이션 키포인트 처음으로 이동후 실행
    }
    private void ChangeVariable()
    {
        isReloading = false;
        canAttack = true;
    }
}

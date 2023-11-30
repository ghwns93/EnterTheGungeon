using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGunMagazine : MonoBehaviour
{
    int redGunBulletCount;
    Transform redBulletSquareTransform1;
    Transform redBulletSquareTransform2;
    Transform redBulletSquareTransform3;
    Transform redBulletSquareTransform4;
    Transform redBulletSquareTransform5;
    Transform redBulletSquareTransform6;
    Transform redBulletSquareTransform7;
    Transform redBulletSquareTransform8;
    Transform redBulletSquareTransform9;
    Transform redBulletSquareTransform10;
    Transform redBulletSquareTransform11;
    Transform redBulletSquareTransform12;
    Transform redBulletSquareTransform13;
    Transform redBulletSquareTransform14;
    Transform redBulletSquareTransform15;
    Transform redBulletSquareTransform16;
    Transform redBulletSquareTransform17;
    Transform redBulletSquareTransform18;
    Transform redBulletSquareTransform19;
    Transform redBulletSquareTransform20;

    private bool inlobby;      //PlayerController의 변수 가져와서 저장할 변수 

    // Start is called before the first frame update
    void Start()
    {
        // inlobby 변수 가져오기
        inlobby = GameObject.Find("Pilot").GetComponent<PlayerController>().inlobby;

    }

    // Update is called once per frame
    void Update()
    {
        // 로비에 있으면 캔버스 안보이게하고(캔버스 컨트롤러에서) 조건문 이후 실행 x
        if (inlobby)
        {
            return;
        }

        // gunNumber 변수 가져오기
        GameObject player = GameObject.FindWithTag("Player");
        int gunNumber = player.GetComponent<GunController>().gunNumber;

        // 플레이어 가진 총에 따라 창탄 보이거나 안보이게
        if (gunNumber == 2)
        {
            foreach (Transform childTransform in transform)
            {
                // 자식 오브젝트를 직접 순회하며 처리
                childTransform.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform childTransform in transform)
            {
                childTransform.gameObject.SetActive(false);
            }
        }

        redGunBulletCount = player.GetComponent<GunController>().redGunBulletCount;

        redBulletSquareTransform1 = transform.Find("BulletSquare1");
        redBulletSquareTransform2 = transform.Find("BulletSquare2");
        redBulletSquareTransform3 = transform.Find("BulletSquare3");
        redBulletSquareTransform4 = transform.Find("BulletSquare4");
        redBulletSquareTransform5 = transform.Find("BulletSquare5");
        redBulletSquareTransform6 = transform.Find("BulletSquare6");
        redBulletSquareTransform7 = transform.Find("BulletSquare7");
        redBulletSquareTransform8 = transform.Find("BulletSquare8");
        redBulletSquareTransform9 = transform.Find("BulletSquare9");
        redBulletSquareTransform10 = transform.Find("BulletSquare10");
        redBulletSquareTransform11 = transform.Find("BulletSquare11");
        redBulletSquareTransform12 = transform.Find("BulletSquare12");
        redBulletSquareTransform13 = transform.Find("BulletSquare13");
        redBulletSquareTransform14 = transform.Find("BulletSquare14");
        redBulletSquareTransform15 = transform.Find("BulletSquare15");
        redBulletSquareTransform16 = transform.Find("BulletSquare16");
        redBulletSquareTransform17 = transform.Find("BulletSquare17");
        redBulletSquareTransform18 = transform.Find("BulletSquare18");
        redBulletSquareTransform19 = transform.Find("BulletSquare19");
        redBulletSquareTransform20 = transform.Find("BulletSquare20");

        if (redGunBulletCount == 0)
        {
            redBulletSquareTransform1.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 1)
        {
            redBulletSquareTransform2.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 2)
        {
            redBulletSquareTransform3.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 3)
        {
            redBulletSquareTransform4.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 4)
        {
            redBulletSquareTransform5.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 5)
        {
            redBulletSquareTransform6.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 6)
        {
            redBulletSquareTransform7.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 7)
        {
            redBulletSquareTransform8.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 8)
        {
            redBulletSquareTransform9.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 9)
        {
            redBulletSquareTransform10.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 10)
        {
            redBulletSquareTransform11.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 11)
        {
            redBulletSquareTransform12.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 12)
        {
            redBulletSquareTransform13.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 13)
        {
            redBulletSquareTransform14.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 14)
        {
            redBulletSquareTransform15.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 15)
        {
            redBulletSquareTransform16.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 16)
        {
            redBulletSquareTransform17.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 17)
        {
            redBulletSquareTransform18.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 18)
        {
            redBulletSquareTransform19.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (redGunBulletCount == 19)
        {
            redBulletSquareTransform20.GetComponent<SpriteRenderer>().color = Color.gray;
        }

        if (redGunBulletCount == 20)
        {
            redBulletSquareTransform1.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform2.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform3.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform4.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform5.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform6.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform7.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform8.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform9.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform10.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform11.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform12.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform13.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform14.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform15.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform16.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform17.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform18.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform19.GetComponent<SpriteRenderer>().color = Color.red;
            redBulletSquareTransform20.GetComponent<SpriteRenderer>().color = Color.red;
        }
        
    }
}

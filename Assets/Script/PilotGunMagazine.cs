using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotGunMagazine : MonoBehaviour
{
    int pilotGunBulletCount;
    Transform bulletSquareTransform1;
    Transform bulletSquareTransform2;
    Transform bulletSquareTransform3;
    Transform bulletSquareTransform4;
    Transform bulletSquareTransform5;
    Transform bulletSquareTransform6;
    Transform bulletSquareTransform7;
    Transform bulletSquareTransform8;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // gunNumber 변수 가져오기
        GameObject player = GameObject.FindWithTag("Player");
        int gunNumber = player.GetComponent<GunController>().gunNumber;

        // 플레이어 가진 총에 따라 창탄 보이거나 안보이게
        if (gunNumber == 1)
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

        pilotGunBulletCount = player.GetComponent<GunController>().pilotGunBulletCount;
        bulletSquareTransform1 = transform.Find("BulletSquare1");
        bulletSquareTransform2 = transform.Find("BulletSquare2");
        bulletSquareTransform3 = transform.Find("BulletSquare3");
        bulletSquareTransform4 = transform.Find("BulletSquare4");
        bulletSquareTransform5 = transform.Find("BulletSquare5");
        bulletSquareTransform6 = transform.Find("BulletSquare6");
        bulletSquareTransform7 = transform.Find("BulletSquare7");
        bulletSquareTransform8 = transform.Find("BulletSquare8");

        if (pilotGunBulletCount == 0) 
        {
            bulletSquareTransform1.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (pilotGunBulletCount == 1)
        {
            bulletSquareTransform2.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (pilotGunBulletCount == 2)
        {
            bulletSquareTransform3.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (pilotGunBulletCount == 3)
        {
            bulletSquareTransform4.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (pilotGunBulletCount == 4)
        {
            bulletSquareTransform5.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (pilotGunBulletCount == 5)
        {
            bulletSquareTransform6.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (pilotGunBulletCount == 6)
        {
            bulletSquareTransform7.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (pilotGunBulletCount == 7)
        {
            bulletSquareTransform8.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (pilotGunBulletCount == 8)
        {
            bulletSquareTransform8.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform7.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform6.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform5.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform4.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform3.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform2.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform1.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotGunMagazine : MonoBehaviour
{
    GameObject player;
    int bulletCount;
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
        player = GameObject.Find("Pilot");        

    }

    // Update is called once per frame
    void Update()
    {
        bulletCount = player.GetComponent<GunController>().bulletCount;
        bulletSquareTransform1 = transform.Find("BulletSquare1");
        bulletSquareTransform2 = transform.Find("BulletSquare2");
        bulletSquareTransform3 = transform.Find("BulletSquare3");
        bulletSquareTransform4 = transform.Find("BulletSquare4");
        bulletSquareTransform5 = transform.Find("BulletSquare5");
        bulletSquareTransform6 = transform.Find("BulletSquare6");
        bulletSquareTransform7 = transform.Find("BulletSquare7");
        bulletSquareTransform8 = transform.Find("BulletSquare8");

        if (bulletCount == 0) 
        {
            bulletSquareTransform8.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform7.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform6.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform5.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform4.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform3.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform2.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform1.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (bulletCount == 1)
        {
            bulletSquareTransform8.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform7.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform6.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform5.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform4.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform3.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform2.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform1.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (bulletCount == 2)
        {
            bulletSquareTransform8.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform7.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform6.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform5.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform4.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform3.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform2.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform1.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (bulletCount == 3)
        {
            bulletSquareTransform8.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform7.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform6.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform5.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform4.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform3.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform2.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform1.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (bulletCount == 4)
        {
            bulletSquareTransform8.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform7.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform6.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform5.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform4.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform3.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform2.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform1.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (bulletCount == 5)
        {
            bulletSquareTransform8.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform7.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform6.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform5.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform4.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform3.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform2.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform1.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (bulletCount == 6)
        {
            bulletSquareTransform8.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform7.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform6.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform5.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform4.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform3.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform2.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform1.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (bulletCount == 7)
        {
            bulletSquareTransform8.GetComponent<SpriteRenderer>().color = Color.gray;
            bulletSquareTransform7.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform6.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform5.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform4.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform3.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform2.GetComponent<SpriteRenderer>().color = Color.green;
            bulletSquareTransform1.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (bulletCount == 8)
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

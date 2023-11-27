using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 종류
public enum ItemType
{
    money,
    key,
    life
}

public class ItemData : MonoBehaviour
{
    public ItemType type;       //아이템 종류
    public int count = 1;       //아이템 수
    public int arrangedId = 0;  //식별용 값

    public static int hasKeys = 1;      //열쇠 수
    public static int money = 100;    //화살 수

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 접촉한 게임오브젝트 == 플레이어
        if (collision.gameObject.tag == "Player")
        {
            if (type == ItemType.key)
            {
                hasKeys += 1;
            }
            else if (type == ItemType.money)
            {
                //ArrowShoot shoot = collision.gameObject.GetComponent<ArrowShoot>();
                money += count;
            }
            else if (type == ItemType.life)
            {
                if (PlayerController.hp < PlayerController.maxHp)
                {
                    PlayerController.hp++;                    
                }
            }

            //// 아이템 휙득 연출
            //gameObject.GetComponent<CircleCollider2D>().enabled = false;
            //Rigidbody2D itemBody = GetComponent<Rigidbody2D>();
            //itemBody.gravityScale = 2.5f;
            //itemBody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
            //Destroy(gameObject, 0.5f);

        }
    }
}

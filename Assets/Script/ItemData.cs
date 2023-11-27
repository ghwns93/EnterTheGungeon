using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ����
public enum ItemType
{
    money,
    key,
    life
}

public class ItemData : MonoBehaviour
{
    public ItemType type;       //������ ����
    public int count = 1;       //������ ��
    public int arrangedId = 0;  //�ĺ��� ��

    public static int hasKeys = 1;      //���� ��
    public static int money = 100;    //ȭ�� ��

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
        // ������ ���ӿ�����Ʈ == �÷��̾�
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

            //// ������ �׵� ����
            //gameObject.GetComponent<CircleCollider2D>().enabled = false;
            //Rigidbody2D itemBody = GetComponent<Rigidbody2D>();
            //itemBody.gravityScale = 2.5f;
            //itemBody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
            //Destroy(gameObject, 0.5f);

        }
    }
}

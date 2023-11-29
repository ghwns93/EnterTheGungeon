using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Animator _door;
    public GameObject BattleArea;

    private bool isDoorClosed = true;
    private bool hasEnemy = false;

    void Start()
    {
        _door = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isDoorClosed)
            {
                _door.Play("UD_Open");
                isDoorClosed = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // BattleArea에 Enemy 태그를 가진 오브젝트가 있는지 확인
            if (BattleArea != null)
            {
                Collider2D[] colliders = Physics2D.OverlapBoxAll(BattleArea.transform.position, BattleArea.transform.localScale, 0f);

                hasEnemy = true;

                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.tag == "Enemy" && collider.gameObject.activeSelf)
                    {

                        _door.Play("UD_Close");
                        break;
                    }
                }
            }
        }

    }

    private void Update()
    {

        if (hasEnemy && isDoorClosed)
        {
            Debug.Log("check");
            // BattleArea에 Enemy 태그를 가진 오브젝트가 더 이상 없고, 문이 닫혀 있는 경우에만 문을 열음
            _door.Play("UD_Open");
        }
    }
}
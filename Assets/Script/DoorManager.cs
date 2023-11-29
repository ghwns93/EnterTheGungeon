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
            // BattleArea�� Enemy �±׸� ���� ������Ʈ�� �ִ��� Ȯ��
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
            // BattleArea�� Enemy �±׸� ���� ������Ʈ�� �� �̻� ����, ���� ���� �ִ� ��쿡�� ���� ����
            _door.Play("UD_Open");
        }
    }
}
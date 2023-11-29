using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private Animator _door = null;
    public GameObject BattleArea;
    public static bool isOpening = false;
    public static bool firstOpen = true;
    
    private void Awake()
    {
        _door = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log("check1");
        if(BattleAreaManager.enemyCheck && !isOpening)
        {
            Debug.Log("check2");
            if(!BattleAreaManager.enemyCheck)
            {
              OpenDoor();
                Debug.Log("check3");
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OpenDoor();
            Debug.Log("�浹�� ������");
        }
    }

    // ���� ���� ���� �Լ�
    private void OpenDoor()
    {
        if (!isOpening) // �̹� �������� ���� ��쿡�� ����
        {
            _door.Play("UD_Open");
            isOpening = true;
        }
    }
}










/*
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
 */
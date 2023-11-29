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
            Debug.Log("충돌로 문열기");
        }
    }

    // 문을 열기 위한 함수
    private void OpenDoor()
    {
        if (!isOpening) // 이미 열려있지 않은 경우에만 열기
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
 */
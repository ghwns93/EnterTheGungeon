using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Rect screenRect = Rect.zero;
    string gameState;        //PlayerController 에서 가져다 저장할곳

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.Find("Pilot").GetComponent<PlayerController>().gameState;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (gameState == "gameover")
        {
            transform.position = player.transform.position;     //gameover되면 player로 카메라 위치 이동
            return;                                             //gameover면 밑에 조건문은 실행 x
        }
        if (player != null)
        {            
            Vector2 pos = Vector2.zero;
            Vector2 playerPos = new Vector2(player.transform.position.x,player.transform.position.y);
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            screenRect = new Rect(0f, 0f, Screen.width, Screen.height);
            
            if (screenRect.Contains(mousePos))
            {
                pos = Camera.main.ScreenToWorldPoint(mousePos);
            }
            transform.position = Vector2.Lerp(playerPos, pos, 0.5f);
        }
    }
}

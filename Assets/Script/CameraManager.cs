using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Rect screenRect = Rect.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
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

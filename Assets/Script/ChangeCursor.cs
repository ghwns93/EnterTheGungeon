using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D customCursor; // Inspector 창에서 할당할 사용자 정의 커서 이미지

    void Start()
    {        
        // 커서 이미지를 지정된 이미지로 변경
        Cursor.SetCursor(customCursor,new Vector2(7f,6f), CursorMode.Auto); // 마우스 커서 hotspot위치 변경
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

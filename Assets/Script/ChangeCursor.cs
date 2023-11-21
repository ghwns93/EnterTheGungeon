using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D customCursor; // Inspector 창에서 할당할 사용자 정의 커서 이미지

    void Start()
    {
        // 커서 이미지를 지정된 이미지로 변경합니다.
        Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

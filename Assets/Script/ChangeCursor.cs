using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D customCursor; // Inspector â���� �Ҵ��� ����� ���� Ŀ�� �̹���

    void Start()
    {
        // Ŀ�� �̹����� ������ �̹����� �����մϴ�.
        Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

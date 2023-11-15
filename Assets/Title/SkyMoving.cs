using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMoving : MonoBehaviour
{
    // 배경이 스크롤되는 속도
    public Vector2 m_speed;

    // 매 프레임마다 호출된 함수
    private void Update()
    {
        // 배경 스프라이트를 표시해주는 기능에 관련된 정보를 전달 받는다
        var spriteRenderer = GetComponent<SpriteRenderer>();

        // 배경 텍스처를 표시해주는 마테리얼을 전달 받는다
        var material = spriteRenderer.material;

        // 배경 텍스처를 스크롤 한다.
        material.mainTextureOffset += m_speed * Time.deltaTime;

    }
}

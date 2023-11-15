using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMoving : MonoBehaviour
{
    // ����� ��ũ�ѵǴ� �ӵ�
    public Vector2 m_speed;

    // �� �����Ӹ��� ȣ��� �Լ�
    private void Update()
    {
        // ��� ��������Ʈ�� ǥ�����ִ� ��ɿ� ���õ� ������ ���� �޴´�
        var spriteRenderer = GetComponent<SpriteRenderer>();

        // ��� �ؽ�ó�� ǥ�����ִ� ���׸����� ���� �޴´�
        var material = spriteRenderer.material;

        // ��� �ؽ�ó�� ��ũ�� �Ѵ�.
        material.mainTextureOffset += m_speed * Time.deltaTime;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunManager : MonoBehaviour
{
    public GameObject bulletPrefab;         //�Ѿ�
    public float shootSpeed = 5.0f;         //�Ѿ� �ӵ�

    public bool isActive = true;

    void Attack()
    {
        if (isActive)
        {
            //�߻� ��ġ�� ����� ���ӿ�����Ʈ ��������
            Transform tr = transform.Find("Gate");
            GameObject gate = tr.gameObject;

            //�÷��̾� ������ ��������
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                //�� �� ����� �Ÿ��� ���ϱ�
                float dx = player.transform.position.x - gate.transform.position.x;
                float dy = player.transform.position.y - gate.transform.position.y;

                //��ũź��Ʈ2 �Լ��� ����(ȣ����) ���ϱ�
                float rad = Mathf.Atan2(dy, dx);

                //������ ����(���ʺй�)�� ��ȯ
                float angle = rad * Mathf.Rad2Deg;

                //�������� �̿��Ͽ� �Ѿ� ������Ʈ ����� (���� �������� ȸ��)
                Quaternion r = Quaternion.Euler(0, 0, angle);
                GameObject bullet = Instantiate(bulletPrefab, gate.transform.position, r);
                float x = Mathf.Cos(rad);
                float y = Mathf.Sin(rad);
                Vector3 v = new Vector3(x, y) * shootSpeed;

                //�Ѿ� �߻�
                Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
                rbody.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }
}

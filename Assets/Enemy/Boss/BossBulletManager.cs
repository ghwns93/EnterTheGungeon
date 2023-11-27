using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletManager : MonoBehaviour
{
    public string patten = "SpinAttack";

    public GameObject bulletPrefab;
    public float shootSpeed = 5.0f;         //�Ѿ� �ӵ�

    public float shootWatingTime = 3.0f;    //�Ѿ� �߻� ����
    public float minAngle = 45.0f;          //�ּ� ���� (���� 0 , �Ʒ��� 90 , ������ 180, ���� 270)
    public float maxAngle = 135.0f;         //�ִ� ���� (���� 0 , �Ʒ��� 90 , ������ 180, ���� 270)
    public float angleCount = 2;            //�� ���� ���̿� ��� �Ѿ��� ������ ���� ����
    public int MaxBullet = 10;              //1��ƾ�� �Ѿ� ����
    public bool bulletTwoWay = true;        //�Ѿ� �Դ� ���� ����.

    List<GameObject> bulletStats;           //�Ѿ��� ǥ���� �ѹ��� ��� ���� List�� ����
    int count = 0;                          //�Ѿ� ���� ī��Ʈ
    int routineCount = 0;                   //���° �߻������� ī��Ʈ

    float plusAngle = 0.0f;

    float rDelay = 0;

    private void Awake()
    {
        rDelay = shootWatingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (rDelay >= shootWatingTime)
        {
            BulletShoot();
            rDelay = 0;
        }
        else
        {
            rDelay += Time.deltaTime;
        }
    }

    private void BulletShoot()
    {
        float objX = 0, objY = 0;

        float rad = 0.0f;
        float halfCount = MaxBullet / 2;
        float limit = 0.0f;

        count = 0;

        while (count <= MaxBullet + 1)
        {
            if (bulletTwoWay)
            {
                if (halfCount < count)
                {
                    limit = (float)(maxAngle - (((maxAngle - minAngle) / halfCount) * ((count - halfCount) - 0.5f)));

                    limit = limit <= minAngle ? minAngle : limit;

                    rad = limit * Mathf.Deg2Rad;
                    objX = transform.position.x + ((float)Math.Cos(rad));
                    objY = transform.position.y - ((float)Math.Sin(rad));
                }
                else
                {
                    limit = (float)(minAngle + (((maxAngle - minAngle) / halfCount) * count));

                    limit = limit >= maxAngle ? maxAngle : limit;

                    rad = limit * Mathf.Deg2Rad;
                    objX = transform.position.x + ((float)Math.Cos(rad));
                    objY = transform.position.y - ((float)Math.Sin(rad));
                }
            }
            else
            {
                limit = (float)(minAngle + (((maxAngle - minAngle) / MaxBullet) * count));

                limit = limit >= maxAngle ? maxAngle : limit;

                Debug.Log("plusAngle : " + plusAngle);
                rad = (limit + plusAngle) * Mathf.Deg2Rad;
                objX = transform.position.x + ((float)Math.Cos(rad));
                objY = transform.position.y - ((float)Math.Sin(rad));
            }

            //������ ����(���ʺй�)�� ��ȯ
            float angle = rad * Mathf.Rad2Deg;

            Vector3 v = new Vector3(objX, objY);

            Quaternion r = Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(bulletPrefab, v, r);

            float dx = transform.position.x - bullet.transform.position.x;
            float dy = transform.position.y - bullet.transform.position.y;

            Vector2 v2 = new Vector3(dx * -1, dy * -1) * shootSpeed;

            Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
            rbody.AddForce(v2, ForceMode2D.Impulse);

            count++;
        }

        if(routineCount < angleCount)
        {
            plusAngle = (((maxAngle - minAngle) / MaxBullet) / angleCount) * (++routineCount);
        }
        else
        {
            plusAngle = 0;
            routineCount = 0;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletManager : MonoBehaviour
{
    public string patten = "SpinAttack";

    public GameObject bulletPrefab;
    public float shootSpeed = 5.0f;         //총알 속도

    public float shootWatingTime = 3.0f;    //총알 발사 간격
    public float minAngle = 45.0f;          //최소 각도 (왼쪽 0 , 아랫쪽 90 , 오른쪽 180, 윗쪽 270)
    public float maxAngle = 135.0f;         //최대 각도 (왼쪽 0 , 아랫쪽 90 , 오른쪽 180, 윗쪽 270)
    public float angleCount = 2;            //각 각도 사이에 몇개의 총알을 넣을것 인지 설정
    public int MaxBullet = 10;              //1루틴당 총알 갯수
    public bool bulletTwoWay = true;        //총알 왔다 갔다 설정.

    List<GameObject> bulletStats;           //총알을 표현후 한번에 쏘기 위해 List에 저장
    int count = 0;                          //총알 갯수 카운트
    int routineCount = 0;                   //몇번째 발사중인지 카운트

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

            //라디안을 각도(육십분법)로 변환
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

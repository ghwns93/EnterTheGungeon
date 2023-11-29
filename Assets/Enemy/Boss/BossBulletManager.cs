using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletManager : MonoBehaviour
{
    internal enum PattenNumber
    {
        SPIN = 0,
        SHOTGUN = 1,
        FIRE = 2
    }

    //�Ѿ� ���� �ɼ�
    public float shootSpeed = 5.0f;         //�Ѿ� �ӵ�

    public bool isAttack = false;                   //���� ������ �������� Ȯ��

    //ȸ�� ���� �ɼ�
    public float shootWatingTime = 3.0f;    //�Ѿ� �߻� ����
    public GameObject bulletPrefab;
    public float minAngle = 45.0f;          //�ּ� ���� (���� 0 , �Ʒ��� 90 , ������ 180, ���� 270)
    public float maxAngle = 135.0f;         //�ִ� ���� (���� 0 , �Ʒ��� 90 , ������ 180, ���� 270)
    public float angleCount = 2;            //�� ���� ���̿� ��� �Ѿ��� ������ ���� ����
    public int MaxBullet = 10;              //1��ƾ�� �Ѿ� ����
    public float spinAttackTime = 4.0f;     //�� ȸ�� �ð�
    public bool bulletTwoWay = true;        //�Ѿ� �Դ� ���� ����.

    //���� �߻�
    public GameObject shotgunBulletPrefab;  //���� �Ѿ� ������
    public int shotgunCount = 3;            //���� �߻� ����
    public int shotgunFireCount = 2;        //���� ���� Ƚ��
    private int realFireCount = 0;
    public float shotAngle = 90.0f;         //���� ź ���� ����
    public float shotAttackTime = 0.5f;     //���� �߻� �ӵ�

    //���� �߻�
    public GameObject fireBulletPrefab;     //���� �Ѿ� ������

    //���� private
    List<GameObject> bulletStats;           //�Ѿ��� ǥ���� �ѹ��� ��� ���� List�� ����
    int count = 0;                          //�Ѿ� ���� ī��Ʈ
    int routineCount = 0;                   //���° �߻������� ī��Ʈ

    float plusAngle = 0.0f;
    float attackTime = 0.0f;

    float rDelay = 0;
    internal PattenNumber patten;

    private void Awake()
    {
        rDelay = shootWatingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack)
        {
            if (patten == PattenNumber.SPIN)
            {
                #region [ ���� ���� ]
                if (attackTime == 0.0f)
                {
                    attackTime = spinAttackTime;
                }

                if (rDelay >= shootWatingTime)
                {
                    if (patten == PattenNumber.SPIN)
                    {
                        if (attackTime > 0)
                        {
                            SpinAttack();
                        }
                        else
                        {
                            isAttack = false;
                            SpinAttackFinish();
                            attackTime = spinAttackTime;
                        }
                    }

                    rDelay = 0;
                }
                else
                {
                    rDelay += Time.deltaTime;
                    attackTime -= Time.deltaTime;
                }
                #endregion
            }
            else if (patten == PattenNumber.SHOTGUN)
            {
                #region [ ���� ���� ]

                if (realFireCount == shotgunFireCount) realFireCount = 0;

                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (rDelay >= shotAttackTime)
                {
                    if (player != null)
                    {
                        //�� �� ����� �Ÿ��� ���ϱ�
                        float dx = player.transform.position.x - transform.position.x;
                        float dy = player.transform.position.y - transform.position.y;

                        //��ũź��Ʈ2 �Լ��� ����(ȣ����) ���ϱ�
                        float rad = Mathf.Atan2(dy, dx);

                        //������ ����(���ʺй�)�� ��ȯ
                        float angle = rad * Mathf.Rad2Deg;

                        ShotgunAttack(angle);

                        if (++realFireCount == shotgunFireCount) isAttack = false;
                    }

                    rDelay = 0;
                }
                else
                {
                    rDelay += Time.deltaTime;
                }
                #endregion
            }
            else if (patten == PattenNumber.FIRE)
            {
                #region [ ���� ���� ]
                Transform tr = transform.Find("FireBulletPos");
                GameObject gate = tr.gameObject;

                //�������� �̿��Ͽ� �Ѿ� ������Ʈ ����� (���� �������� ȸ��)
                Quaternion r = Quaternion.Euler(0, 0, 0);
                GameObject bullet = Instantiate(fireBulletPrefab, gate.transform.position, r);

                isAttack = false;
                #endregion
            }
        }
    }

    private void SpinAttack()
    {
        #region [ ȸ�� ���� ]

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

                rad = (limit + plusAngle + (attackTime * 20)) * Mathf.Deg2Rad;
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

        if (routineCount < angleCount - 1)
        {
            plusAngle = (((maxAngle - minAngle) / MaxBullet) / angleCount) * (++routineCount);
        }
        else
        {
            plusAngle = 0;
            routineCount = 0;
        }

        #endregion
    }

    private void SpinAttackFinish()
    {
        #region [ ȸ�� ���� ������ ]

        float objX = 0, objY = 0;

        float FinishMaxBullet = (MaxBullet * angleCount) * 2;

        float rad = 0.0f;
        float halfCount = FinishMaxBullet / 2;
        float limit = 0.0f;

        count = 0;

        while (count <= FinishMaxBullet + 1)
        {
            limit = (float)(minAngle + (((maxAngle - minAngle) / FinishMaxBullet) * count));

            limit = limit >= maxAngle ? maxAngle : limit;

            rad = limit * Mathf.Deg2Rad;
            objX = transform.position.x + ((float)Math.Cos(rad));
            objY = transform.position.y - ((float)Math.Sin(rad));

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

        #endregion
    }

    private void ShotgunAttack(float angle)
    {
        #region [ ���� ���� ]

        float minAngle = angle + (shotAngle / 2);

        //�������� �̿��Ͽ� �Ѿ� ������Ʈ ����� (���� �������� ȸ��)
        for (int i = 0; i < shotgunCount; i++) 
        {
            float plusAngle = (shotAngle / (shotgunCount - 1)) * i;
            Quaternion r = Quaternion.Euler(0, 0, minAngle - plusAngle);
            GameObject bullet = Instantiate(shotgunBulletPrefab, transform.position, r);

            float localRad = (minAngle - plusAngle) * Mathf.Deg2Rad;

            float x = Mathf.Cos(localRad);
            float y = Mathf.Sin(localRad);
            Vector3 v = new Vector3(x, y) * shootSpeed;

            //�Ѿ� �߻�
            Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
            rbody.AddForce(v, ForceMode2D.Impulse);
        }

        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBulletMiddleScript : MonoBehaviour
{
    public GameObject fireBullet;

    public int MaxBullet = 6;
    public float moveSpeed = 2.0f;
    public float shootSpeed = 5.0f;
    public float liveTime = 2.0f;
    public float firePower = 1.0f;

    Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v = rbody.velocity;

        rbody.velocity = Vector2.zero;

        if (v.x > 0)
        {
            v.x -= Time.deltaTime * moveSpeed;
            if (v.x <= 0) v.x = 0;
        }
        else
        {
            v.x += Time.deltaTime * moveSpeed;
            if (v.x >= 0) v.x = 0;
        }

        if (v.y > 0)
        {
            v.y -= Time.deltaTime * moveSpeed;
            if (v.y <= 0) v.y = 0;
        }
        else
        {
            v.y += Time.deltaTime * moveSpeed;
            if (v.y >= 0) v.y = 0;
        }

        rbody.AddForce(v, ForceMode2D.Impulse);

        liveTime -= Time.deltaTime;

        if (liveTime <= 0.0f)
        {
            float limitAngle = (360.0f / MaxBullet) / 2;
            float rAngle = UnityEngine.Random.Range(-limitAngle, limitAngle);
            SpinAttackFinish(rAngle);
            Destroy(gameObject);
        }
    }

    private void SpinAttackFinish(float addAngle)
    {
        #region [ 폭죽 공격 ]

        float objX = 0, objY = 0;

        float rad = 0.0f;
        float limit = 0.0f;
        
        int count = 0;

        while (count <= MaxBullet + 1)
        {
            limit = (float)(((360 / MaxBullet) * count));

            rad = (limit + addAngle) * Mathf.Deg2Rad;
            objX = transform.position.x + ((float)Mathf.Cos(rad));
            objY = transform.position.y - ((float)Mathf.Sin(rad));

            //라디안을 각도(육십분법)로 변환
            float angle = rad * Mathf.Rad2Deg;

            Vector3 v = new Vector3(objX, objY);

            Quaternion r = Quaternion.Euler(0, 0, angle);
            GameObject bullet = Instantiate(fireBullet, v, r);

            float dx = transform.position.x - bullet.transform.position.x;
            float dy = transform.position.y - bullet.transform.position.y;

            Vector2 v2 = new Vector3(dx * -1, dy * -1) * shootSpeed;

            Rigidbody2D rbody = bullet.GetComponent<Rigidbody2D>();
            rbody.AddForce(v2, ForceMode2D.Impulse);

            count++;
        }

        #endregion
    }
}

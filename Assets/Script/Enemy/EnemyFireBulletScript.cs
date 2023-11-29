using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBulletScript : MonoBehaviour
{
    public GameObject fireBullet;

    public int MaxBullet = 6;
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
        rbody.velocity = Vector3.zero;

        Vector2 v = Vector2.up * firePower * liveTime;

        rbody.AddForce(v, ForceMode2D.Force);

        liveTime -= Time.deltaTime;

        if (liveTime <= 0.0f)
        {
            SpinAttackFinish();
            Destroy(gameObject);
        }
    }

    private void SpinAttackFinish()
    {
        #region [ 회전 공격 마무리 ]

        float objX = 0, objY = 0;

        float rad = 0.0f;
        float limit = 0.0f;
        
        int count = 0;

        while (count <= MaxBullet + 1)
        {
            limit = (float)(((360 / MaxBullet) * count));

            rad = limit * Mathf.Deg2Rad;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDelete : MonoBehaviour
{
    public float deleteTime = 3.0f; //소멸에 걸리는 시간

    public GameObject bulletBombPrefab; //총알 터지는 애니메이션가진 프리팹

    GameObject bulletBombObj;       //프리팹으로 만들 객체

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deleteTime);    //총알은 3초뒤 자동소멸
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag.ToLower() == "enemy")
        {
            // 닿은 곳에서 총알 터지는 애니메이션 가진 객체생성
            bulletBombObj = Instantiate(bulletBombPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(bulletBombObj, 1.0f);
            //이거 에네미에 달아야되려나

            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDelete : MonoBehaviour
{
    public float deleteTime = 3.0f; //소멸에 걸리는 시간

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deleteTime);    //총알은 3초뒤 자동소멸        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

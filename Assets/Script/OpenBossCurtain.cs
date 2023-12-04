using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBossCurtain : MonoBehaviour
{
    GameObject bossObj;

    // Start is called before the first frame update
    void Start()
    {
        bossObj = transform.Find("RoomEnterManager").transform.Find("BossObject").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossObj == null)
        {
            Debug.Log("11111");
            GameObject curtain = GameObject.FindGameObjectWithTag("ExitCurtain");
            curtain.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

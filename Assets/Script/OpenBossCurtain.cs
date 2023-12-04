using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBossCurtain : MonoBehaviour
{
    GameObject bossObj;
    GameObject mobObj;

    // Start is called before the first frame update
    void Start()
    {
        bossObj = transform.Find("RoomEnterManager").transform.Find("BossObject").gameObject;
        mobObj = transform.Find("RoomEnterManager").transform.Find("Agonizer").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossObj == null && mobObj == null)
        {
            Debug.Log("11111");
            GameObject curtain = GameObject.FindGameObjectWithTag("ExitCurtain");
            curtain.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

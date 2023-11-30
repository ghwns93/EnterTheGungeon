using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnterManager : MonoBehaviour
{
    List<MonsterAwakeManager> mList;

    // Start is called before the first frame update
    void Start()
    {
        mList = new List<MonsterAwakeManager>();

        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<MonsterAwakeManager>() != null)
                mList.Add(transform.GetChild(i).GetComponent<MonsterAwakeManager>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision : " + collision.gameObject.tag);

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Enter!");
            foreach(var monster in mList) 
            {
                monster.isAwake = true;
            }
        }
    }
}

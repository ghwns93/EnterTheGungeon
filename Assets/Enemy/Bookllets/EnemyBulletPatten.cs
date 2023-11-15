using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyBulletPatten : MonoBehaviour
{
    EnemyScript enemyScript;

    bool isAttack = false;
    bool isAct = false;


    // Start is called before the first frame update
    void Start()
    {
        enemyScript = gameObject.GetComponent<EnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(enemyScript != null) 
        {
            
        }
    }

    
}

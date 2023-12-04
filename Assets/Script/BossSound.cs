using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SoundManager sound = GameObject.Find("SoundPrefab").GetComponent<SoundManager>();
        sound.bossRoom= true;
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}

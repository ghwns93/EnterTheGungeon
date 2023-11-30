using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGateController : MonoBehaviour
{
    GameObject player;
    Animator animator;
    BoxCollider2D box;

    public string openAnime = "BossGateAnim";

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null) 
        {
            float dis = Vector2.Distance(transform.position, player.transform.position);

            if(Mathf.Abs(dis) < 5)
            {
                animator.Play(openAnime);
            }
        }
    }

    private void collderChangeTrigger()
    {
        box.isTrigger = true;
    }
}

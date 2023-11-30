using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    public string TableIdle = "TableIdle";
    public string TableRight = "TableRight";
    public string TableLeft = "TableLeft";
    public string TableUp = "TableUp";
    public string TableDown = "TableDown";
    public bool guardOn = false;

    public Animator animator;

    Rigidbody2D rbody;

    PlayerController playerCnt;

    // Start is called before the first frame update
    void Start()
    {
        playerCnt = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            animator.Play("TableIdle");
            animator.Play("Table_ShadowIdle");
        }
    }

//    private void OnCollisionEnter2D(Collider2D collision)
//    {
        
//    }
}

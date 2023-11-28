using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public Animator ulDoorAnimator;
    public Animator urDoorAnimator;

    private bool doorsOpen = false;

    void Start()
    {
        ulDoorAnimator = transform.Find("ULDoor").GetComponent<Animator>();
        urDoorAnimator = transform.Find("URDoor").GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player" && doorsOpen == false)
        {
            OpenDoors();
            CloseDoors();
        }
        
    }

    void OpenDoors()
    {
        ulDoorAnimator.Play("ULD_Open");
        urDoorAnimator.Play("URD_Open");

        GetComponent<BoxCollider2D>().enabled = true;

       // doorsOpen = true;
    }

    void CloseDoors()
    {

    }
}

/**/
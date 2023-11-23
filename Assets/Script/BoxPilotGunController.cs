using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPilotGunController : MonoBehaviour
{
    // �� �ִϸ�����
    private Animator gunAnimator;

    // �ִϸ��̼�
    public string pilotGunHold = "PilotGunHold";
    public string pilotGunReady = "PilotGunReady";
    public string pilotGunFire = "PilotGunFire";
    public string pilotGunReturn = "PilotGunReturn";
    public string pilotGunReload = "PilotGunReload";

    GameObject player;
    //Transform pilotGun;

    // �÷��̾� �� �ִϸ�����
    private Animator PlayerGunAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // �⺻ �ִϸ��̼� ����
        gunAnimator.Play(pilotGunHold);

        player = GameObject.FindGameObjectWithTag("Player");
        PlayerGunAnimator = player.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(PlayerGunAnimator)
    }
}

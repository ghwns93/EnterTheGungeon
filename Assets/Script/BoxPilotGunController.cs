using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPilotGunController : MonoBehaviour
{
    // 총 애니메이터
    private Animator gunAnimator;

    // 애니메이션
    public string pilotGunHold = "PilotGunHold";
    public string pilotGunReady = "PilotGunReady";
    public string pilotGunFire = "PilotGunFire";
    public string pilotGunReturn = "PilotGunReturn";
    public string pilotGunReload = "PilotGunReload";

    GameObject player;
    //Transform pilotGun;

    // 플레이어 총 애니메이터
    private Animator PlayerGunAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // 기본 애니메이션 설정
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

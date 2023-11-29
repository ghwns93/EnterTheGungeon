using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossChairManager : MonoBehaviour
{
    #region [ 애니메이션 이름 ]
    public string chairIdle = "BossChiarIdle";
    public string chairAttacking = "BossChairAttacking";
    public string chairLeftAttack = "BossChiarLeftAttack";
    public string chairRightAttack = "BossChiarRightAttack";
    public string chairShotgunAttack = "BossChiarShotgunAttack";
    public string chairDead = "BossChiarDead";

    public string bodyIdle = "BossBodyIdle";
    public string bodyAttack = "BossBodyAttack";
    public string bodyDead = "BossBodyDead";
    #endregion

    public float attackDelay = 2.0f;
    private float realAttackTime = 0.0f;

    private string oldBodyAnimation = "";
    private string newBodyAnimation = "";

    private string oldChairAnimation = "";
    private string newChairAnimation = "";

    internal bool isAttack = false;

    private bool OnceOk = true;
    private Animator bodyAnimator;
    private SpriteRenderer bodyRenderer;
    BossBulletManager bulletManager;

    // 애니메이터
    private Animator chairAnimator;

    private void Start()
    {
        bodyAnimator = transform.Find("BossBody").GetComponent<Animator>();
        bodyRenderer = transform.Find("BossBody").GetComponent<SpriteRenderer>();

        chairAnimator = GetComponent<Animator>();
        bulletManager = GetComponent<BossBulletManager>();

        newBodyAnimation = bodyIdle;
        newChairAnimation = chairIdle;
    }

    private void Update()
    {
        if (isAttack && OnceOk && realAttackTime <= 0.0f)
        {
            OnceOk = false;
            realAttackTime = attackDelay;

            int bolletShape = UnityEngine.Random.Range(0, 2);

            bulletManager.patten = (BossBulletManager.PattenNumber)bolletShape;

            if (newChairAnimation == chairIdle)
            {
                if (bulletManager.patten == BossBulletManager.PattenNumber.SPIN)
                {
                    newChairAnimation = chairLeftAttack;
                }
                else if (bulletManager.patten == BossBulletManager.PattenNumber.SHOTGUN)
                {
                    newChairAnimation = chairShotgunAttack;
                }
            }
        }
        else if(isAttack && OnceOk)
        {
            newBodyAnimation = bodyIdle;
            newChairAnimation = chairIdle;

            realAttackTime -= Time.deltaTime;
        }

        if(oldBodyAnimation != newBodyAnimation)
        {
            oldBodyAnimation = newBodyAnimation;
            bodyAnimator.Play(newBodyAnimation);
        }

        if(oldChairAnimation != newChairAnimation) 
        {
            oldChairAnimation = newChairAnimation;
            chairAnimator.Play(newChairAnimation);
        }
    }

    private void SpinAttacking()
    {
        if(bulletManager.isAttack == false) bulletManager.isAttack = true;

        if (bulletManager.patten == BossBulletManager.PattenNumber.SPIN)
        {
            newChairAnimation = chairAttacking;
            bodyRenderer.color = new Color(1, 1, 1, 0); // 회전중 바디가 안보이게 변경
        }
        else
        {
            newBodyAnimation = bodyAttack;
        }
    }

    private void CheckAttackFinish()
    {
        if (bulletManager.isAttack == false)
        {
            ReturnIdle();
        }
    }

    private void ReturnIdle()
    {
        newChairAnimation = chairIdle;
        newBodyAnimation = bodyIdle;
        bodyRenderer.color = new Color(1, 1, 1, 1);
        OnceOk = true;
    }
}

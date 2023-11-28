using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossChairManager : MonoBehaviour
{
    #region [ �ִϸ��̼� �̸� ]
    public string chairIdle = "BossChiarIdle";
    public string chairAttacking = "BossChairAttacking";
    public string chairLeftAttack = "BossChiarLeftAttack";
    public string chairRightAttack = "BossChiarRightAttack";
    public string chairDead = "BossChiarDead";

    public string bodyIdle = "BossBodyIdle";
    public string bodyAttack = "BossBodyAttack";
    public string bodyDead = "BossBodyDead";
    #endregion

    public float attackDelay = 2.0f;
    private float realAttackTime = 0.0f;

    internal bool isAttack = false;

    private bool OnceOk = true;
    private Animator bodyAnimator;
    private SpriteRenderer bodyRenderer;
    BossBulletManager bulletManager;

    // �ִϸ�����
    private Animator chairAnimator;

    private void Start()
    {
        bodyAnimator = transform.Find("BossBody").GetComponent<Animator>();
        bodyRenderer = transform.Find("BossBody").GetComponent<SpriteRenderer>();

        chairAnimator = GetComponent<Animator>();
        bulletManager = GetComponent<BossBulletManager>();
    }

    private void Update()
    {
        if (isAttack && OnceOk && realAttackTime <= 0.0f)
        {
            realAttackTime = attackDelay;
            OnceOk = false;
            bulletManager.patten = 0;

            if(bulletManager.patten == BossBulletManager.PattenNumber.SPIN) 
            {
                chairAnimator.Play(chairLeftAttack);
            }
        }
        else if(isAttack && OnceOk)
        {
            realAttackTime -= Time.deltaTime;
        }
    }

    private void SpinAttacking()
    {
        bulletManager.isAttack = true;
        chairAnimator.Play(chairAttacking);
        bodyRenderer.color = new Color(1, 1, 1, 0); // ȸ���� �ٵ� �Ⱥ��̰� ����
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
        chairAnimator.Play(chairIdle);
        bodyRenderer.color = new Color(1, 1, 1, 1);
        bodyAnimator.Play(bodyIdle);
        OnceOk = true;
    }
}

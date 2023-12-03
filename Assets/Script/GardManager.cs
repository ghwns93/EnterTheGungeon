using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardManager : MonoBehaviour
{
    private Animator animator;

    public string openGardName; // 열릴 때의 애니메이션 이름
    public string closeGardName; // 닫힐 때의 애니메이션 이름

    void Start()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
        Debug.Log("최초 가드 숨겨짐");
    }

    public void CloseGard()
    {
            gameObject.SetActive(true);
        Debug.Log("최초 가드 숨겨짐");
            animator.Play(closeGardName);
    }

    public void OpenGard()
    {
            animator.Play(openGardName);
            gameObject.SetActive(false);
    }
}

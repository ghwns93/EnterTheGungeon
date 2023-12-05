using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardManager : MonoBehaviour
{
    private Animator animator;

    public string openGardName; // 열릴 때의 애니메이션 이름
    public string closeGardName; // 닫힐 때의 애니메이션 이름

    AudioSource audioSource;

    public AudioClip GuardOpenSound;
    public AudioClip GuardCloseSound;

    void Start()
    {
        // 오디오 소스 가져오기
        if (GameObject.Find("SeSoundPrefab") != null)
            audioSource = GameObject.Find("SeSoundPrefab").GetComponent<AudioSource>();
        else
            audioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
        Debug.Log("최초 가드 숨겨짐");
    }

    public void CloseGard()
    {
            gameObject.SetActive(true);
        Debug.Log("최초 가드 숨겨짐");
            animator.Play(closeGardName);
        audioSource.PlayOneShot(GuardCloseSound);
    }

    public void OpenGard()
    {
            animator.Play(openGardName);
            gameObject.SetActive(false);
        audioSource.PlayOneShot(GuardOpenSound);
    }
}

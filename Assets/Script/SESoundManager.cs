using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SESoundManager : MonoBehaviour
{
    public static SESoundManager Instance { get; private set; }

    void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 게임 오브젝트가 씬 전환시에도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }

        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SEvolumeValue");
    }
}

using Unity.Mathematics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public enum BGMType { None, Title, Lobby, InGame, Boss }

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource bgmSource; // 배경음악을 위한 AudioSource
    public AudioSource seSource;  // 효과음을 위한 AudioSource

    public AudioClip bgmTitle;          // BGM(타이틀)
    public AudioClip bgmLobby;       // BGM(로비)
    public AudioClip bgmInGame;    // BGM(게임 중)
    public AudioClip bgmBoss;          // BGM(보스전)

    public float bgmVolume = 1.0f; // 기본 BGM 볼륨 값
    public float seVolume = 1.0f;  // 기본 SE 볼륨 값

    // 현재 실행되고 있는 BGM
    public static BGMType playingBGM = BGMType.None;

    public bool bossRoom = false;

    //private Dictionary<string, AudioClip> seClips = new Dictionary<string, AudioClip>();

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

        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("BGMvolumeValue");
    }

    void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // Scene 이름에 따라 BGM 재생
        if (sceneName == "Title")
        {
            PlayBGM(BGMType.Title);
        }
        else if (sceneName == "Lobby")
        {
            PlayBGM(BGMType.Lobby);
        }
        else if (sceneName == "MainStage")
        {
            if(bossRoom == false)
            {
                PlayBGM(BGMType.InGame);
            }
            else
            {
                PlayBGM(BGMType.Boss);
            }
        }
        else if (sceneName == "EndingCredit")
        {
            PlayBGM(BGMType.Title);
        }
    }

    public void PlayBGM(BGMType type)
    {
        if (type != playingBGM)
        {
            playingBGM = type;
            AudioSource audio = GetComponent<AudioSource>();

            if (type == BGMType.Title)
                audio.clip = bgmTitle; // 타이틀 BGM
            else if (type == BGMType.InGame)
                audio.clip = bgmInGame; // 게임 BGM
            else if (type == BGMType.Lobby)
                audio.clip = bgmLobby; // 게임 BGM
            else if (type == BGMType.Boss)
                audio.clip = bgmBoss; // 보스 BGM

            audio.Play(); // 사운드 재생
        }
    }
    
    public void PlaySE(string seName)
    {
        /*
        if (seClips.ContainsKey(seName))
        {
            seSource.PlayOneShot(seClips[seName]);
        }
        else
        {
            Debug.LogWarning("SE clip not found: " + seName);
        }
         */
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        bgmSource.volume = volume;
    }

    public void SetSEVolume(float volume)
    {
        seVolume = volume;
        seSource.volume = volume;
    }
}
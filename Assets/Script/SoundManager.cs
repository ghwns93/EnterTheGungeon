using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BGMType { None, Title, Lobby, InGame, Boss }
public enum SEType { UI, InGameSE, Shoot }

public class SoundManager : MonoBehaviour
{
    public AudioClip bgmTitle;          // BGM(타이틀)
    public AudioClip bgmLobby;       // BGM(로비)
    public AudioClip bgmInGame;    // BGM(게임 중)
    public AudioClip bgmBoss;          // BGM(보스전)

    public AudioClip seInGame;  // SE 인게임
    public AudioClip seShoot;     // SE 쏘는 소리
    public AudioClip seUI;            // SE UI

    // 사운드매니저를 저장할 변수
    public static SoundManager soundManager;

    // 현재 재생중인 BGM
    public static BGMType playingBGM = BGMType.None;

    private void Awake()
    {
        // BGM 재생
        if (soundManager == null)
        {
            // static 변수에 자기자신을 저장
            soundManager = this;

            // Scene 이 이동해도 오브젝트를 파기하지 않음
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 정보가 삽입되어 있다면 즉시 파기
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
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

    }
    // BGM 재생
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

    public void StopBGM()
    {
        GetComponent<AudioSource>().Stop();
        playingBGM = BGMType.None;
    }

    public void SEPlay(SEType type)
    {
        if (type == SEType.UI)
            GetComponent<AudioSource>().PlayOneShot(seUI);
        else if (type == SEType.InGameSE)
            GetComponent<AudioSource>().PlayOneShot(seInGame);
        else if (type == SEType.Shoot)
            GetComponent<AudioSource>().PlayOneShot(seShoot);
    }
}


using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider BgmSlider;
    
     void Start()
    {
        if (BgmSlider != null) BgmSlider.value = 1.0f;
        else Debug.Log("null1");
    }

    void Update()
    {
        if (BgmSlider != null) BgmSliderValue();
        else Debug.Log("null2");
    }

    public void BgmSliderValue()
    {
        float value = BgmSlider.value;
        float soundValue = Mathf.Round(value / 5.0f) * 5.0f;
        BgmSlider.value = soundValue;
    }
}


/*
public enum BGMType { None, Title, InGame, InBoss }
public enum SEType { GameClear, GameOver, Shoot }

public class SoundManager : MonoBehaviour
{
    public AudioClip bgmInTitle;         // BGM(Ÿ��Ʋ)   
    public AudioClip bgmInGame;      // BGM(���� ��)
    public AudioClip bgmInBoss;        // BGM(������)
    public AudioClip seShoot;             // SE(�ѽ��)
    public AudioClip seUI;             // SE(�ѽ��)

    // ù SoundManager�� ������ Static ����
    public static SoundManager soundManager;

    // ���� ������� BGM
    public static BGMType playingBGM = BGMType.None;

    // Start is called before the first frame update
    void Start()
    {
        // BGM ���
        if (soundManager == null)
        {
            // static ������ �ڱ��ڽ��� ����
            soundManager = this;

            // Scene�� �̵��ص� ������Ʈ�� �ı����� ����
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // ������ ���ԵǾ� �ִٸ� ��� �ı�
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayBGM(BGMType type)
    {
        if (type != playingBGM)
        {

            playingBGM = type;
            AudioSource audio = GetComponent<AudioSource>();
            if (type == BGMType.Title)
            {
                audio.clip = bgmInTitle;    // Ÿ��Ʋ bgm
            }
            else if (type == BGMType.InGame)
            {
                audio.clip = bgmInGame; // �ΰ��� bgm
            }
            else if (type == BGMType.InBoss)
            {
                audio.clip = bgmInBoss; // ���� bgm
            }

            audio.Play();   // ���� ���
        }
    }

    public void StopBGM()
    {
        GetComponent<AudioSource>().Stop();
        playingBGM = BGMType.None;
    }

    public void SEPlay(SEType type)
    {
        /*
        if (type == SEType.GameClear)
            GetComponent<AudioSource>().PlayOneShot(meGameClear);
        else if (type == SEType.GameOver)
            GetComponent<AudioSource>().PlayOneShot(meGameOver);
         
        if (type == SEType.Shoot)
            GetComponent<AudioSource>().PlayOneShot(seShoot);
    }

}
 */

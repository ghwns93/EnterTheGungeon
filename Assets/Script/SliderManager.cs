using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;

    private float originalBGMVolume; // 원래 BGM 볼륨 값
    private float originalSEVolume; // 원래 SE 볼륨 값

    private void Start()
    {
        // 슬라이더의 값을 로드합니다.
        LoadValues();
    }

    // 볼륨 값을 저장하고 로드하는 메서드
    public void SaveVolumeButton()
    {
        // 슬라이더의 값을 PlayerPrefs에 저장합니다.
        PlayerPrefs.SetFloat("BGMvolumeValue", bgmSlider.value);
        PlayerPrefs.SetFloat("SEvolumeValue", seSlider.value);

        // 값을 다시 로드하여 적용합니다.
        LoadValues();

        // 옵션 창을 비활성화합니다.
        gameObject.SetActive(false);
    }

    // 저장된 값을 불러오는 메서드
    void LoadValues()
    {
        // PlayerPrefs에서 볼륨 값을 가져옵니다. 기본값은 1입니다.
        originalBGMVolume = PlayerPrefs.GetFloat("BGMvolumeValue", 1f);
        originalSEVolume = PlayerPrefs.GetFloat("SEvolumeValue", 1f);

        // 슬라이더와 오디오 리스너에 값을 적용합니다.
        bgmSlider.value = originalBGMVolume;
        seSlider.value = originalSEVolume;
        // 이 부분에서 실제 게임의 오디오 볼륨을 조절하는 로직을 추가해야 합니다.
        // 예: AudioManager.SetVolume(originalBGMVolume, originalSEVolume);
    }

    // 취소 버튼을 눌렀을 때의 메서드
    public void CancelButton()
    {
        // 슬라이더 값을 원래대로 복구합니다.
        bgmSlider.value = originalBGMVolume;
        seSlider.value = originalSEVolume;

        // 실제 게임의 오디오 볼륨을 이전 값으로 조절하는 로직을 추가해야 합니다.
        // 예: AudioManager.SetVolume(originalBGMVolume, originalSEVolume);

        // 옵션 창을 비활성화합니다.
        gameObject.SetActive(false);
    }

    // 초기화 버튼을 눌렀을 때의 메서드
    public void ResetVolumeButton()
    {
        // 슬라이더 값을 기본 값으로 설정합니다.
        bgmSlider.value = 1f;
        seSlider.value = 1f;

        // 오디오 리스너의 볼륨을 기본 값으로 조절하는 로직을 추가해야 합니다.
        // 예: AudioManager.SetVolume(1f, 1f);
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private Slider Bgmslider;
    [SerializeField] private Slider SEslider;


    private float originalBGMVolume; // 처음 저장된 BGM 볼륨 값
    private float originalSEVolume;  // 처음 저장된 SE 볼륨 값

    private void Start()
    {
        Bgmslider.value = 1f;
        SEslider.value = 1f;
        LoadValues();
    }

    public void saveVolumeButton()
    {
        float BGMvolumeValue = Bgmslider.value;
        float SEvolumeValue = SEslider.value;
        PlayerPrefs.SetFloat("BGMvolumeValue", BGMvolumeValue);
        PlayerPrefs.SetFloat("SEvolumeValue", SEvolumeValue);
        LoadValues();

       
        // 여기에 옵션 창을 비활성화 하는 코드를 추가
        gameObject.SetActive(false);
        TitleManager.Destroy(gameObject);
    }

    void LoadValues()
    {
        originalBGMVolume = PlayerPrefs.GetFloat("BGMvolumeValue", 1f); // 기본 값은 1
        originalSEVolume = PlayerPrefs.GetFloat("SEvolumeValue", 1f); // 기본 값은 1

        Bgmslider.value = originalBGMVolume;
        SEslider.value = originalSEVolume;
        AudioListener.volume = originalBGMVolume; // BGM 볼륨만 설정
    }

    public void CancelButton()
    {
        Bgmslider.value = originalBGMVolume;
        SEslider.value = originalSEVolume;
        AudioListener.volume = originalBGMVolume; // BGM 볼륨만 설정

        // 여기에 옵션 창을 비활성화 하는 코드를 추가
        gameObject.SetActive(false);
        TitleManager.Destroy(gameObject);
    }

    public void resetVolumeButton()
    {
        Bgmslider.value = 1f;
        SEslider.value = 1f;
        AudioListener.volume = 1f;
    }
}
 */
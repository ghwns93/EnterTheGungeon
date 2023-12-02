using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider seSlider;
    public Button confirmButton;
    public Button cancelButton;
    public Button resetButton;

    private float originalBGMVolume;
    private float originalSEVolume;

    void Start()
    {
        originalBGMVolume = SoundManager.Instance.bgmVolume;
        originalSEVolume = SoundManager.Instance.seVolume;

        bgmSlider.value = originalBGMVolume;
        seSlider.value = originalSEVolume;

        confirmButton.onClick.AddListener(Confirm);
        cancelButton.onClick.AddListener(Cancel);
        resetButton.onClick.AddListener(Reset);
    }

    public void Confirm()
    {
        // 슬라이더에서 설정한 볼륨 값을 SoundManager에 적용
        SoundManager.Instance.SetBGMVolume(bgmSlider.value);
        SoundManager.Instance.SetSEVolume(seSlider.value);
        SliderManager.Destroy(gameObject);
    }

    public void Cancel()
    {
        // 슬라이더를 이전 설정 값으로 되돌림
        bgmSlider.value = originalBGMVolume;
        seSlider.value = originalSEVolume;
        SliderManager.Destroy(gameObject);
    }

    public void Reset()
    {
        // 슬라이더를 기본 설정 값으로 되돌림
        bgmSlider.value = 1.0f;
        seSlider.value = 1.0f;
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
using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private Slider Bgmslider;
    [SerializeField] private Slider SEslider;

    private void Start()
    {
        // 시작 시에 슬라이더 값 로드
        Bgmslider.value = PlayerPrefs.GetFloat("BGMvolumeValue", 1f);
        SEslider.value = PlayerPrefs.GetFloat("SEvolumeValue", 1f);
    }

    public void SaveVolumeButton()
    {
        // 슬라이더 값 저장
        PlayerPrefs.SetFloat("BGMvolumeValue", Bgmslider.value);
        PlayerPrefs.SetFloat("SEvolumeValue", SEslider.value);

        // 사운드 옵션을 닫습니다.
        CloseSoundOption();
    }

    public void ResetVolumeButton()
    {
        // 슬라이더 값을 기본값으로 리셋
        Bgmslider.value = 1f;
        SEslider.value = 1f;
        PlayerPrefs.SetFloat("BGMvolumeValue", 1f);
        PlayerPrefs.SetFloat("SEvolumeValue", 1f);
    }

    private void CloseSoundOption()
    {
        // 이 오브젝트가 사운드 옵션 UI라면 비활성화합니다.
        this.gameObject.SetActive(false);
        Time.timeScale = 1; // 게임의 시간 흐름을 정상으로 복원합니다.
    }

    public void DeskTop()
    {
        // 이 오브젝트가 사운드 옵션 UI라면 비활성화합니다.
        Application.Quit();
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
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
        // ���� �ÿ� �����̴� �� �ε�
        Bgmslider.value = PlayerPrefs.GetFloat("BGMvolumeValue", 1f);
        SEslider.value = PlayerPrefs.GetFloat("SEvolumeValue", 1f);
    }

    public void SaveVolumeButton()
    {
        // �����̴� �� ����
        PlayerPrefs.SetFloat("BGMvolumeValue", Bgmslider.value);
        PlayerPrefs.SetFloat("SEvolumeValue", SEslider.value);

        // ���� �ɼ��� �ݽ��ϴ�.
        CloseSoundOption();
    }

    public void ResetVolumeButton()
    {
        // �����̴� ���� �⺻������ ����
        Bgmslider.value = 1f;
        SEslider.value = 1f;
        PlayerPrefs.SetFloat("BGMvolumeValue", 1f);
        PlayerPrefs.SetFloat("SEvolumeValue", 1f);
    }

    private void CloseSoundOption()
    {
        // �� ������Ʈ�� ���� �ɼ� UI��� ��Ȱ��ȭ�մϴ�.
        this.gameObject.SetActive(false);
        Time.timeScale = 1; // ������ �ð� �帧�� �������� �����մϴ�.
    }

    public void DeskTop()
    {
        // �� ������Ʈ�� ���� �ɼ� UI��� ��Ȱ��ȭ�մϴ�.
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


    private float originalBGMVolume; // ó�� ����� BGM ���� ��
    private float originalSEVolume;  // ó�� ����� SE ���� ��

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

       
        // ���⿡ �ɼ� â�� ��Ȱ��ȭ �ϴ� �ڵ带 �߰�
        gameObject.SetActive(false);
        TitleManager.Destroy(gameObject);
    }

    void LoadValues()
    {
        originalBGMVolume = PlayerPrefs.GetFloat("BGMvolumeValue", 1f); // �⺻ ���� 1
        originalSEVolume = PlayerPrefs.GetFloat("SEvolumeValue", 1f); // �⺻ ���� 1

        Bgmslider.value = originalBGMVolume;
        SEslider.value = originalSEVolume;
        AudioListener.volume = originalBGMVolume; // BGM ������ ����
    }

    public void CancelButton()
    {
        Bgmslider.value = originalBGMVolume;
        SEslider.value = originalSEVolume;
        AudioListener.volume = originalBGMVolume; // BGM ������ ����

        // ���⿡ �ɼ� â�� ��Ȱ��ȭ �ϴ� �ڵ带 �߰�
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
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
        // �����̴����� ������ ���� ���� SoundManager�� ����
        SoundManager.Instance.SetBGMVolume(bgmSlider.value);
        SoundManager.Instance.SetSEVolume(seSlider.value);
        SliderManager.Destroy(gameObject);
    }

    public void Cancel()
    {
        // �����̴��� ���� ���� ������ �ǵ���
        bgmSlider.value = originalBGMVolume;
        seSlider.value = originalSEVolume;
        SliderManager.Destroy(gameObject);
    }

    public void Reset()
    {
        // �����̴��� �⺻ ���� ������ �ǵ���
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
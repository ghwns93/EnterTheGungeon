using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;

    private float originalBGMVolume; // ���� BGM ���� ��
    private float originalSEVolume; // ���� SE ���� ��

    private void Start()
    {
        // �����̴��� ���� �ε��մϴ�.
        LoadValues();
    }

    // ���� ���� �����ϰ� �ε��ϴ� �޼���
    public void SaveVolumeButton()
    {
        // �����̴��� ���� PlayerPrefs�� �����մϴ�.
        PlayerPrefs.SetFloat("BGMvolumeValue", bgmSlider.value);
        PlayerPrefs.SetFloat("SEvolumeValue", seSlider.value);

        // ���� �ٽ� �ε��Ͽ� �����մϴ�.
        LoadValues();

        // �ɼ� â�� ��Ȱ��ȭ�մϴ�.
        gameObject.SetActive(false);
    }

    // ����� ���� �ҷ����� �޼���
    void LoadValues()
    {
        // PlayerPrefs���� ���� ���� �����ɴϴ�. �⺻���� 1�Դϴ�.
        originalBGMVolume = PlayerPrefs.GetFloat("BGMvolumeValue", 1f);
        originalSEVolume = PlayerPrefs.GetFloat("SEvolumeValue", 1f);

        // �����̴��� ����� �����ʿ� ���� �����մϴ�.
        bgmSlider.value = originalBGMVolume;
        seSlider.value = originalSEVolume;
        // �� �κп��� ���� ������ ����� ������ �����ϴ� ������ �߰��ؾ� �մϴ�.
        // ��: AudioManager.SetVolume(originalBGMVolume, originalSEVolume);
    }

    // ��� ��ư�� ������ ���� �޼���
    public void CancelButton()
    {
        // �����̴� ���� ������� �����մϴ�.
        bgmSlider.value = originalBGMVolume;
        seSlider.value = originalSEVolume;

        // ���� ������ ����� ������ ���� ������ �����ϴ� ������ �߰��ؾ� �մϴ�.
        // ��: AudioManager.SetVolume(originalBGMVolume, originalSEVolume);

        // �ɼ� â�� ��Ȱ��ȭ�մϴ�.
        gameObject.SetActive(false);
    }

    // �ʱ�ȭ ��ư�� ������ ���� �޼���
    public void ResetVolumeButton()
    {
        // �����̴� ���� �⺻ ������ �����մϴ�.
        bgmSlider.value = 1f;
        seSlider.value = 1f;

        // ����� �������� ������ �⺻ ������ �����ϴ� ������ �߰��ؾ� �մϴ�.
        // ��: AudioManager.SetVolume(1f, 1f);
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
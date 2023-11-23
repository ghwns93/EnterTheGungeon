using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider seSlider;
    public Image bgmFillImage;
    public Image seFillImage;
    public Sprite grayImage; // ȸ�� �簢�� �̹���
    public Sprite whiteImage; // ��� �簢�� �̹���

    void Start()
    {
        InitializeSliders();
    }

    void InitializeSliders()
    {
        bgmSlider.onValueChanged.AddListener(delegate { UpdateVolume(bgmSlider, bgmFillImage); });
        seSlider.onValueChanged.AddListener(delegate { UpdateVolume(seSlider, seFillImage); });

        // �ʱ� ���� ����
        bgmSlider.value = 100f; // BGM �ʱ� ���� ����
        seSlider.value = 100f;  // SE �ʱ� ���� ����

        // �ʱ� �̹��� ����
        UpdateVolume(bgmSlider, bgmFillImage);
        UpdateVolume(seSlider, seFillImage);
    }

    void UpdateVolume(Slider slider, Image fillImage)
    {
        int whiteRectCount = Mathf.CeilToInt(slider.value / 5f);

        // �̹��� ���� �ʱ�ȭ
        fillImage.sprite = grayImage;

        // �Ͼ� �簢�� ������ŭ �̹��� ����
        for (int i = 0; i < whiteRectCount; i++)
        {
            fillImage.overrideSprite = whiteImage; // �����̽��� �̹����� �Ϻκ��� �Ͼ� �簢�� �̹����� ����
            // �̹��� �����̽��� Ȱ���Ͽ� �̹��� ����
            // ��: fillImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ??);
            //     fillImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ??);
        }

        // ���� ���� �ڵ� �߰� (���� AudioManager�� Ȱ���� ����)
        //AudioManager.SetVolume(slider.value); // AudioManager�� ������ �����ϴ� �޼��尡 �ִٰ� ����
    }
}
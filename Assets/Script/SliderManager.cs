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
    public Sprite grayImage; // 회색 사각형 이미지
    public Sprite whiteImage; // 흰색 사각형 이미지

    void Start()
    {
        InitializeSliders();
    }

    void InitializeSliders()
    {
        bgmSlider.onValueChanged.AddListener(delegate { UpdateVolume(bgmSlider, bgmFillImage); });
        seSlider.onValueChanged.AddListener(delegate { UpdateVolume(seSlider, seFillImage); });

        // 초기 볼륨 설정
        bgmSlider.value = 100f; // BGM 초기 볼륨 설정
        seSlider.value = 100f;  // SE 초기 볼륨 설정

        // 초기 이미지 설정
        UpdateVolume(bgmSlider, bgmFillImage);
        UpdateVolume(seSlider, seFillImage);
    }

    void UpdateVolume(Slider slider, Image fillImage)
    {
        int whiteRectCount = Mathf.CeilToInt(slider.value / 5f);

        // 이미지 색상 초기화
        fillImage.sprite = grayImage;

        // 하얀 사각형 개수만큼 이미지 변경
        for (int i = 0; i < whiteRectCount; i++)
        {
            fillImage.overrideSprite = whiteImage; // 슬라이스된 이미지의 일부분을 하얀 사각형 이미지로 변경
            // 이미지 슬라이싱을 활용하여 이미지 변경
            // 예: fillImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ??);
            //     fillImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ??);
        }

        // 볼륨 조절 코드 추가 (실제 AudioManager를 활용한 예시)
        //AudioManager.SetVolume(slider.value); // AudioManager에 볼륨을 조절하는 메서드가 있다고 가정
    }
}
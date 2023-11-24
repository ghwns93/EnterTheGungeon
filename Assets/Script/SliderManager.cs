using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Slider BgmSlider;
    public Slider SeSlider;

    private void Start()
    {
        // 슬라이더의 값이 변경될 때마다 호출되는 이벤트에 대한 리스너 추가
        BgmSlider.onValueChanged.AddListener(delegate { UpdateVolume(BgmSlider); });
        SeSlider.onValueChanged.AddListener(delegate { UpdateVolume(SeSlider); });
    }

    private void UpdateVolume(Slider slider)
    {
        // 현재 슬라이더의 값을 가져옴
        float value = slider.value;

        // 가장 가까운 5의 배수로 반올림
        float roundedValue = Mathf.Round(value / 5.0f) * 5.0f;

        // 반올림된 값을 슬라이더에 설정
        slider.value = roundedValue;

        // 여기에서 다른 원하는 동작 수행 가능
        Debug.Log("슬라이더 값: " + roundedValue);
    }
}
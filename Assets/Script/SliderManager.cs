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
        // �����̴��� ���� ����� ������ ȣ��Ǵ� �̺�Ʈ�� ���� ������ �߰�
        BgmSlider.onValueChanged.AddListener(delegate { UpdateVolume(BgmSlider); });
        SeSlider.onValueChanged.AddListener(delegate { UpdateVolume(SeSlider); });
    }

    private void UpdateVolume(Slider slider)
    {
        // ���� �����̴��� ���� ������
        float value = slider.value;

        // ���� ����� 5�� ����� �ݿø�
        float roundedValue = Mathf.Round(value / 5.0f) * 5.0f;

        // �ݿø��� ���� �����̴��� ����
        slider.value = roundedValue;

        // ���⿡�� �ٸ� ���ϴ� ���� ���� ����
        Debug.Log("�����̴� ��: " + roundedValue);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
        // �ν����Ϳ��� ��ư���� �Ҵ��մϴ�.
    public GameObject PlayBtn;  // ���ӽ���
    public GameObject OptionBtn;  // �ɼǹ�ư
    public GameObject QuitBtn;  // �������� ��ư

    // ĵ���� �Ҵ�
    public GameObject btnCanvas;  // �������� ��ư

    // �ɼ� Prefab�� �ν����Ϳ��� �Ҵ�
    public GameObject OptionPrefab;

    // Option Prefab�� �ν��Ͻ��� ������ ����
    private GameObject OptionInstance;

    // ���� ���� ù ȭ�� �̸��� �ν����Ϳ��� �Ҵ��մϴ�.
    public string firstSceneName;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.soundManager.PlayBGM(BGMType.Title);
    }

    // Update is called once per frame
    void Update()
    {
        // ESC Ű�� ������ �ɼ�â�� �ߵ��� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // �ɼ� Prefab�� ���� Ȱ��ȭ�Ǿ� �ִ� �� ���� �Ǵ�
            if (OptionInstance == null)
            {
                OptionButtonClicked();
            }
            else
            {
                // ������ �Ⱥ��̰�
                HideOptionCanvas();
            }
        }
    }

    // ��ŸƮ ��ư Ŭ�� �̺�Ʈ
    public void PlayButtonClicked()
    {
        // Scene �̵�
        SceneManager.LoadScene(firstSceneName);
    }

    // �ɼ� ��ư Ŭ�� �̺�Ʈ
    public void OptionButtonClicked()
    {
        // Option Prefab�� ȣ��
        OptionInstance = Instantiate(OptionPrefab);

        // Option Prefab���� Canvas ������Ʈ ��������
        Canvas canvas = OptionInstance.GetComponent<Canvas>();

        if (canvas != null)
        {
            // Canvas�� Render Mode�� Screen Space - Camera�� ����
            canvas.renderMode = RenderMode.ScreenSpaceCamera;

            // ���� ���� Main Camera�� �����ͼ� Render Camera�� �Ҵ�
            Camera mainCamera = Camera.main;

            if (mainCamera != null)
            {
                canvas.worldCamera = mainCamera;
            }
            else
            {
                Debug.LogError("����ī�޶� ����");
            }
        }
        else
        {
            Debug.LogError("�����տ� Canvas ������Ʈ�� ����");
        }
    }

    // ���� ���� ��ư Ŭ�� �̺�Ʈ
    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    // �ɼ�â �����
    void HideOptionCanvas()
    {
        // �ɼ� Prefab�� �����մϴ�.
        Destroy(OptionInstance);
        OptionInstance = null;
    }
}
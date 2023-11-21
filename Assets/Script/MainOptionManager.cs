using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainOptionManager : MonoBehaviour
{

    public GameObject gamePlayBtn;  // �����÷��� �ɼ�
    public GameObject controlBtn;  // ��Ʈ�� �ɼ� ��ư
    public GameObject videoBtn;  // ���� �ɼǹ�ư
    public GameObject audioBtn;  // ����� �ɼ� ��ư

    // �ɼ��� �Ҵ�
    public GameObject gamePlayPrefab;
    public GameObject controlPrefab;
    public GameObject videoPrefab;
    public GameObject audioPrefab;

    // Option Prefab�� �ν��Ͻ��� ������ ����
    private GameObject OptionInstance;

    // Start is called before the first frame update
    void Start()
    {
      
    }


    // Update is called once per frame
    void Update()
    {
        // ESC Ű�� ������ �ɼ�â�� �������� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // esc ������ ȭ�� ������
            HideOptionCanvas();
        }
    }

    public void GamePlayButtonClick()
    {
        OptionInstance = Instantiate(gamePlayPrefab);
        CanvasOpen();
    }

    public void ControlButtonClick()
    {
        OptionInstance = Instantiate(controlPrefab);
        CanvasOpen();
    }

    public void VideoButtonClick()
    {
        OptionInstance= Instantiate(videoPrefab);
        CanvasOpen();
    }

    public void AudioButtonClick()
    {
        OptionInstance= Instantiate(audioPrefab);
        CanvasOpen();
    }

    void HideOptionCanvas()
    {
        // �ɼ� Prefab�� �����մϴ�.
        Destroy(OptionInstance);
        OptionInstance = null;
    }

    public void CanvasOpen()
    {
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
}
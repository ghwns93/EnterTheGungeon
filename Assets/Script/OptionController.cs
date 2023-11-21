using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionController : MonoBehaviour
{
    public GameObject InGameOption;   // �ΰ��� �ɼ� ������
    public GameObject optionCanvas; // ���� �ɼ� ������
    public GameObject soundOptionCanvas; // ���� �ɼ� ������
    public GameObject indexCanvasPrefab; // ��ź ��Ȱ���� ������

    private bool isPaused = false;      // ���� �Ͻ������� Ȯ���ϱ� ���� bool��

    void Start()
    {
        // �ʱ⿡ ��� ĵ������ ��Ȱ��ȭ
        InGameOption.SetActive(true);
        optionCanvas.SetActive(false);
        soundOptionCanvas.SetActive(false);

        // �� ĵ������ Render Mode�� Camera�� �����ϰ� Render Camera�� Main Camera�� ����
        SetCanvasRenderMode(InGameOption);
        SetCanvasRenderMode(optionCanvas);
        SetCanvasRenderMode(soundOptionCanvas);
    }

    void Update()
    {
        // ESC Ű�� ���� ���� ������ ó���մϴ�.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (soundOptionCanvas.activeSelf)
            {
                // SoundOption ĵ������ ���� �ִٸ� Option ĵ������ ���ư��ϴ�.
                soundOptionCanvas.SetActive(false);
                optionCanvas.SetActive(true);
            }
            else if (optionCanvas.activeSelf)
            {
                // Option ĵ������ ���� �ִٸ� InGameOption ĵ������ ���ư��ϴ�.
                optionCanvas.SetActive(false);
                InGameOption.SetActive(true);
            }
            else if (InGameOption.activeSelf)
            {
                // InGameOption ĵ������ ���� �ִٸ� �������� ���ư��ϴ�.
                InGameOption.SetActive(false);
                ResumeGame();
            }
            else
            {
                // ���� �߿� ESC Ű�� ������ InGameOption ĵ������ �����ϴ�.
                InGameOption.SetActive(true);
                PauseGame();
            }
        }
    }

    public void OpenOptionMenu()
    {
        // InGameOption ĵ�������� Option ��ư�� ���� ���� ������ ó���մϴ�.
        InGameOption.SetActive(false);
        optionCanvas.SetActive(true);
    }

    public void OpenSoundOptionMenu()
    {
        // Option ĵ�������� SoundOption ��ư�� ���� ���� ������ ó���մϴ�.
        optionCanvas.SetActive(false);
        soundOptionCanvas.SetActive(true);
    }

    private void PauseGame()
    {
        // ������ �Ͻ� ����
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void ResumeGame()
    {
        // ������ �簳�մϴ�.
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Render Mode�� Camera�� �����ϰ� Render Camera�� Main Camera�� �����ϴ� �Լ�
    private void SetCanvasRenderMode(GameObject canvas)
    {
        Canvas canvasComponent = canvas.GetComponent<Canvas>();
        canvasComponent.renderMode = RenderMode.WorldSpace;
        canvasComponent.worldCamera = Camera.main;
    }

    // Index Canvas�� ���� ���� �Լ�
    private void OpenIndexCanvas()
    {
        // ���⿡ Index Canvas�� ���� ���� ������ �߰�
        Instantiate(indexCanvasPrefab);
    }
}
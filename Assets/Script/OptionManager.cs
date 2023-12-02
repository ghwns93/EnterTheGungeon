using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public GameObject IngameOptionPrefab; // �ΰ��� �ɼ� ������
    public GameObject SoundOptionPrefab; // ���� �ɼ� ������

    private GameObject ingameOptionInstance; // �ΰ��� �ɼ� �ν��Ͻ�
    private GameObject soundOptionInstance; // ���� �ɼ� �ν��Ͻ�

    // ������ �Ͻ����� ���¸� �����ϴ� �÷���
    private bool isGamePaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscapeKeyPress();
        }
    }

    public void HandleEscapeKeyPress()
    {
        // ���� �ɼ��� Ȱ��ȭ�Ǿ� ������, ���� �ɼ��� �ݰ� �ΰ��� �ɼ��� �ٽ� ����.
        if (soundOptionInstance != null)
        {
            Destroy(soundOptionInstance);
            soundOptionInstance = null;
            if (ingameOptionInstance == null)
            {
                ingameOptionInstance = Instantiate(IngameOptionPrefab);
                SetupCanvas(ingameOptionInstance);
            }
        }
        // �ΰ��� �ɼ��� Ȱ��ȭ�Ǿ� ������ �ݴ´�.
        else if (ingameOptionInstance != null)
        {
            Destroy(ingameOptionInstance);
            ingameOptionInstance = null;
            ResumeGame();
        }
        // �ƹ� �ɼǵ� Ȱ��ȭ�Ǿ� ���� ������ �ΰ��� �ɼ��� ����.
        else
        {
            ingameOptionInstance = Instantiate(IngameOptionPrefab);
            SetupCanvas(ingameOptionInstance);
            PauseGame();
        }
    }

    // ���� �ɼ��� ���ų� �ݴ� �޼���
    public void ToggleSoundOption(bool open)
    {
        if (open)
        {
            if (soundOptionInstance == null)
            {
                soundOptionInstance = Instantiate(SoundOptionPrefab);
                SetupCanvas(soundOptionInstance);
            }
        }
        else
        {
            if (soundOptionInstance != null)
            {
                Destroy(soundOptionInstance);
                soundOptionInstance = null;
            }
        }
    }

    // ������ �Ͻ� ���� �Ǵ� �簳�ϴ� �޼���
    public void PauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;
        // ��Ÿ �Ͻ������� ���õ� ó���� �߰��� �� �ֽ��ϴ�.
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        // ��Ÿ �簳�� ���õ� ó���� �߰��� �� �ֽ��ϴ�.
    }

    // Canvas ������ ���� ���� �޼���
    private void SetupCanvas(GameObject instance)
    {
        if (instance != null)
        {
            Canvas canvas = instance.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = Camera.main;
                canvas.sortingLayerName = "UI";
            }
            else
            {
                Debug.LogError("Instance does not have a Canvas component.");
            }
        }
    }

    // ���� ���� �޼��� (�ɼų�)
    public void ExitGame()
    {
        Application.Quit();
    }
}
/*
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class OptionManager : MonoBehaviour
{
    public GameObject IngameOption;
    public GameObject SoundOption;

    private GameObject ingameOptionInstance;
    private GameObject soundOptionInstance;
    private bool isGamePaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (soundOptionInstance != null)
            {
                // SoundOption�� Ȱ��ȭ�� ���¿��� ESC�� ������, SoundOption�� ��Ȱ��ȭ�ϰ� IngameOption�� Ȱ��ȭ
                ToggleSoundOption(false);
                ToggleIngameOption(true);
            }
            else if (ingameOptionInstance == null)
            {
                // IngameOption�� Ȱ��ȭ���� �ʾ��� �� ESC�� ������, IngameOption Ȱ��ȭ
                ToggleIngameOption(true);
            }
            else
            {
                // IngameOption�� Ȱ��ȭ�� ���¿��� ESC�� ������, IngameOption�� ��Ȱ��ȭ
                ToggleIngameOption(false);
            }
        }
    }

    public void OpenSoundOption()
    {
        // SoundOption ����
        ToggleSoundOption(true);
        ToggleIngameOption(false);
    }

    public void CloseSoundOption()
    {
        // SoundOption �ݱ�
        ToggleSoundOption(false);
        ToggleIngameOption(true);
    }

    private void ToggleIngameOption(bool isActive)
    {
        if (isActive)
        {
            ingameOptionInstance = Instantiate(IngameOption);
            PauseGame();
            SetupCanvas(ingameOptionInstance);
        }
        else if (ingameOptionInstance != null)
        {
            Destroy(ingameOptionInstance);
            ingameOptionInstance = null;
            ResumeGame();
        }
    }

    private void ToggleSoundOption(bool isActive)
    {
        if (isActive)
        {
            soundOptionInstance = Instantiate(SoundOption);
            SetupCanvas(soundOptionInstance);
        }
        else if (soundOptionInstance != null)
        {
            Destroy(soundOptionInstance);
            soundOptionInstance = null;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
    }

    private void SetupCanvas(GameObject instance)
    {
        if (instance != null)
        {
            Canvas canvas = instance.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = Camera.main;
                canvas.sortingLayerName = "UI";
            }
            else
            {
                Debug.LogError("�����տ� Canvas ������Ʈ�� ����");
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
 */
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject IngameOption;
    public GameObject SoundOption;

    private GameObject OptionInstance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionCanvas();
        }
    }


    public void OptionOpen()
    {
        OptionInstance = Instantiate(IngameOption);
        cameraRender();
      
    }

    public void OpenSoundOption()
    {
        Destroy(gameObject);
        OptionInstance = Instantiate(SoundOption);
        cameraRender();

    }

    private void ToggleOptionCanvas()
    {
        if (OptionInstance == null)
        {
            OptionOpen();
        }

        else
        {
            HideOption();
        }
    }
    private void HideOption()
    {
        // �ɼ� Prefab�� �����մϴ�.
        Destroy(OptionInstance);
        OptionInstance = null;
    }

    public void cameraRender()
    {
        if (OptionInstance != null)
        {
            Canvas canvas = OptionInstance.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
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

    public void ResumeGame()
    {
        Destroy(gameObject);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
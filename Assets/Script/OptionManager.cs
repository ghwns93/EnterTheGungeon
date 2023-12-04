using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public GameObject SoundOptionPrefab; // SoundOption2 프리팹에 대한 참조
    public static GameObject soundOptionInstance; // 현재 활성화된 SoundOption2 인스턴스

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (soundOptionInstance == null)
            {
                OpenSoundOption();
            }
            else
            {
                CloseSoundOption();
            }
        }
    }

    private void OpenSoundOption()
    {
        soundOptionInstance = Instantiate(SoundOptionPrefab);
        SetupCanvas(soundOptionInstance);
        PauseGame(); // 게임을 일시 정지 상태로 만듭니다.
    }

    public void CloseSoundOption()
    {
        if (soundOptionInstance != null)
        {
            Destroy(soundOptionInstance);
            soundOptionInstance = null;
        }
        ResumeGame(); // 게임을 재개합니다.
    }

    private void PauseGame()
    {
        Time.timeScale = 0; // 게임을 일시 정지합니다.
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // 게임의 시간 흐름을 정상으로 복원합니다.
    }

    private void SetupCanvas(GameObject instance)
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
            Debug.LogError("Canvas component is missing on the prefab!");
        }
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
                // SoundOption이 활성화된 상태에서 ESC를 누르면, SoundOption을 비활성화하고 IngameOption을 활성화
                ToggleSoundOption(false);
                ToggleIngameOption(true);
            }
            else if (ingameOptionInstance == null)
            {
                // IngameOption이 활성화되지 않았을 때 ESC를 누르면, IngameOption 활성화
                ToggleIngameOption(true);
            }
            else
            {
                // IngameOption이 활성화된 상태에서 ESC를 누르면, IngameOption을 비활성화
                ToggleIngameOption(false);
            }
        }
    }

    public void OpenSoundOption()
    {
        // SoundOption 열기
        ToggleSoundOption(true);
        ToggleIngameOption(false);
    }

    public void CloseSoundOption()
    {
        // SoundOption 닫기
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
                Debug.LogError("프리팹에 Canvas 컴포넌트가 없음");
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
 */
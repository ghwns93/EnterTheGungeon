using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public GameObject IngameOptionPrefab; // 인게임 옵션 프리팹
    public GameObject SoundOptionPrefab; // 사운드 옵션 프리팹

    private GameObject ingameOptionInstance; // 인게임 옵션 인스턴스
    private GameObject soundOptionInstance; // 사운드 옵션 인스턴스

    // 게임의 일시정지 상태를 추적하는 플래그
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
        // 사운드 옵션이 활성화되어 있으면, 사운드 옵션을 닫고 인게임 옵션을 다시 연다.
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
        // 인게임 옵션이 활성화되어 있으면 닫는다.
        else if (ingameOptionInstance != null)
        {
            Destroy(ingameOptionInstance);
            ingameOptionInstance = null;
            ResumeGame();
        }
        // 아무 옵션도 활성화되어 있지 않으면 인게임 옵션을 연다.
        else
        {
            ingameOptionInstance = Instantiate(IngameOptionPrefab);
            SetupCanvas(ingameOptionInstance);
            PauseGame();
        }
    }

    // 사운드 옵션을 열거나 닫는 메서드
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

    // 게임을 일시 정지 또는 재개하는 메서드
    public void PauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;
        // 기타 일시정지와 관련된 처리를 추가할 수 있습니다.
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        // 기타 재개와 관련된 처리를 추가할 수 있습니다.
    }

    // Canvas 설정을 위한 보조 메서드
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

    // 게임 종료 메서드 (옵셔널)
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
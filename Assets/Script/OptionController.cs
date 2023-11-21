using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionController : MonoBehaviour
{
    public GameObject InGameOption;   // 인게임 옵션 프리팹
    public GameObject optionCanvas; // 메인 옵션 프리팹
    public GameObject soundOptionCanvas; // 사운드 옵션 프리팹
    public GameObject indexCanvasPrefab; // 총탄 부활술서 프리팹

    private bool isPaused = false;      // 게임 일시정지를 확인하기 위한 bool형

    void Start()
    {
        // 초기에 모든 캔버스를 비활성화
        InGameOption.SetActive(true);
        optionCanvas.SetActive(false);
        soundOptionCanvas.SetActive(false);

        // 각 캔버스의 Render Mode를 Camera로 설정하고 Render Camera를 Main Camera로 지정
        SetCanvasRenderMode(InGameOption);
        SetCanvasRenderMode(optionCanvas);
        SetCanvasRenderMode(soundOptionCanvas);
    }

    void Update()
    {
        // ESC 키를 누를 때의 동작을 처리합니다.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (soundOptionCanvas.activeSelf)
            {
                // SoundOption 캔버스가 켜져 있다면 Option 캔버스로 돌아갑니다.
                soundOptionCanvas.SetActive(false);
                optionCanvas.SetActive(true);
            }
            else if (optionCanvas.activeSelf)
            {
                // Option 캔버스가 켜져 있다면 InGameOption 캔버스로 돌아갑니다.
                optionCanvas.SetActive(false);
                InGameOption.SetActive(true);
            }
            else if (InGameOption.activeSelf)
            {
                // InGameOption 캔버스가 켜져 있다면 게임으로 돌아갑니다.
                InGameOption.SetActive(false);
                ResumeGame();
            }
            else
            {
                // 게임 중에 ESC 키를 누르면 InGameOption 캔버스가 켜집니다.
                InGameOption.SetActive(true);
                PauseGame();
            }
        }
    }

    public void OpenOptionMenu()
    {
        // InGameOption 캔버스에서 Option 버튼을 누를 때의 동작을 처리합니다.
        InGameOption.SetActive(false);
        optionCanvas.SetActive(true);
    }

    public void OpenSoundOptionMenu()
    {
        // Option 캔버스에서 SoundOption 버튼을 누를 때의 동작을 처리합니다.
        optionCanvas.SetActive(false);
        soundOptionCanvas.SetActive(true);
    }

    private void PauseGame()
    {
        // 게임을 일시 정지
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void ResumeGame()
    {
        // 게임을 재개합니다.
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Render Mode를 Camera로 설정하고 Render Camera를 Main Camera로 지정하는 함수
    private void SetCanvasRenderMode(GameObject canvas)
    {
        Canvas canvasComponent = canvas.GetComponent<Canvas>();
        canvasComponent.renderMode = RenderMode.WorldSpace;
        canvasComponent.worldCamera = Camera.main;
    }

    // Index Canvas를 열기 위한 함수
    private void OpenIndexCanvas()
    {
        // 여기에 Index Canvas를 열기 위한 로직을 추가
        Instantiate(indexCanvasPrefab);
    }
}
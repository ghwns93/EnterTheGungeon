using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainOptionManager : MonoBehaviour
{

    public GameObject gamePlayBtn;  // 게임플레이 옵션
    public GameObject controlBtn;  // 컨트롤 옵션 버튼
    public GameObject videoBtn;  // 비디오 옵션버튼
    public GameObject audioBtn;  // 오디오 옵션 버튼

    // 옵션을 할당
    public GameObject gamePlayPrefab;
    public GameObject controlPrefab;
    public GameObject videoPrefab;
    public GameObject audioPrefab;

    // Option Prefab의 인스턴스를 저장할 변수
    private GameObject OptionInstance;

    // Start is called before the first frame update
    void Start()
    {
      
    }


    // Update is called once per frame
    void Update()
    {
        // ESC 키를 누르면 옵션창이 꺼지도록 설정
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // esc 누르면 화면 꺼지기
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
        // 옵션 Prefab을 제거합니다.
        Destroy(OptionInstance);
        OptionInstance = null;
    }

    public void CanvasOpen()
    {
        // Option Prefab에서 Canvas 컴포넌트 가져오기
        Canvas canvas = OptionInstance.GetComponent<Canvas>();

        if (canvas != null)
        {
            // Canvas의 Render Mode를 Screen Space - Camera로 설정
            canvas.renderMode = RenderMode.ScreenSpaceCamera;

            // 현재 씬의 Main Camera를 가져와서 Render Camera에 할당
            Camera mainCamera = Camera.main;

            if (mainCamera != null)
            {
                canvas.worldCamera = mainCamera;
            }
            else
            {
                Debug.LogError("메인카메라가 없음");
            }
        }
        else
        {
            Debug.LogError("프리팹에 Canvas 컴포넌트가 없음");
        }
    }
}
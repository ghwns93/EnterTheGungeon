using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
        // 인스펙터에서 버튼들을 할당합니다.
    public GameObject PlayBtn;  // 게임시작
    public GameObject OptionBtn;  // 옵션버튼
    public GameObject QuitBtn;  // 게임종료 버튼

    // 캔버스 할당
    public GameObject btnCanvas;  // 게임종료 버튼

    // 옵션 Prefab을 인스펙터에서 할당
    public GameObject OptionPrefab;

    // Option Prefab의 인스턴스를 저장할 변수
    private GameObject OptionInstance;

    // 게임 시작 첫 화면 이름을 인스펙터에서 할당합니다.
    public string firstSceneName;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.soundManager.PlayBGM(BGMType.Title);
    }

    // Update is called once per frame
    void Update()
    {
        // ESC 키를 누르면 옵션창이 뜨도록 설정
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 옵션 Prefab이 현재 활성화되어 있는 지 여부 판단
            if (OptionInstance == null)
            {
                OptionButtonClicked();
            }
            else
            {
                // 없으면 안보이게
                HideOptionCanvas();
            }
        }
    }

    // 스타트 버튼 클릭 이벤트
    public void PlayButtonClicked()
    {
        // Scene 이동
        SceneManager.LoadScene(firstSceneName);
    }

    // 옵션 버튼 클릭 이벤트
    public void OptionButtonClicked()
    {
        // Option Prefab을 호출
        OptionInstance = Instantiate(OptionPrefab);

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

    // 게임 종료 버튼 클릭 이벤트
    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    // 옵션창 숨기기
    void HideOptionCanvas()
    {
        // 옵션 Prefab을 제거합니다.
        Destroy(OptionInstance);
        OptionInstance = null;
    }
}
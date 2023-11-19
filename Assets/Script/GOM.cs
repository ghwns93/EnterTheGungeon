using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GOM: MonoBehaviour
{
   // 옵션을 할당
    public GameObject OptionPrefab;
    public GameObject InoptionPrefab;

    public GameObject Playingbtn;  // 게임시작
    public GameObject Optionbtn;  // 옵션버튼
    public GameObject Indexbtn;  // 게임종료 버튼
    public GameObject EXITbtn;  // 게임종료 버튼

    // Option Prefab의 인스턴스를 저장할 변수
    private GameObject OptionInstance;

    // Start is called before the first frame update
    void Start()
    {

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
                OpenInOption();
            }

            else
            {
                // 없으면 안보이게
                HideOptionCanvas();
            }
        }
    }
    
    public void OpenInOption() 
    {

        // Option Prefab을 호출
        OptionInstance = Instantiate(InoptionPrefab);

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
    // 스타트 버튼 클릭 이벤트
    public void PlayingButtonClicked()
    {
        Destroy(OptionInstance);
        OptionInstance = null;
    }

    // 옵션 버튼 클릭 이벤트
    public void OptionButtonClicked()
    {
        InoptionPrefab.SetActive(false);

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

        // ESC 키를 누르면 옵션창이 뜨도록 설정
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InoptionPrefab.SetActive(true);
            HideOptionCanvas();

        }



    }

    // 게임 종료 버튼 클릭 이벤트
    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    public void HideOptionCanvas()
    {
        // 옵션 Prefab을 제거합니다.
        Destroy(OptionInstance);
        OptionInstance = null;
    }
}

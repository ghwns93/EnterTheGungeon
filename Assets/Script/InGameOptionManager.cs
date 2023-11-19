using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameOptionManager : MonoBehaviour
{
    // 옵션을 할당
    public GameObject OptionPrefab;
    public GameObject InoptionPrefab;


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


    void HideOptionCanvas()
    {
        // 옵션 Prefab을 제거합니다.
        Destroy(OptionInstance);
        OptionInstance = null;
    }
}


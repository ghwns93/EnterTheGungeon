using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BookSceneChange : MonoBehaviour
{
    void Start()
    {
        // Button 컴포넌트 가져오기
        Button button = GetComponent<Button>();

        // 버튼에 클릭 이벤트 리스너 추가
        button.onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked()
    {
        Debug.Log("Button Clicked!");
        // 원하는 동작 추가
    }

    // Update is called once per frame
    void Update()
    {        
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Lobby");
    }
}

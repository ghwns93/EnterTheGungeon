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
    }

    public void ButtonClicked()
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

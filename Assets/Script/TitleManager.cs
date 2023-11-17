using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject PlayBtn;  // 게임시작
    public GameObject OptionBtn;  // 옵션버튼
    public GameObject QuitBtn;  // 게임종료 버튼

    public GameObject OptionPrefab;

    public string firstSceneName;   // 게임 시작 첫 화면 이름

    // Start is called before the first frame update
    void Start()
    {
 
        // 타이틀 BGM 재생
        //SoundManager.soundManager.PlayBGM(BGMType.Title);
    }

    // Update is called once per frame
    void Update() { }

    // 스타트 버튼
    public void PlayButtonClicked()
    {
        // Scene 이동
        SceneManager.LoadScene(firstSceneName);

    }

    //옵션버튼
    public void OptionButtonClicked()
    {
        Instantiate(OptionPrefab, gameObject.transform.position, Quaternion.identity);
    }

    // 게임종료버튼
    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}

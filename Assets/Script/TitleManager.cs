using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject PlayBtn;  // ���ӽ���
    public GameObject OptionBtn;  // �ɼǹ�ư
    public GameObject QuitBtn;  // �������� ��ư

    public GameObject OptionPrefab;

    public string firstSceneName;   // ���� ���� ù ȭ�� �̸�

    // Start is called before the first frame update
    void Start()
    {
 
        // Ÿ��Ʋ BGM ���
        //SoundManager.soundManager.PlayBGM(BGMType.Title);
    }

    // Update is called once per frame
    void Update() { }

    // ��ŸƮ ��ư
    public void PlayButtonClicked()
    {
        // Scene �̵�
        SceneManager.LoadScene(firstSceneName);

    }

    //�ɼǹ�ư
    public void OptionButtonClicked()
    {
        Instantiate(OptionPrefab, gameObject.transform.position, Quaternion.identity);
    }

    // ���������ư
    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}

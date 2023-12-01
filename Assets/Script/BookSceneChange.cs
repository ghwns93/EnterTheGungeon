using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BookSceneChange : MonoBehaviour
{
    void Start()
    {
        // Button ������Ʈ ��������
        Button button = GetComponent<Button>();

        // ��ư�� Ŭ�� �̺�Ʈ ������ �߰�
        button.onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked()
    {
        Debug.Log("Button Clicked!");
        // ���ϴ� ���� �߰�
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

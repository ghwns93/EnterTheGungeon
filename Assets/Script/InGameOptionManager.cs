using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameOptionManager : MonoBehaviour
{
    // �ɼ��� �Ҵ�
    public GameObject OptionPrefab;
    public GameObject InoptionPrefab;


    // Option Prefab�� �ν��Ͻ��� ������ ����
    private GameObject OptionInstance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ESC Ű�� ������ �ɼ�â�� �ߵ��� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // �ɼ� Prefab�� ���� Ȱ��ȭ�Ǿ� �ִ� �� ���� �Ǵ�
            if (OptionInstance == null)
            {
                OpenInOption();
            }
            else
            {
                // ������ �Ⱥ��̰�
                HideOptionCanvas();
            }
        }
    }

    public void OpenInOption()
    {


        // Option Prefab�� ȣ��
        OptionInstance = Instantiate(InoptionPrefab);

        // Option Prefab���� Canvas ������Ʈ ��������
        Canvas canvas = OptionInstance.GetComponent<Canvas>();

        if (canvas != null)
        {
            // Canvas�� Render Mode�� Screen Space - Camera�� ����
            canvas.renderMode = RenderMode.ScreenSpaceCamera;

            // ���� ���� Main Camera�� �����ͼ� Render Camera�� �Ҵ�
            Camera mainCamera = Camera.main;

            if (mainCamera != null)
            {
                canvas.worldCamera = mainCamera;
            }
            else
            {
                Debug.LogError("����ī�޶� ����");
            }
        }
        else
        {
            Debug.LogError("�����տ� Canvas ������Ʈ�� ����");
        }

    }


    void HideOptionCanvas()
    {
        // �ɼ� Prefab�� �����մϴ�.
        Destroy(OptionInstance);
        OptionInstance = null;
    }
}


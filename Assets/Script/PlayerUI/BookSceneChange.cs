using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BookSceneChange : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {        
    }

    public void ChangeSceneToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void ChangeSceneToMainStage()
    {
        SceneManager.LoadScene("MainStage");
        //SceneManager.LoadScene("MainStage", LoadSceneMode.Single);
    }
}

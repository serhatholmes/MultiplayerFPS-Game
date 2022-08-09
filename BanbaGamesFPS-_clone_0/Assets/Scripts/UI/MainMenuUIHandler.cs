using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUIHandler : MonoBehaviour
{
    public TMP_InputField inputField;

    
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerNickname"))
            inputField.text = PlayerPrefs.GetString("PlayerNickname");
    }

    public void OnJoinGameClicked()
    {
        PlayerPrefs.SetString("PlayerNickname", inputField.text);
        PlayerPrefs.Save();

        SceneManager.LoadScene("PlayGround");
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void OnStartButton()
    {
        string name = nameInput.text;
        if (string.IsNullOrEmpty(name))
            name = "Jugador";

        GameManager.Instance.currentPlayerName = name;
        SceneManager.LoadScene("SCN_Level");
    }
}
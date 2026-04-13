using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Events : MonoBehaviour
{
    public GameObject pauseMenu, Tutor, AD, Helper, hintButton, hintWindow, window;
    private bool isPause = false;
    private bool isHelping = false;

    void Start()
    {
        pauseMenu.SetActive(isPause);
        if (!PlayerPrefs.HasKey("firstStart")) AD.SetActive(true); //проверка для обучения
        else Tutor.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
        if (isPause) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }

    public void Pause() //пауза
    {
        Helper.GetComponent<Helper>().ReLoad(); //чтобы без перезахода были новые пункты справочника
        isHelping = false;
        isPause = !isPause;
        pauseMenu.SetActive(isPause);
    }

    public void Leave() //выход в меню
    {
        PlayerPrefs.Save();
        isPause = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenHelper()
    {
        isHelping = !isHelping;
        Helper.SetActive(isHelping);
        pauseMenu.SetActive(!isHelping);
    }

    public void CloseHint()
    {
        hintWindow.SetActive(false);
        hintButton.SetActive(true);
        window.SetActive(true);
    }
}

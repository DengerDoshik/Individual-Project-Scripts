using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class EventSystem : MonoBehaviour
{
    public GameObject ExitVariant;
    public GameObject MainMenu;

    public void ExitButton()
    {
        ExitVariant.SetActive(true);
        MainMenu.SetActive(false);
    }
    public void YesExit()
    {
        Application.Quit();
        //EditorApplication.ExitPlaymode();
    }
    public void NoExit()
    {
        ExitVariant.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("Sandbox");
    }

    public void Start()
    {
        if (!PlayerPrefs.HasKey("firstStart")) PlayerPrefs.DeleteAll(); //очистка лишних данных на всякий случай
    }
}

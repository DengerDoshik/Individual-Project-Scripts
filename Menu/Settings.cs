using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject Menu;

    public void Open()
    {
        Menu.SetActive(false);
        gameObject.SetActive(true);
    }

    public void Close()
    {
        Menu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
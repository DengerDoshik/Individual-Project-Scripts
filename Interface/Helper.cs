using UnityEngine;

public class Helper : MonoBehaviour
{
    public GameObject [] buttons;
    public GameObject [] bodies;
    public GameObject menu, bodymenu;
    int lastnumber;

    public void OpenHelp(int number)
    {
        bodymenu.SetActive(true);
        bodies[number].SetActive(true);
        menu.SetActive(false);
        lastnumber = number;
    }

    public void ReLoad()
    {
        foreach (GameObject but in buttons) if (!PlayerPrefs.HasKey(but.name)) but.SetActive(false);
        gameObject.SetActive(false);
        foreach (GameObject body in bodies) body.SetActive(false);
    }

    public void CloseHelp()
    {
        bodymenu.SetActive(false);
        bodies[lastnumber].SetActive(false);
        menu.SetActive(true);
    }
}

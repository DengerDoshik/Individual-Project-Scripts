using UnityEngine;

public class FirstEnter : MonoBehaviour
{
    public GameObject AD;
    public GameObject jump, jumpTrigger;
    public GameObject end;

    void Start()
    {
        if (PlayerPrefs.HasKey("firstStart")) AD.SetActive(true);
        else gameObject.SetActive(false);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other == jumpTrigger)
        {
            AD.SetActive(false);
            jump.SetActive(true);
        }
        if (other == end)
        {
            PlayerPrefs.SetString("firstStart", "yes");
            gameObject.SetActive(false);
        }
    }
}

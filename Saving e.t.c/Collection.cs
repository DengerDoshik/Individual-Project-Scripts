using UnityEngine;

public class Collection : MonoBehaviour
{
    public bool isTouching = true;
    public GameObject leave; //выход
    public GameObject Ebutton; //подсказка
    protected string imya;
    private double t = 0;
    public GameObject TopText, BottomText, image; //текст сверху, снизу и картинка способности

    void Start()
    {
        imya = gameObject.name;
        Ebutton.SetActive(false);
        if (PlayerPrefs.HasKey(imya)) gameObject.SetActive(false);
    }

    void Update()
    {
        if (leave.GetComponent<LeaveCheck>().isTouch == true) isTouching = false;

        if (isTouching)
        {
            Ebutton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                //сделать анимку
                image.SetActive(true);
                TopText.SetActive(true);
                BottomText.SetActive(true);
                PlayerPrefs.SetString(imya, "yes");
                isTouching = false;
                Debug.Log(imya);
            }
        }

        if (BottomText.activeSelf) t += Time.deltaTime;

        if (t >= 5) 
        { 
            image.SetActive(false);
            TopText.SetActive(false);
            BottomText.SetActive(false);
            t = 0;
            Start();
        }
        if (!isTouching) Ebutton.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D others)
    {
        if (others.GetComponent<Player>()) isTouching = true;
    }

}

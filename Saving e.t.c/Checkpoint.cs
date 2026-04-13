using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private float myX, myY;
    public bool isTouched = false;
    public GameObject leave; //выход из сейва
    public GameObject Ebutton; // подсказка

    void Start()
    {
        Ebutton.SetActive(false);
        myX = transform.position.x;
        myY = transform.position.y;
    }

    void Update()
    {
        if (leave.GetComponent<LeaveCheck>().isTouch == true) isTouched = false;

        if (isTouched && PlayerPrefs.GetFloat("saveX") != myX && PlayerPrefs.GetFloat("saveY") != myY)
        {
            Ebutton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerPrefs.SetFloat("saveX",  myX);
                PlayerPrefs.SetFloat("saveY", myY);
                Debug.Log("Saved");
                isTouched = false;
            }
        }

        if(!isTouched) Ebutton.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D others)
    {
        if (others.GetComponent<Player>()) isTouched = true;
    }

}

using UnityEngine;

public class LeaveCheck : MonoBehaviour
{
    public bool isTouch = false;
    public GameObject parent; //сам сейвер

    void Update()
    {
        if (parent.GetComponent<Checkpoint>()) if (parent.GetComponent<Checkpoint>().isTouched == false) isTouch = false;
        if (parent.GetComponent<Collection>()) if (parent.GetComponent<Collection>().isTouching == false) isTouch = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>()) isTouch = true;
    }
}

using UnityEngine;

public class ButtonClicker : MonoBehaviour
{
    public bool isClicked = false;

    public void Click()
    {
        isClicked = !isClicked;
    }
}

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    Vector3 Target;
    public float speed = 1f;
    public bool isMove = true;


    void Update()
    {
        isMove = player.GetComponent<Player>().isCamMove;

        Vector3 currentPos = Vector3.Lerp(transform.position, Target, speed*Time.deltaTime);
        transform.position = currentPos;

        if (isMove) Target = new Vector3(player.transform.position.x, player.transform.position.y + 9, -25);
        if (!isMove) Target = new Vector3(player.GetComponent<Player>().camx, player.GetComponent<Player>().camy, -25); //корды стоянки
    }
}

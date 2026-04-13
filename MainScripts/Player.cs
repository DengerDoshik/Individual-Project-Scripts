using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float hight = 5.0f;
    public float accel = 5.0f;
    private float startSpeed;
    public float max_acc = 4f; //множитель максимальной скорости от ускорения

    public float rayDistance = 0.03f; //длина луча до земли
    private bool isGrounded = true;

    public float camx, camy;
    public bool isCamMove = true;
    private float startX, startY;

    public GameObject AD, jump, firstEnter;
    public Collider2D jumpTrigger, end;

    private string taskType; //тип задачи
    private float buffing; //усиление с последней задачи
    private string buffingType; //что конкретно усилять (сделать через префы)

    Rigidbody2D rb;
    SpriteRenderer sr; 

    void Start()
    {
        startSpeed = speed;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        if (PlayerPrefs.HasKey("firstStart")) //проверка на спавн (чтобы первый раз не делать корды для спавна)
        {
            startX = PlayerPrefs.GetFloat("saveX");
            startY = PlayerPrefs.GetFloat("saveY");
            transform.position = new Vector3(startX, startY, -1);
        }
        Restart();
    }

    void Update()
    {
        Moving();
        Jump();
        if(PlayerPrefs.HasKey("Acceleration")) Acceleration();

        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, rayDistance, LayerMask.GetMask("Ground"));
        if (hit.collider != null) isGrounded = true;
        else isGrounded = false;
    }

    void Moving() //движение
    {

        float deltaX = Input.GetAxis("Horizontal");
        transform.position += new Vector3(deltaX, 0, 0) * speed * Time.deltaTime;

        if (deltaX > 0) sr.flipX = true;
        if (deltaX < 0) sr.flipX = false;

    }

    void Jump() //прыжок
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) rb.AddForce(new Vector2(0, hight), ForceMode2D.Impulse);
    }

    void Acceleration() //ускорение
    {
        if (Input.GetKey(KeyCode.LeftShift) && speed < accel * max_acc) speed += 2*accel * Time.deltaTime;
        else if (speed > startSpeed) speed -= 2*accel * Time.deltaTime;
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.GetComponent<Tasker>()) //коснулся стоянки
        {
            taskType = other.GetComponent<Tasker>().type;
            if (PlayerPrefs.HasKey(taskType))
            {
                camx = other.GetComponent<Tasker>().posx;
                camy = other.GetComponent<Tasker>().posy;
                isCamMove = false;

                buffing = other.GetComponent<Tasker>().buff; //взять данные на прокачку
                buffingType = other.GetComponent<Tasker>().BuffType;
            }
        }
            
        if (other == jumpTrigger) //триггер с обучением
        {
            AD.SetActive(false);
            jump.SetActive(true);
        }
        
        if (other == end) //триггер конца обучения
        {
            firstEnter.SetActive(false);
            PlayerPrefs.SetString("firstStart", "yes");
        }
    }

    void Restart()
    {
        if (PlayerPrefs.GetFloat("PlayerSpeed") != 0f) speed = PlayerPrefs.GetFloat("PlayerSpeed");
        if (PlayerPrefs.GetFloat("PlayerHight") != 0f) hight = PlayerPrefs.GetFloat("PlayerHight");
    }

    public void GoOn() //метод для вруба камеры
    {
        isCamMove = true;
        if (buffingType == "s") PlayerPrefs.SetFloat("PlayerSpeed", speed + buffing);
        if (buffingType == "h") PlayerPrefs.SetFloat("PlayerHight", hight + buffing);
        Restart();
    }
}

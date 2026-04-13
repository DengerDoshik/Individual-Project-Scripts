using UnityEngine;

public class Car : MonoBehaviour
{
    public float S, h, Vx, Vy, t, a, startingT; //скорость, врем€, ускорение, врем€ дл€ запуска (дл€ задач с двум€ машинами)
    public float g = 0f;
    private float startVx, startVy, startA, startT, startG;

    public float timer = 0f;
    private float currentX;
    private float currentY;

    GameObject vec; //вектор
    Collider2D col, vecol; //коллайдер машины и вектора
    SpriteRenderer vecsr; //спрайтрендер дл€ вектора

    void Start()
    {
        try { vec = GameObject.Find(gameObject.name + "/¬ектор 1_0"); //чтобы не перетаскивать вектор на объект
            vecol = vec.GetComponent<Collider2D>();
            vecsr = vec.GetComponent<SpriteRenderer>();
        }
        catch { Debug.Log("Ќет вектора");
            vec = null;
        }
        col = GetComponent<Collider2D>();

        startVx = Vx;
        startVy = Vy;
        startT = t; 
        startA = a;
        startG = g;

        S = Mathf.Abs(Vx * (t - startingT) + (t - startingT) * (t - startingT) * a / 2);
        h = Mathf.Abs(Vy * t - t * t * g / 2);

        if (vec != null) vec.SetActive(false);
        
        if (Vx < 0 && vec != null)
        {
            vec.transform.position = new Vector2(vec.transform.position.x - (col.bounds.size.x + vecol.bounds.size.x)*1.43f, vec.transform.position.y); //дл€ сдвига в лево машины
            vecsr.flipX = true;
        }
        currentX = transform.position.x;
        currentY = transform.position.y;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && timer == 0) //начало отсчЄта
        {
            Vx = startVx;
            Vy = startVy;
            a = startA;
            StartCoroutine(Move());
        }
    }

    private System.Collections.IEnumerator Move() //корутина на движение
    {
        transform.position = new Vector3(currentX, currentY, 0);
        while (timer < t)
        {
            if (timer >= startingT)
            {
                if (vec != null) vec.SetActive(true);
                transform.Translate(Vector2.right * Vx * Time.deltaTime);
                transform.Translate(Vector2.up * Vy * Time.deltaTime);
                Vx += a * Time.deltaTime;
                Vy -= g * Time.deltaTime;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        Vx = startVx;
        Vy = startVy;
        t = startT;
        a = startA;
        g = startG;
        if (vec != null) vec.SetActive(false);
    }

    public void WrongAns(float wrongAns, string ans) 
    {
        StopCoroutine(Move());
        if (ans == "vx") Vx = wrongAns;
        if (ans == "vy") Vy = wrongAns;
        if (ans == "a") a = wrongAns;
        if (ans == "t") t = wrongAns;
        StartCoroutine(Move());
    }
}

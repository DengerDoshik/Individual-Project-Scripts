//using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class Tasker : MonoBehaviour
{
    public float posx, posy; //свои корды
    private bool isnt = false; //is in task
    public GameObject [] objects; //привязанные объекты
    public GameObject[] walls;   
    public int chosen = 0; //определяет, у какого объекта брать ответ и условия
    public GameObject window; //канвас
    public TMP_InputField inf; //окно для ответа
    public TextMeshProUGUI text;//куда писать условия
    public string myName, type; //название для сохранения и тип задачи
    public GameObject player;
    private string usl;
    public int baseNum;
    public string what; 
    private string answer;
    private bool activated = false;

    private int ans_count = 0; //на 3 открывается подсказка
    public int ans_num; //номер для подсказки

    public GameObject hintButton;
    public GameObject hint; //сама подсказка
    public GameObject hintWindow; //окно с подсказкой
    public GameObject clicker;

    private GameObject dataBase;

    public float buff; //улучшение после задачи (число)
    public string BuffType; //тип улучшения (прыжок, скорость и т.п.)

    //только для задач на скорость (Timer)
    public TextMeshProUGUI timertext; //секундомер

    void Start()
    {
        dataBase = GameObject.Find("DataBase");

        what = dataBase.GetComponent<DataBase>().whats[baseNum];
        buff = dataBase.GetComponent<DataBase>().buffs[baseNum];
        BuffType = dataBase.GetComponent<DataBase>().buffTypes[baseNum];
        usl = dataBase.GetComponent<DataBase>().uslovia[baseNum];

        myName = gameObject.name;
        if (PlayerPrefs.GetInt(myName) == -1) gameObject.SetActive(false); 
        posx = transform.position.x;
        posy = transform.position.y;
        foreach (GameObject obj in objects) obj.SetActive(false);
        foreach (GameObject obj in walls) obj.SetActive(false);

        if (what == "vx") answer = objects[chosen].GetComponent<Car>().Vx.ToString();
        if (what == "vy") answer = objects[chosen].GetComponent<Car>().Vy.ToString();
        if (what == "s") answer = objects[chosen].GetComponent<Car>().S.ToString();
        if (what == "t") answer = objects[chosen].GetComponent<Car>().t.ToString();
        if (what == "a") answer = objects[chosen].GetComponent<Car>().a.ToString();
        if (what == "g") answer = objects[chosen].GetComponent<Car>().g.ToString();

        hint.SetActive(false);
        hintButton.SetActive(false);
    }

    void Update()
    {
        if (isnt)
        {
            if (!activated)
            {
                foreach (GameObject obj in objects) obj.SetActive(true);
                foreach (GameObject obj in walls) obj.SetActive(true);
                window.SetActive(true);
                activated = true;
            }
                if (type == "Timer")
            {
                text.text = usl;
                if (objects[chosen].GetComponent<Car>().timer != 0f) timertext.text = Mathf.Floor(objects[chosen].GetComponent<Car>().timer).ToString();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return)) CheckAnswer();

        if (ans_count >= 3) 
        { 
            hintButton.SetActive(true);
        }

        if (clicker.GetComponent<ButtonClicker>().isClicked)
        {
            OpenHint();
            clicker.GetComponent<ButtonClicker>().Click();
        }
        else hint.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() && PlayerPrefs.HasKey(type)) 
        {
            isnt = true;
            foreach (GameObject task in dataBase.GetComponent<DataBase>().tasks) if (task.name != gameObject.name) task.SetActive(false);
        }
    }

    public void CheckAnswer()
    {
        if (type == "Timer")
        {
            if (answer == inf.text)
            {
                isnt = false;
                window.SetActive(false);
                player.GetComponent<Player>().GoOn();
                inf.text = "Введите ответ...";
                timertext.text = "0";
                gameObject.SetActive(false);
                PlayerPrefs.SetInt(myName, -1);
                foreach (GameObject task in dataBase.GetComponent<DataBase>().tasks) if (task.name != gameObject.name && !PlayerPrefs.HasKey(task.name)) task.SetActive(true);
            }
            else
            {
                try //чтобы не было вылета при нечисленном ответе
                {
                    objects[chosen].GetComponent<Car>().WrongAns(float.Parse(inf.text), what);
                    ans_count++;
                }
                catch 
                {
                    inf.text = "Ошибка!";
                }
            }
        }
    }
    
    public void OpenHint()
    {
        hintWindow.SetActive(true);
        hintButton.SetActive(false);
        hint.SetActive(true);
        window.SetActive(false);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public Text text_money;
    public Text text_year;
    public Text text_season;
    public Text text_date;
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject option;
    StringBuilder sb = new StringBuilder();
    StringBuilder sb2 = new StringBuilder();

    public static bool canEscape = true;
    public static bool seasonChanged = false;
    public static string SEASON = "Season"; // s
    public static int totalMoney = 0; // s
    public static float saveDelay = 60.0f;
    public int money = 0;
    public static int year = 1; // s
    public static int date = 28; // s
    public bool tutorialFinish = false; // s
    float times = 0.0f; // s
    float duration = 0.0f;
    float saveTime = 0.0f;
    bool firstTime = true; // s

    void Awake()
    {
        if (instance == null)
            instance = this;

        //Application.targetFrameRate = 60;
        //Qualityoptions.vSyncCount = 1;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

	// Use this for initialization
	void Start ()
    {
        GetData();

        if (tutorialFinish)
            tutorial1.SetActive(false);
        else
            tutorial1.SetActive(true);
        tutorial2.SetActive(false);
        
        if (firstTime)
        {
            addMoney(0);
            switchInfoTexts();
            firstTime = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!tutorialFinish)
        {
            if (Input.touchCount != 0 && times >= 0.2f)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    if (tutorial1.activeInHierarchy)
                    {
                        Debug.Log("1 finish");
                        tutorial1.SetActive(false);
                        tutorial2.SetActive(true);
                    } else
                    {
                        Debug.Log("2 finish");
                        tutorial2.SetActive(false);
                        tutorialFinish = true;
                        times = 0.0f;
                    }
                }
            }

            times += Time.deltaTime;
        }

        else if (tutorialFinish)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (duration >= 0.7f)
                    addMoney(10000);

                duration += Time.deltaTime;
            } else if (Input.GetKeyUp(KeyCode.Escape))
            {
                duration = 0.0f;
            }

            if (times >= 5.0f) // 30.0f
            {
                switchingTime();
                Debug.Log(SEASON + ", " + times + ", " + date);
                times = 0.0f;
            }

            if (saveTime >= GameController.saveDelay)
            {
                SaveData();
                saveTime = 0.0f;
            }

            saveTime += Time.deltaTime;

            times += Time.deltaTime;
        }
	}

    void switchingTime()
    {
        date++;
        if (date >= 30)
        {
            switch (SEASON)
            {
                case "Spring":
                case "Autumn":
                    if (SEASON.Equals("Spring"))
                        SEASON = "Summer";
                    else
                        SEASON = "Winter";

                    seasonChanged = true;
                    date = 28;
                    switchInfoTexts();
                    break;

                case "Summer":
                case "Winter":
                    if (date >= 31)
                    {
                        if (SEASON.Equals("Summer"))
                            SEASON = "Autumn";
                        else
                            SEASON = "Spring";

                        seasonChanged = true;
                        date = 28;
                        switchInfoTexts();
                    }
                    break;
            }
        }

        if (saveTime >= GameController.saveDelay)
        {
            SaveData();
            saveTime = 0.0f;
        }

        saveTime += Time.deltaTime;

        switchInfoTexts();
    }

    void switchInfoTexts()
    {
        sb.Remove(0, sb.Length);
        sb.Append(year);
        sb.Append("년");
        text_year.text = sb.ToString();


        sb.Remove(0, sb.Length);
        switch (SEASON)
        {
            case "Spring":
                sb.Append("봄");
                break;

            case "Summer":
                sb.Append("여름");
                break;

            case "Autumn":
                sb.Append("가을");
                break;

            case "Winter":
                sb.Append("겨울");
                break;
        }
        text_season.text = sb.ToString();

        
        sb.Remove(0, sb.Length);
        sb.Append(date);
        sb.Append("일");
        text_date.text = sb.ToString();
    }

    public void addMoney()
    {
        sb.Remove(0, sb.Length);
        sb2.Remove(0, sb2.Length);
        totalMoney += money;

        int _money = totalMoney;
        int count = 0;

        while (_money > 0)
        {
            if (count != 0 && count % 3 == 0)
                sb.Append(',');

            sb.Append(_money % 10);
            _money /= 10;
            count++;
        }

        for (int i = sb.Length - 1; i >= 0; i--)
            sb2.Append(sb[i]);

        text_money.text = sb2.ToString();
    }

    public void addMoney(int add)
    {
        sb.Remove(0, sb.Length);
        sb2.Remove(0, sb2.Length);
        totalMoney += add;

        int _money = totalMoney;
        int count = 0;

        while (_money > 0)
        {
            if (count != 0 && count % 3 == 0)
                sb.Append(',');

            sb.Append(_money % 10);
            _money /= 10;
            count++;
        }

        for (int i = sb.Length - 1; i >= 0; i--)
            sb2.Append(sb[i]);

        text_money.text = sb2.ToString();
    }

    public void tutorialAgain()
    {
        option.SetActive(false);
        tutorialFinish = false;
        tutorial1.SetActive(true);
        times = 0.0f;
    }

    public void exitGame()
    {
        Application.Quit();
    }

    void GetData()
    {
        SEASON = PlayerPrefs.GetString("Season", "Spring");
        totalMoney = PlayerPrefs.GetInt("TotalMoney", 200000000);
        year = PlayerPrefs.GetInt("Year", 1);
        date = PlayerPrefs.GetInt("Date", 1);
        times = PlayerPrefs.GetFloat("Times", 0.0f);
        tutorialFinish = bool.Parse(PlayerPrefs.GetString("TutorialFinish", "false"));
        firstTime = bool.Parse(PlayerPrefs.GetString("FirstTime", "true"));
    }

    void SaveData()
    {
        PlayerPrefs.GetString("Season", SEASON);
        PlayerPrefs.GetInt("TotalMoney", totalMoney);
        PlayerPrefs.GetInt("Year", year);
        PlayerPrefs.GetInt("Date", date);
        PlayerPrefs.GetFloat("Times", times);
        bool.Parse(PlayerPrefs.GetString("TutorialFinish", tutorialFinish.ToString()));
        bool.Parse(PlayerPrefs.GetString("FirstTime", firstTime.ToString()));
    }
}

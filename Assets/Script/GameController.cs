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
    public static string SEASON = "Season";
    public static int totalMoney = 0;
    public int money = 0;
    public static int date = 28;
    int year = 1;
    float times = 0.0f;
    float duration = 0.0f;
    bool tutorialFinish = false;

    void Awake()
    {
        if (instance == null)
            instance = this;

        //Application.targetFrameRate = 60;
        //Qualityoptions.vSyncCount = 1;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

	// Use this for initialization
	void Start () {
        if (tutorialFinish)
            tutorial1.SetActive(false);
        else
            tutorial1.SetActive(true);
        tutorial2.SetActive(false);

        // 저장된 값이 없다면!
        addMoney(20000);

        SEASON = "Spring";
        switchInfoTexts();

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
                switchingTIme();
                Debug.Log(SEASON + ", " + times + ", " + date);
                times = 0.0f;
            }

            times += Time.deltaTime;
        }
	}

    void switchingTIme()
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
}

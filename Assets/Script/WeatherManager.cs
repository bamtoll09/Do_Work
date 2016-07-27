using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;

public class WeatherManager : MonoBehaviour {

    public List<Image> image_wts;
    public List<Sprite> weatherSpritesForGame;
    public List<Sprite> weatherSpritesForInfo;
    public List<Text> text_changePrice;
    public List<GameObject> arrows;
    StringBuilder sb = new StringBuilder();
    Vector3 reverse = new Vector3(1.0f, -1.0f, 1.0f);

    public static string WEATHER = "Sunny"; // Sunny, Rainy, Snowy, Cloudy

    float[][] weatherChangePercentageBySeason = { // 1~100기준
        new float[]{ 40.5f, 55.5f, 100.0f, 0.0f }, // 봄
        new float[]{ 30.0f, 60.0f, 100.0f, 0.0f }, // 여름
        new float[]{ 36.5f, 54.5f, 100.0f, 0.0f }, // 가을
        new float[]{ 42.5f, 59.5f, 0.0f, 100.0f } // 겨울
    }; //               해    구름     비    눈

    int[] weatherPerDate = { 0, 0, 0, 0, 0, 0, 0 }; // s
    // 1 -> 해, 2 -> 구름, 3 -> 비, 4 -> 눈

    float calc = 0.0f;
    int checkDay = 0;
    int index = 0;
    float saveTime = 0.0f;


    // Use this for initialization
    void Start () {
        GetData();
	}
	
	// Update is called once per frame
	void Update () {
	    /*
        switch (GameController.SEASON)
        {
            case "Spring":
                break;

            case "Summer":
                break;

            case "Autumn":
                break;

            case "Winter":
                break;
        }
        */

        if (checkDay != GameController.date)
        {
            WeatherForcast();
            아몰랑_날씨변화();
            switch (weatherPerDate[0])
            {
                case 1:
                    WEATHER = "Sunny";
                    for (int i=0; i<4; i++)
                    {
                        FruitManager.instance.currentPrice[i] += FruitManager.instance.changePrice[i];
                        FruitManager.instance.currentPrice[i] = (int) Mathf.Round(FruitManager.instance.currentPrice[i] * 0.5f);
                        if (FruitManager.instance.currentPrice[i] < FruitManager.instance.minPrice[i])
                            FruitManager.instance.currentPrice[i] = FruitManager.instance.minPrice[i];
                        else if (FruitManager.instance.currentPrice[i] > FruitManager.instance.maxPrice[i])
                            FruitManager.instance.currentPrice[i] = FruitManager.instance.maxPrice[i];
                        arrows[i].transform.localScale = reverse;
                    }
                    break;

                case 2:
                    WEATHER = "Cloudy";
                    for (int i = 0; i < 4; i++)
                    {
                        FruitManager.instance.currentPrice[i] += FruitManager.instance.changePrice[i];
                        FruitManager.instance.currentPrice[i] = (int)Mathf.Round(FruitManager.instance.currentPrice[i] * 1.5f);
                        if (FruitManager.instance.currentPrice[i] < FruitManager.instance.minPrice[i])
                            FruitManager.instance.currentPrice[i] = FruitManager.instance.minPrice[i];
                        else if (FruitManager.instance.currentPrice[i] > FruitManager.instance.maxPrice[i])
                            FruitManager.instance.currentPrice[i] = FruitManager.instance.maxPrice[i];
                        arrows[i].transform.localScale = Vector3.one;
                    }
                    break;

                case 3:
                    WEATHER = "Rainy";
                    for (int i = 0; i < 4; i++)
                    {
                        FruitManager.instance.currentPrice[i] += FruitManager.instance.changePrice[i];
                        FruitManager.instance.currentPrice[i] = (int)Mathf.Round(FruitManager.instance.currentPrice[i] * 2.0f);
                        if (FruitManager.instance.currentPrice[i] < FruitManager.instance.minPrice[i])
                            FruitManager.instance.currentPrice[i] = FruitManager.instance.minPrice[i];
                        else if (FruitManager.instance.currentPrice[i] > FruitManager.instance.maxPrice[i])
                            FruitManager.instance.currentPrice[i] = FruitManager.instance.maxPrice[i];
                        arrows[i].transform.localScale = Vector3.one;
                    }
                    break;

                case 4:
                    WEATHER = "Snowy";
                    for (int i = 0; i < 4; i++)
                    {
                        FruitManager.instance.currentPrice[i] += FruitManager.instance.changePrice[i];
                        FruitManager.instance.currentPrice[i] = (int)Mathf.Round(FruitManager.instance.currentPrice[i] * 0.5f);
                        if (FruitManager.instance.currentPrice[i] < FruitManager.instance.minPrice[i])
                            FruitManager.instance.currentPrice[i] = FruitManager.instance.minPrice[i];
                        else if (FruitManager.instance.currentPrice[i] > FruitManager.instance.maxPrice[i])
                            FruitManager.instance.currentPrice[i] = FruitManager.instance.maxPrice[i];
                        arrows[i].transform.localScale = Vector3.one;
                    }
                    break;
            }
            setText();
            checkDay = GameController.date;

            Debug.Log("Current Price: " + FruitManager.instance.currentPrice[0] + ", " + FruitManager.instance.currentPrice[1] + ", " + FruitManager.instance.currentPrice[2] + ", " + FruitManager.instance.currentPrice[3]);

        }

        if (saveTime >= GameController.saveDelay)
        {
            SaveData();
            saveTime = 0.0f;
        }

        saveTime += Time.deltaTime;
    }

    void GetData()
    {
        WEATHER = PlayerPrefs.GetString("Weather", "Sunny");

        string[] _dataArr = PlayerPrefs.GetString("WeatherPerDate", "0,0,0,0,0,0,0").Split(',');
        int[] _dataIn = new int[_dataArr.Length];
        for (int i = 0; i < _dataArr.Length; i++)
        {
            _dataIn[i] = int.Parse(_dataArr[i]);
        }

        weatherPerDate = _dataIn;
    }

    void SaveData()
    {
        PlayerPrefs.SetString("Weather", WEATHER);

        string _tmpStr = "";

        for (int i = 0; i < weatherPerDate.Length; i++)
        {
            _tmpStr = _tmpStr + weatherPerDate[i];
            if (i < weatherPerDate.Length)
            {
                _tmpStr = _tmpStr + ",";
            }
        }

        PlayerPrefs.SetString("Data", _tmpStr);
    }

    void setText()
    {
        for (int i=0; i<text_changePrice.Count; i++)
        {
            sb.Remove(0, sb.Length);
            sb.Append(FruitManager.instance.currentPrice[i]);
            text_changePrice[i].text = sb.ToString();
        }
        
    }

    public void 아몰랑_날씨변화()
    {
        calc = Random.Range(0.0f, 100.0f);

        if (calc > Upgrade.날씨퍼센트)
        {
            Debug.Log("쨔잔~ 날씨가 바꼈습니다!!");
            switch (GameController.SEASON)
            {
                case "Spring":
                    index = 0;
                    break;

                case "Summer":
                    index = 1;
                    break;

                case "Autumn":
                    index = 2;
                    break;

                case "Winter":
                    index = 3;
                    break;
            }

            calc = Random.Range(0.0f, 100.0f);
            for (int i = 0; i < weatherChangePercentageBySeason[0].Length; i++)
            {
                if (weatherChangePercentageBySeason[index][i] != 0.0f)
                {
                    if (calc <= weatherChangePercentageBySeason[index][i])
                    {
                        weatherPerDate[0] = i + 1;
                        break;
                    }
                }
            }

            image_wts[0].sprite = weatherSpritesForGame[weatherPerDate[0] - 1];
            image_wts[1].sprite = weatherSpritesForGame[weatherPerDate[0] - 1];
        }
    }

    public void WeatherForcast()
    {
        switch (GameController.SEASON)
        {
            case "Spring":
                index = 0;
                break;

            case "Summer":
                index = 1;
                break;

            case "Autumn":
                index = 2;
                break;

            case "Winter":
                index = 3;
                break;
        }

        if (weatherPerDate[0] == 0)
        {
            for (int i=0; i<weatherPerDate.Length; i++)
            {
                calc = Random.Range(0.0f, 100.0f);
                for (int j=0; j<weatherChangePercentageBySeason[0].Length; j++)
                {
                    if (weatherChangePercentageBySeason[index][j] != 0.0f)
                    {
                        if (calc <= weatherChangePercentageBySeason[index][j])
                        {
                            weatherPerDate[i] = j + 1;
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < weatherPerDate.Length - 1; i++)
            {
                weatherPerDate[i] = weatherPerDate[i + 1];
            }

            calc = Random.Range(0.0f, 100.0f);
            for (int i = 0; i < weatherChangePercentageBySeason[0].Length; i++)
            {
                if (weatherChangePercentageBySeason[index][i] != 0.0f)
                {
                    if (calc <= weatherChangePercentageBySeason[index][i])
                    {
                        weatherPerDate[6] = i + 1;
                        break;
                    }
                }
            }
        }

        image_wts[0].sprite = weatherSpritesForGame[weatherPerDate[0]-1];
        for (int i=1; i<image_wts.Count; i++)
        {
            Debug.Log("weatherPerDate: " + weatherPerDate[i - 1]);
            image_wts[i].sprite = weatherSpritesForInfo[weatherPerDate[i-1]-1];
        }

        //Debug.Log(weatherPerDate[0] + ", " + weatherPerDate[1] + ", " + weatherPerDate[2] + ", " + weatherPerDate[3] + ", " + weatherPerDate[4] + ", " + weatherPerDate[5] + ", " + weatherPerDate[6]);
    }
}

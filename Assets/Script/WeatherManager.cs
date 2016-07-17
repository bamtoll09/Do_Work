using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WeatherManager : MonoBehaviour {

    public List<Image> image_wts;
    public List<Sprite> weatherSpritesForGame;
    public List<Sprite> weatherSpritesForInfo;

    public static string WEATHER = "Sunny"; // Sunny, Rainy, Snowy, Cloudy

    float[][] weatherChangePercentageBySeason = { // 1~100기준
        new float[]{ 40.5f, 55.5f, 100.0f, 0.0f }, // 봄
        new float[]{ 30.0f, 60.0f, 100.0f, 0.0f }, // 여름
        new float[]{ 36.5f, 54.5f, 100.0f, 0.0f }, // 가을
        new float[]{ 42.5f, 59.5f, 0.0f, 100.0f } // 겨울
    }; //               해    구름     비    눈

    int[] weatherPerDate = { 0, 0, 0, 0, 0, 0, 0 };
    // 1 -> 해, 2 -> 구름, 3 -> 비, 4 -> 눈

    float calc = 0.0f;
    int checkDay = 0;
    int index = 0;


    // Use this for initialization
    void Start () {
	
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
            checkDay = GameController.date;
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
            image_wts[i].sprite = weatherSpritesForInfo[weatherPerDate[i-1]-1];
        }

        //Debug.Log(weatherPerDate[0] + ", " + weatherPerDate[1] + ", " + weatherPerDate[2] + ", " + weatherPerDate[3] + ", " + weatherPerDate[4] + ", " + weatherPerDate[5] + ", " + weatherPerDate[6]);
    }
}

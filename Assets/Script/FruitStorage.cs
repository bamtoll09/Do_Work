using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FruitStorage : MonoBehaviour {

    public static FruitStorage instance;

    public GameObject spring;
    public GameObject summer;
    public GameObject autumn;
    public GameObject winter;

    public Button[] button_spring;
    public Button[] button_summer;
    public Button[] button_autumn;
    public Button[] button_winter;

    ColorBlock cb = new ColorBlock();
    ColorBlock _cb = new ColorBlock();

    public int[] springFruits = { 0, 0, 0, 0 };
    public int[] summerFruits = { 0, 0, 0, 0 };
    public int[] autumnFruits = { 0, 0, 0, 0 };
    public int[] winterFruits = { 0, 0, 0, 0 };

    bool[] fulled = { false, false, false, false };
    string checkSeason = "Season";
    int discountPrice = 0;
    int index = 0;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        cb = button_spring[0].colors;
        _cb = cb;
        _cb.pressedColor = Color.red;
	}

    // Update is called once per frame
    void Update()
    {
        if (!checkSeason.Equals(GameController.SEASON))
        {
            checkSeason = GameController.SEASON;

            switch (checkSeason)
            {
                case "Spring":
                    winter.SetActive(false);
                    spring.SetActive(true);
                    break;

                case "Summer":
                    spring.SetActive(false);
                    summer.SetActive(true);
                    break;

                case "Autumn":
                    summer.SetActive(false);
                    autumn.SetActive(true);
                    break;

                case "Winter":
                    autumn.SetActive(false);
                    winter.SetActive(true);
                    break;
            }
        }

        if (isTrue())
        {

            switch (checkSeason)
            {
                case "Spring":
                    _cb = cb;
                    _cb.pressedColor = Color.red;
                    button_spring[index].colors = _cb;
                    fulled[index] = false;
                    break;

                case "Summer":
                    _cb = cb;
                    _cb.pressedColor = Color.red;
                    button_spring[index].colors = _cb;
                    fulled[index] = false;
                    break;

                case "Autumn":
                    _cb = cb;
                    _cb.pressedColor = Color.red;
                    button_spring[index].colors = _cb;
                    fulled[index] = false;
                    break;

                case "Winter":
                    _cb = cb;
                    _cb.pressedColor = Color.red;
                    button_spring[index].colors = _cb;
                    fulled[index] = false;
                    break;
            }
        }

        for (int i = 0; i < fulled.Length; i++)
        {
            switch (GameController.SEASON)
            {
                case "Spring":
                    if (springFruits[i] == Upgrade.창최몇)
                        button_spring[i].colors = _cb;
                    else if (springFruits[i] < Upgrade.창최몇 && !button_spring[i].colors.Equals(cb))
                        button_spring[i].colors = cb;
                    break;

                case "Summer":
                    if (summerFruits[i] == Upgrade.창최몇)
                        button_summer[i].colors = _cb;
                    else if (summerFruits[i] < Upgrade.창최몇 && !button_summer[i].colors.Equals(cb))
                        button_summer[i].colors = cb;
                    break;

                case "Autumn":
                    if (autumnFruits[i] == Upgrade.창최몇)
                        button_autumn[i].colors = _cb;
                    else if (autumnFruits[i] < Upgrade.창최몇 && !button_autumn[i].colors.Equals(cb))
                        button_autumn[i].colors = cb;
                    break;

                case "Winter":
                    if (winterFruits[i] == Upgrade.창최몇)
                        button_winter[i].colors = _cb;
                    else if (winterFruits[i] < Upgrade.창최몇 && !button_winter[i].colors.Equals(cb))
                        button_winter[i].colors = cb;
                    break;
            }
        }
    }

    public bool isEmpty()
    {
        bool empty = true;

        for (int i = 0; i < fulled.Length; i++)
        {
            switch (GameController.SEASON)
            {
                case "Spring":
                    if (springFruits[i] != 0)
                        empty = false;
                    break;

                case "Summer":
                    if (summerFruits[i] != 0)
                        empty = false;
                    break;

                case "Autumn":
                    if (autumnFruits[i] != 0)
                        empty = false;
                    break;

                case "Winter":
                    if (winterFruits[i] != 0)
                        empty = false;
                    break;
            }
        }

        return empty;
    }

    public bool isEmpty(int index)
    {
        bool empty = true;
        
        switch (GameController.SEASON)
        {
            case "Spring":
                if (springFruits[index] != 0)
                    empty = false;
                break;

            case "Summer":
                if (summerFruits[index] != 0)
                    empty = false;
                break;

            case "Autumn":
                if (autumnFruits[index] != 0)
                    empty = false;
                break;

            case "Winter":
                if (winterFruits[index] != 0)
                    empty = false;
                break;
        }

        return empty;
    }

    bool isTrue() // 하나라도 ture면
    {
        for (int i=0; i<fulled.Length; i++)
        {
            if (fulled[i])
            {
                index = i;
                return true;
            }
        }

        return false;
    }

    public void addPlum()
    {
        if (springFruits[0] < Upgrade.창최몇)
        {
            springFruits[0]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[0] - Upgrade.싸게싸게 / 5));
        }
    }

    public void addBlueberry()
    {
        if (springFruits[1] < Upgrade.창최몇)
        {
            springFruits[1]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[1] - Upgrade.싸게싸게 / 5));
        }
    }

    public void addStrawberry()
    {
        if (springFruits[2] < Upgrade.창최몇)
        {
            springFruits[2]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[2] - Upgrade.싸게싸게 / 5));
        }
    }

    public void addPeach()
    {
        if (springFruits[3] < Upgrade.창최몇)
        {
            springFruits[3]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[3] - Upgrade.싸게싸게 / 5));
        }
    }

    public void addChamwe()
    {
        if (summerFruits[0] < Upgrade.창최몇)
        {
            summerFruits[0]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[0] - Upgrade.싸게싸게 / 5));
        }
    }

    public void addGrape()
    {
        if (summerFruits[1] < Upgrade.창최몇)
        {
            summerFruits[1]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[1] - Upgrade.싸게싸게 / 5));
        }
    }

    public void addTomato()
    {
        if (summerFruits[2] < Upgrade.창최몇)
        {
            summerFruits[2]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[2] - Upgrade.싸게싸게 / 5));
        }
    }

    public void addWatermelon()
    {
        if (summerFruits[3] < Upgrade.창최몇)
        {
            summerFruits[3]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[3] - Upgrade.싸게싸게 / 5));
        }
    }

    public void addFeeling()
    {
        if (autumnFruits[0] < Upgrade.창최몇)
        {
            autumnFruits[0]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[0] - Upgrade.싸게싸게 / 5));
        }
    }

    public void addPear()
    {
        if (autumnFruits[1] < Upgrade.창최몇)
        {
            autumnFruits[1]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[1] - Upgrade.싸게싸게 / 5));
        }
    }

    public void addApple()
    {
        if (autumnFruits[2] < Upgrade.창최몇)
        {
            autumnFruits[2]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[2] - Upgrade.싸게싸게 / 5));
        }
    }

    public void addKiwi()
    {
        if (autumnFruits[3] < Upgrade.창최몇)
        {
            autumnFruits[3]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[3] - Upgrade.싸게싸게 / 5));
        }
    }

    public void addMikang()
    {
        if (winterFruits[0] < Upgrade.창최몇)
        {
            winterFruits[0]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[0] - Upgrade.싸게싸게 / 5));
        }
    }

    public void addMelon()
    {
        if (winterFruits[1] < Upgrade.창최몇)
        {
            winterFruits[1]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[1] - Upgrade.싸게싸게 / 5));
        }
    }

    public void addSuckrew()
    {
        if (winterFruits[2] < Upgrade.창최몇)
        {
            winterFruits[2]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[2] - Upgrade.싸게싸게 / 5));
        }
    }

    public void add1Rabong()
    {
        if (winterFruits[3] < Upgrade.창최몇)
        {
            winterFruits[3]++;
            GameController.instance.addMoney(-1 * (FruitManager.instance.defaultPrice[3] - Upgrade.싸게싸게 / 5));
        }
    }
}
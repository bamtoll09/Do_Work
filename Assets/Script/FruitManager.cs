using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FruitManager : MonoBehaviour {

    public static FruitManager instance;
    public List<GameObject> springFruits;
    public List<GameObject> summerFruits;
    public List<GameObject> autumnFruits;
    public List<GameObject> winterFruits;
    public List<GameObject> dishes; // y +0.89
    public List<GameObject> fruits = new List<GameObject>(8);
    public GameObject mouse; // x -1.6, y -0.5
    public Collider2D[] dishCollider = new Collider2D[6];
    Vector3 reverse = new Vector3(1, -1, 1);

    public int[] defaultPrice = { 3, 5, 7, 12 }; // 과일 기본 금액
    public int[] currentPrice = { 0, 0, 0, 0 }; // 과일 현재 금액
    public int[] minPrice = { 1, 2, 2, 7 }; // 과일 최소 금액
    public int[] maxPrice = {5, 8, 12, 17}; // 과일 최대 금액
    public int[] changePrice = { 2, 3, 5, 5 }; // 과일 변동 금액
    public int[] fruitsInfo = new int[8];
    public int sellTimeMin = 1; // 최소 5
    public int sellTimeMax = 5;
    int fruitIndex = 0;
    int dishIndex = 0;
    float times = 0.0f;
    float saveTime = 0.0f;
    float saveDelay = 60.0f;

    void Awake()
    {
        instance = this;

        if (currentPrice[0] == 0)
            currentPrice = defaultPrice;
    }

    // Use this for initialization
    void Start () {
        for (int i = 0; i < dishes.Count; i++)
            dishes[i].transform.localScale = reverse;

        //for (int i = 0; i < 8; i++)
        //{
        //    springFruits[i].SetActive(false);
        //    summerFruits[i].SetActive(false);
        //    autumnFruits[i].SetActive(false);
        //    winterFruits[i].SetActive(false);
        //}
        //autumnFruits[8].SetActive(false);
        mouse.SetActive(false);
        changeSeason();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameController.seasonChanged)
        {
            changeSeason();
            GameController.seasonChanged = false;
        }

        if (Upgrade.햄스터On && times >= Upgrade.햄스터Delay && isEmpty())
        {
            int index = 0;

            do
                index = Random.Range(0, dishes.Count);
            while (dishes[index].transform.localScale.Equals(Vector3.one));

            mouse.SetActive(true);
            mouse.transform.position = dishes[index].transform.position + new Vector3(-1.6f, -0.5f, -1.0f);
            switch (GameController.SEASON)
            {
                case "Spring":
                    Spawn_SpringFruits(index);
                    break;

                case "Summer":
                    Spawn_SummerFruits(index);
                    break;

                case "Autumn":
                    Spawn_AutumnFruits(index);
                    break;

                case "Winter":
                    Spawn_WinterFruits(index);
                    break;
            }
            SoundManager.instance.playHamster();

            times = 0.0f;
        }


        if (Input.touchCount != 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                for (int i = 0; i < dishes.Count; i++)
                {
                    if (dishCollider[i] == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)))
                    {
                        if (dishes[i].transform.localScale.Equals(reverse) && isEmpty())
                        {
                            switch (GameController.SEASON)
                            {
                                case "Spring":
                                    Spawn_SpringFruits(i);
                                    break;

                                case "Summer":
                                    Spawn_SummerFruits(i);
                                    break;

                                case "Autumn":
                                    Spawn_AutumnFruits(i);
                                    break;

                                case "Winter":
                                    Spawn_WinterFruits(i);
                                    break;
                            }

                            SoundManager.instance.playDish();
                        }
                    }
                }
                //Instantiate(springFruits[Random.Range(0, 4)], Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Quaternion.identity);
            }
        }

        times += Time.deltaTime;
    }

    IEnumerator Sell_SpringFruits()
    {
        while (true)
        {
            if (!isAllEmpty())
            {
                int index = 0;
                do
                    index = Random.Range(0, dishes.Count);
                while (dishes[index].transform.localScale.Equals(reverse));

                Destroy(fruits[index]);
                dishes[index].transform.localScale = reverse;

                GameController.instance.addMoney(FruitManager.instance.currentPrice[fruitsInfo[index]]);

            }
            yield return new WaitForSeconds(Random.Range(sellTimeMin, sellTimeMax));
        }
    }

    IEnumerator Sell_SummerFruits()
    {
        while (true)
        {
            if (!isAllEmpty())
            {
                int index = 0;
                do
                    index = Random.Range(0, dishes.Count);
                while (dishes[index].transform.localScale.Equals(reverse));

                Destroy(fruits[index]);
                dishes[index].transform.localScale = reverse;

                GameController.instance.addMoney(FruitManager.instance.currentPrice[fruitsInfo[index]]);

            }
            yield return new WaitForSeconds(Random.Range(sellTimeMin, sellTimeMax));
        }
    }

    IEnumerator Sell_AutumnFruits()
    {
        while (true)
        {
            if (!isAllEmpty())
            {
                int index = 0;
                do
                    index = Random.Range(0, dishes.Count);
                while (dishes[index].transform.localScale.Equals(reverse));

                Destroy(fruits[index]);
                dishes[index].transform.localScale = reverse;

                GameController.instance.addMoney(FruitManager.instance.currentPrice[fruitsInfo[index]]);
            }
            yield return new WaitForSeconds(Random.Range(sellTimeMin, sellTimeMax));
        }
    }

    IEnumerator Sell_WinterFruits()
    {
        while (true)
        {
            if (!isAllEmpty())
            {
                int index = 0;
                do
                    index = Random.Range(0, dishes.Count);
                while (dishes[index].transform.localScale.Equals(reverse));

                Destroy(fruits[index]);
                dishes[index].transform.localScale = reverse;

                GameController.instance.addMoney(FruitManager.instance.currentPrice[fruitsInfo[index]]);

            }
            yield return new WaitForSeconds(Random.Range(sellTimeMin, sellTimeMax));
        }
    }

    void Spawn_SpringFruits(int dishIndex)
    {
        if (!FruitStorage.instance.isEmpty())
        {
            while (true)
            {
                fruitIndex = Random.Range(0, 4);
                if (FruitStorage.instance.springFruits[fruitIndex] != 0)
                    if (fruits[dishIndex] == null)
                        break;
            }

            FruitStorage.instance.springFruits[fruitIndex]--;

            fruits[dishIndex] = Instantiate(springFruits[fruitIndex]);

            if (fruits[dishIndex].name.Contains("blueberry"))
                fruits[dishIndex].transform.position = dishes[dishIndex].transform.position + new Vector3(0.0f, 1.15f, 1.0f);
            else
                fruits[dishIndex].transform.position = dishes[dishIndex].transform.position + new Vector3(0.0f, 0.89f, 1.0f);

            dishes[dishIndex].transform.localScale = Vector3.one;

            fruitsInfo[dishIndex] = fruitIndex;
        }
    }

    void Spawn_SummerFruits(int dishIndex)
    {
        if (!FruitStorage.instance.isEmpty())
        {
            while (true)
            {
                fruitIndex = Random.Range(0, 4);
                if (FruitStorage.instance.summerFruits[fruitIndex] != 0)
                    if (fruits[dishIndex] == null)
                        break;
            }
            
            FruitStorage.instance.summerFruits[fruitIndex]--;

            fruits[dishIndex] = Instantiate(summerFruits[fruitIndex]);
            fruits[dishIndex].transform.position = dishes[dishIndex].transform.position + new Vector3(0.0f, 0.89f, 1.0f);

            dishes[dishIndex].transform.localScale = Vector3.one;

            fruitsInfo[dishIndex] = fruitIndex;
        }
    }

    void Spawn_AutumnFruits(int dishIndex)
    {
        if (!FruitStorage.instance.isEmpty())
        {
            while (true)
            {
                fruitIndex = Random.Range(0, 4);
                if (FruitStorage.instance.autumnFruits[fruitIndex] != 0)
                    if (fruits[dishIndex] == null)
                        break;
            }

            FruitStorage.instance.autumnFruits[fruitIndex]--;

            fruits[dishIndex] = Instantiate(autumnFruits[fruitIndex]);
            fruits[dishIndex].transform.position = dishes[dishIndex].transform.position + new Vector3(0.0f, 0.63f, 1.0f);

            dishes[dishIndex].transform.localScale = Vector3.one;

            fruitsInfo[dishIndex] = fruitIndex;
        }
    }

    void Spawn_WinterFruits(int dishIndex)
    {
        if (!FruitStorage.instance.isEmpty())
        {
            while (true)
            {
                fruitIndex = Random.Range(0, 4);
                if (FruitStorage.instance.winterFruits[fruitIndex] != 0)
                    if (fruits[fruitIndex] != null)
                        break;
            }

            FruitStorage.instance.winterFruits[fruitIndex]--;

            fruits[dishIndex] = Instantiate(winterFruits[fruitIndex]);
            fruits[dishIndex].transform.position = dishes[dishIndex].transform.position + new Vector3(0.0f, 0.63f, 1.0f);

            dishes[dishIndex].transform.localScale = Vector3.one;

            fruitsInfo[dishIndex] = fruitIndex;
        }
    }

    bool isEmpty()
    {
        bool empty = false;
        for (int i=0; i<dishes.Count; i++)
        {
            if (dishes[i].transform.localScale.Equals(reverse))
            {
                empty = true;
                break;
            }
        }

        return empty;
    }

    bool isAllEmpty()
    {
        bool allEmpty = true;
        for (int i = 0; i < dishes.Count; i++)
        {
            if (dishes[i].transform.localScale.Equals(Vector3.one))
            {
                allEmpty = false;
                break;
            }
        }

        return allEmpty;
    }

    void changeSeason()
    {
        StopAllCoroutines();

        switch (GameController.SEASON)
        {
            case "Spring":
                for (int i = 0; i < 8; i++)
                {
                    if (dishes[i].transform.localScale == Vector3.one)
                        dishes[i].transform.localScale = reverse;

                    if (fruits[i] != null)
                    {
                        Destroy(fruits[i]);
                        GameController.instance.addMoney((int)Mathf.Round(FruitManager.instance.currentPrice[fruitsInfo[i]] * 0.3f));
                    }
                }
                StartCoroutine(Sell_SpringFruits());
                break;

            case "Summer":
                for (int i = 0; i < 8; i++)
                {
                    if (dishes[i].transform.localScale == Vector3.one)
                        dishes[i].transform.localScale = reverse;

                    if (fruits[i] != null)
                    {
                        Destroy(fruits[i]);
                        GameController.instance.addMoney((int)Mathf.Round(FruitManager.instance.currentPrice[fruitsInfo[i]] * 0.3f));
                    }
                }
                StartCoroutine(Sell_SummerFruits());
                break;

            case "Autumn":
                for (int i = 0; i < 8; i++)
                {
                    if (dishes[i].transform.localScale == Vector3.one)
                        dishes[i].transform.localScale = reverse;

                    if (fruits[i] != null)
                    {
                        Destroy(fruits[i]);
                        GameController.instance.addMoney((int)Mathf.Round(FruitManager.instance.currentPrice[fruitsInfo[i]] * 0.3f));
                    }
                }
                StartCoroutine(Sell_AutumnFruits());
                break;

            case "Winter":
                for (int i = 0; i < 8; i++)
                {
                    if (dishes[i].transform.localScale == Vector3.one)
                        dishes[i].transform.localScale = reverse;

                    if (fruits[i] != null)
                    {
                        Destroy(fruits[i]);
                        GameController.instance.addMoney((int)Mathf.Round(FruitManager.instance.currentPrice[fruitsInfo[i]] * 0.3f));
                    }
                }
                StartCoroutine(Sell_WinterFruits());
                break;

            default:
                StartCoroutine(Sell_SpringFruits());
                break;
        }
    }

}

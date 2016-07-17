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
    public GameObject mouse; // x -1.6, y -0.5
    public Collider2D[] dishCollider = new Collider2D[6];
    Vector3 reverse = new Vector3(1, -1, 1);

    public int[] defaultPrice = { 3, 5, 7, 12 };
    public int[] minPrice = { 1, 2, 2, 7 };
    public int[] maxPrice = {5, 8, 12, 17};
    public int sellTimeMin = 1; // 최소 5
    public int sellTimeMax = 5;
    int[] fruitPos = new int[8];
    int fruitIndex = 0;
    int dishIndex = 0;
    float times = 0.0f;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        for (int i = 0; i < dishes.Count; i++)
            dishes[i].transform.localScale = reverse;

        for (int i = 0; i < 8; i++)
        {
            springFruits[i].SetActive(false);
            summerFruits[i].SetActive(false);
            autumnFruits[i].SetActive(false);
            winterFruits[i].SetActive(false);
        }
        autumnFruits[8].SetActive(false);
        mouse.SetActive(false);
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
            Instantiate(springFruits[0]);
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
            dishes[index].transform.localScale = Vector3.one;

            times = 0.0f;
        }


        if (Input.touchCount != 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                //for (int i = 0; i < dishes.Count; i++)
                //{
                //    if (dishCollider[i] == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)))
                //    {
                //        if (dishes[i].transform.localScale.Equals(reverse) && isEmpty())
                //        {
                //            switch (GameController.SEASON)
                //            {
                //                case "Spring":
                //                    Spawn_SpringFruits(i);
                //                    break;

                //                case "Summer":
                //                    Spawn_SummerFruits(i);
                //                    break;

                //                case "Autumn":
                //                    Spawn_AutumnFruits(i);
                //                    break;

                //                case "Winter":
                //                    Spawn_WinterFruits(i);
                //                    break;
                //            }
                //        }
                //    }
                //}
                Instantiate(springFruits[Random.Range(0, 4)], Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Quaternion.identity);
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
                    index = Random.Range(0, fruitPos.Length);
                while (dishes[index].transform.localScale.Equals(reverse));

                springFruits[fruitPos[index]].SetActive(false);
                dishes[index].transform.localScale = reverse;

                GameController.instance.addMoney();

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
                    index = Random.Range(0, fruitPos.Length);
                while (dishes[index].transform.localScale.Equals(reverse));

                summerFruits[fruitPos[index]].SetActive(false);
                dishes[index].transform.localScale = reverse;

                GameController.instance.addMoney();

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
                    index = Random.Range(0, fruitPos.Length);
                while (dishes[index].transform.localScale.Equals(reverse));

                autumnFruits[fruitPos[index]].SetActive(false);
                dishes[index].transform.localScale = reverse;

                GameController.instance.addMoney();
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
                    index = Random.Range(0, fruitPos.Length);
                while (dishes[index].transform.localScale.Equals(reverse));

                winterFruits[fruitPos[index]].SetActive(false);
                dishes[index].transform.localScale = reverse;

                GameController.instance.addMoney();

            }
            yield return new WaitForSeconds(Random.Range(sellTimeMin, sellTimeMax));
        }
    }

    void Spawn_SpringFruits(int dishIndex)
    {
        if (!FruitStorage.instance.isEmpty())
        {
            int index = 0;

            while (true)
            {
                fruitIndex = Random.Range(0, 8);
                index = fruitIndex;
                if (index > 3)
                    index -= 4;
                if (FruitStorage.instance.springFruits[index] != 0)
                    if (!springFruits[fruitIndex].activeInHierarchy)
                        break;
            }

            FruitStorage.instance.springFruits[index]--;

            springFruits[fruitIndex].SetActive(true);

            if (springFruits[fruitIndex].name.Contains("blueberry"))
                springFruits[fruitIndex].transform.position = dishes[dishIndex].transform.position + new Vector3(0.0f, 1.15f, 1.0f);
            else
                springFruits[fruitIndex].transform.position = dishes[dishIndex].transform.position + new Vector3(0.0f, 0.89f, 1.0f);
            dishes[dishIndex].transform.localScale = Vector3.one;

            fruitPos[dishIndex] = fruitIndex;

            dishes[dishIndex].transform.localScale = Vector3.one;
        }
    }

    void Spawn_SummerFruits(int dishIndex)
    {
        if (!FruitStorage.instance.isEmpty())
        {
            int index = 0;

            while (true)
            {
                fruitIndex = Random.Range(0, 8);
                index = fruitIndex;
                if (index > 3)
                    index -= 4;
                if (FruitStorage.instance.summerFruits[index] != 0)
                    if (!summerFruits[fruitIndex].activeInHierarchy)
                        break;
            }
            
            FruitStorage.instance.summerFruits[index]--;

            summerFruits[fruitIndex].SetActive(true);

            summerFruits[fruitIndex].transform.position = dishes[dishIndex].transform.position + new Vector3(0.0f, 0.89f, 1.0f);
            dishes[dishIndex].transform.localScale = Vector3.one;

            fruitPos[dishIndex] = fruitIndex;

            dishes[dishIndex].transform.localScale = Vector3.one;
        }
    }

    void Spawn_AutumnFruits(int dishIndex)
    {
        if (!FruitStorage.instance.isEmpty())
        {
            int index = 0;

            while (true)
            {
                fruitIndex = Random.Range(0, 9);
                index = fruitIndex;
                if (index == 9)
                    index = 3;
                else if (index > 3)
                    index -= 4;
                if (FruitStorage.instance.autumnFruits[index] != 0)
                    if (!autumnFruits[fruitIndex].activeInHierarchy)
                        break;
            }

            FruitStorage.instance.autumnFruits[index]--;

            autumnFruits[fruitIndex].SetActive(true);
            autumnFruits[fruitIndex].transform.position = dishes[dishIndex].transform.position + new Vector3(0.0f, 0.63f, 1.0f);
            dishes[dishIndex].transform.localScale = Vector3.one;

            fruitPos[dishIndex] = fruitIndex;

            dishes[dishIndex].transform.localScale = Vector3.one;
        }
    }

    void Spawn_WinterFruits(int dishIndex)
    {
        if (!FruitStorage.instance.isEmpty())
        {
            int index = 0;

            while (true)
            {
                fruitIndex = Random.Range(0, 8);
                index = fruitIndex;
                if (index > 3)
                    index -= 4;
                if (FruitStorage.instance.winterFruits[index] != 0)
                    if (!winterFruits[fruitIndex].activeInHierarchy)
                        break;
            }

            FruitStorage.instance.winterFruits[index]--;

            winterFruits[fruitIndex].SetActive(true);
            winterFruits[fruitIndex].transform.position = dishes[dishIndex].transform.position + new Vector3(0.0f, 0.63f, 1.0f);
            dishes[dishIndex].transform.localScale = Vector3.one;

            fruitPos[dishIndex] = fruitIndex;

            dishes[dishIndex].transform.localScale = Vector3.one;
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

                    if (winterFruits[i].activeInHierarchy)
                        winterFruits[i].SetActive(false);
                }
                StartCoroutine(Sell_SpringFruits());
                break;

            case "Summer":
                for (int i = 0; i < 8; i++)
                {
                    if (dishes[i].transform.localScale == Vector3.one)
                        dishes[i].transform.localScale = reverse;

                    if (springFruits[i].activeInHierarchy)
                        springFruits[i].SetActive(false);
                }
                StartCoroutine(Sell_SummerFruits());
                break;

            case "Autumn":
                for (int i = 0; i < 8; i++)
                {
                    if (dishes[i].transform.localScale == Vector3.one)
                        dishes[i].transform.localScale = reverse;

                    if (summerFruits[i].activeInHierarchy)
                        summerFruits[i].SetActive(false);
                }
                StartCoroutine(Sell_AutumnFruits());
                break;

            case "Winter":
                for (int i = 0; i < 9; i++)
                {
                    if (i < 8 && dishes[i].transform.localScale == Vector3.one)
                        dishes[i].transform.localScale = reverse;

                    if (autumnFruits[i].activeInHierarchy)
                        autumnFruits[i].SetActive(false);
                }
                StartCoroutine(Sell_WinterFruits());
                break;

            default:
                StartCoroutine(Sell_SpringFruits());
                break;
        }
    }

}

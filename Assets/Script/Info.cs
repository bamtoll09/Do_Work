using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {

    public Animator animator;
    public GameObject Info_Spring, Info_Summer, Info_Autumn, Info_Winter;

    public static bool isOpened = false;
    string checkSeason = "Season";

    // Use this for initialization
    void Start () {
        Info_Spring.SetActive(false);
        Info_Summer.SetActive(false);
        Info_Autumn.SetActive(false);
        Info_Winter.SetActive(false);
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
                    Info_Winter.SetActive(false);
                    Info_Spring.SetActive(true);
                    break;

                case "Summer":
                    Info_Spring.SetActive(false);
                    Info_Summer.SetActive(true);
                    break;

                case "Autumn":
                    Info_Summer.SetActive(false);
                    Info_Autumn.SetActive(true);
                    break;

                case "Winter":
                    Info_Autumn.SetActive(false);
                    Info_Winter.SetActive(true);
                    break;
            }
        }
    }

    public void open()
    {
        if (!Shop.isOpened)
        {
            animator.SetBool("isOpened", true);
            isOpened = true;
            Debug.Log("Info: " + isOpened);
        }
    }

    public void close()
    {
        if (isOpened)
        {
            animator.SetBool("isOpened", false);
            isOpened = false;
            Debug.Log("Info: " + isOpened);
        }
    }
}

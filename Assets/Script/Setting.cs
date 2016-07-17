using UnityEngine;
using System.Collections;

public class Setting : MonoBehaviour {

    public GameObject option;

    public static bool isOpened = false;

	// Use this for initialization
	void Start () {
        option.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void open()
    {
        option.SetActive(true);
        isOpened = true;
    }

    public void exit()
    {
        isOpened = false;
        option.SetActive(false);
    }
}

using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

    public static Environment instance;
    public Animator animator;

    int outsideBro = 1;
    float saveTime = 0.0f;

    void Awake()
    {
        if (instance == null)
            instance = this;

        GetData();
    }
    
	// Use this for initialization
	void Start () {
        animator.SetInteger("outsideBro", outsideBro);
	}
	
	// Update is called once per frame
	void Update () {
	    if (saveTime >= GameController.saveDelay)
        {
            SaveData();
            saveTime = 0.0f;
        }

        saveTime += Time.deltaTime;
	}

    public void changeEnvironment()
    {
        if (outsideBro < 5)
        {
            outsideBro++;
            animator.SetInteger("outsideBro", outsideBro);
        }
    }

    void GetData()
    {
        outsideBro = PlayerPrefs.GetInt("OutsideBro", 1);
    }

    void SaveData()
    {
        PlayerPrefs.SetInt("OutsideBro", outsideBro);
    }
}

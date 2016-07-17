using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

    public static Environment instance;
    public Animator animator;

    int outsideBro = 1;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void changeEnvironment()
    {
        if (outsideBro < 5)
        {
            outsideBro++;
            animator.SetInteger("outsideBro", outsideBro);
        }
    }
}

using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    public Animator animator;

    public static bool isOpened = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void open()
    {
        if (!Info.isOpened)
        {
            animator.SetBool("isOpened", true);
            isOpened = true;
            GameController.canEscape = false;
            Debug.Log(GameController.canEscape);
        }
    }

    public void close()
    {
        if (isOpened)
        {
            animator.SetBool("isOpened", false);
            isOpened = false;
            GameController.canEscape = true;
            Debug.Log(GameController.canEscape);
        }
    }
}

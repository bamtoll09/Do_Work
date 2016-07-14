using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public Text text_touchCount;
    StringBuilder sb = new StringBuilder();

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
	    if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    public void initialize()
    {
        DanEE.touchCount = 0;
        setTextCount();
    }

    public void setTextCount()
    {
        sb.Remove(0, sb.Length);
        sb.Append(DanEE.touchCount);
        text_touchCount.text = sb.ToString();
    }
}

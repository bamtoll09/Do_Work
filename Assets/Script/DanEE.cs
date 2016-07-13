using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DanEE : MonoBehaviour {

    public SpriteRenderer sr;
    Color[] colors = { Color.black, Color.red, Color.yellow, Color.green, Color.blue, Color.cyan, Color.gray, Color.white };

    float delayTime = 0.02f;
    float time = 0.0f;
    int i = 0;
    public static int touchCount = 0;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount != 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                touchCount++;
        }

        if (touchCount == 0 && !sr.color.Equals(Color.white))
            sr.color = Color.white;

        if (touchCount >= 20)
        {
            changeColor();
        }
	}

    void changeColor()
    {

        if (time > delayTime)
        {
            sr.color = colors[i];
            i++;

            if (i == colors.Length)
                i = 0;

            time = 0.0f;
        }

        time += Time.deltaTime;
    }
}

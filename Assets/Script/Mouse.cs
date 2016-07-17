using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

    public Animator animator;

    float times = 0.0f;

    void OnEnable()
    {
        animator.SetBool("activate", true);
    }

    // Update is called once per frame
    void Update () {
	    float speed = 1.2f;
        transform.Translate(new Vector3(0.0f, speed, 0.0f) * Time.deltaTime/*Vector3.up * speed * Time.deltaTime * 2*/);

        if (times >= 1.0f)
        {
            animator.SetBool("activate", false);
            gameObject.SetActive(false);
        }

            times += Time.deltaTime;
	}

    void OnDisable()
    {
        times = 0.0f;
    }
}

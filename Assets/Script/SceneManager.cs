using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void gotoIngame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("s");
    }
}

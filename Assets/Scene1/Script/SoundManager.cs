using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    public AudioClip cannon;
    public AudioClip heal;

    AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play(string name)
    {
        switch (name)
        {
            case "cannon":
                audioSource.PlayOneShot(cannon);
                break;

            case "heal":
                audioSource.PlayOneShot(heal);
                break;
        }
    }
}

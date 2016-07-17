using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Text text_sound;
    public Text text_music;
    public AudioClip asdf;
    public AudioSource myAudio;
    StringBuilder sb = new StringBuilder();

    public static int soundVolume = 5; // 효과으ㅏㅁ
    public static int musicVolume = 5; // BGM

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

    public void playAsdf()
    {
        myAudio.PlayOneShot(asdf, soundVolume * 0.2f);
    }

    public void soundVolumeUp()
    {
        sb.Remove(0, sb.Length);
        if (soundVolume < 5)
            soundVolume++;
        sb.Append(soundVolume);
        text_sound.text = sb.ToString();
    }

    public void soundVolumeDown()
    {
        sb.Remove(0, sb.Length);
        if (soundVolume > 0)
            soundVolume--;
        sb.Append(soundVolume);
        text_sound.text = sb.ToString();
    }

    public void musicVolumeUp()
    {
        sb.Remove(0, sb.Length);
        if (musicVolume < 5)
            musicVolume++;
        sb.Append(musicVolume);
        text_music.text = sb.ToString();
        myAudio.volume = musicVolume * 0.2f;
    }

    public void musicVolumeDown()
    {
        sb.Remove(0, sb.Length);
        if (musicVolume > 0)
            musicVolume--;
        sb.Append(musicVolume);
        text_music.text = sb.ToString();
        myAudio.volume = musicVolume * 0.2f;
    }
}

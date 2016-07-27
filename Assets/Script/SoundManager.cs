using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Text text_sound;
    public Text text_music;
    public AudioClip buyFruit;
    public AudioClip buyShop;
    public AudioClip dish;
    public AudioClip hamster;
    public AudioSource myAudio;
    StringBuilder sb = new StringBuilder();

    public static int soundVolume = 5; // 효과으ㅏㅁ
    public static int musicVolume = 5; // BGM
    float saveTime = 0.0f;
    void Awake()
    {
        if (instance == null)
            instance = this;

        GetData();
    }

	// Update is called once per frame
	void Update ()
    {
        if (saveTime >= GameController.saveDelay)
        {
            SaveData();
            saveTime = 0.0f;
        }

        saveTime += Time.deltaTime;
    }

    public void playBuyFruit()
    {
        myAudio.PlayOneShot(buyFruit, soundVolume * 0.2f);
    }

    public void playBuyShop()
    {
        myAudio.PlayOneShot(buyShop, soundVolume * 0.2f);
    }

    public void playDish()
    {
        myAudio.PlayOneShot(dish, soundVolume * 0.2f);
    }

    public void playHamster()
    {
        myAudio.PlayOneShot(hamster, soundVolume * 0.2f);
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

    void GetData()
    {
        soundVolume = PlayerPrefs.GetInt("SoundVolume", 5);
        musicVolume = PlayerPrefs.GetInt("MusicVolume", 5);
    }

    void SaveData()
    {
        PlayerPrefs.SetInt("MusicVolume", musicVolume);
        PlayerPrefs.SetInt("SoundVolume", soundVolume);
    }
}

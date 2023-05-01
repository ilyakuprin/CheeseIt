using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _effects;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            _music.volume = PlayerPrefs.GetFloat("Music");
        }
        else
        {
            PlayerPrefs.SetFloat("Music", 0.5f);
        }

        if (PlayerPrefs.HasKey("Effects"))
        {
            _effects.volume = PlayerPrefs.GetFloat("Effects");
        }
        else
        {
            PlayerPrefs.SetFloat("Effects", 0.5f);
        }
    }

    public void SoundsSettings()
    {
        _music.volume = PlayerPrefs.GetFloat("Music");
        _effects.volume = PlayerPrefs.GetFloat("Effects");
    }

    public void PlayEffect()
    {
        _effects.Play();
    }

    public void PlayMusic()
    {
        _music.Play();
    }

    public void StopMusic()
    {
        _music.Pause();
    }
}

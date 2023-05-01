using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonControllerMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _buttons1;
    [SerializeField] private GameObject _buttons2;
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderEffects;
    [SerializeField] private Text _numberResult;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Timer"))
        {
            _numberResult.text = PlayerPrefs.GetFloat("Timer").ToString();
        }
        else
        {
            PlayerPrefs.SetFloat("Timer", 0);
        }

        if (PlayerPrefs.HasKey("Music"))
        {
            _sliderMusic.value = PlayerPrefs.GetFloat("Music");
        }
        else
        {
            PlayerPrefs.SetFloat("Music", 0.5f);
        }

        if (PlayerPrefs.HasKey("Effects"))
        {
            _sliderEffects.value = PlayerPrefs.GetFloat("Effects");
        }
        else
        {
            PlayerPrefs.SetFloat("Effects", 0.5f);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        _sliderMusic.value = PlayerPrefs.GetFloat("Music");
        _sliderEffects.value = PlayerPrefs.GetFloat("Effects");

        _buttons1.SetActive(false);
        _buttons2.SetActive(true);
    }

    public void Back()
    {
        _buttons1.SetActive(true);
        _buttons2.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Music()
    {
        PlayerPrefs.SetFloat("Music", _sliderMusic.value);
    }

    public void Effects()
    {
        PlayerPrefs.SetFloat("Effects", _sliderEffects.value);
    }
}

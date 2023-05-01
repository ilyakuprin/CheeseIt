using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class CanvasConrollerInGame : MonoBehaviour
{
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private Text _timerCurrentText;
    [SerializeField] private Text _timerBestText;
    [SerializeField] private Text _finalResult;
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderEffects;
    [SerializeField] private GameObject _buttons1;
    [SerializeField] private GameObject _buttons2;
    [SerializeField] private MusicController _musicController;

    private float _timer = 0;
    private bool _isPause = false;
    private bool _isGameOver = false;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Timer"))
        {
            _timerBestText.text = PlayerPrefs.GetFloat("Timer").ToString();
        }
        else
        {
            PlayerPrefs.SetFloat("Timer", _timer);
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

        _musicController.SoundsSettings();
        _musicController.PlayMusic();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && !_isPause && !_isGameOver)
        {
            _pauseScreen.SetActive(true);
            _isPause = true;
            Time.timeScale = 0f;

            _musicController.StopMusic();

            _buttons1.SetActive(true);
            _buttons2.SetActive(false);
        }
        else if (Input.GetButtonDown("Cancel") && _isPause && !_isGameOver)
        {
            Continue();
        }

        if (!_isPause && !_isGameOver)
        {
            _timer += Time.deltaTime;
            _timerCurrentText.text = _timer.ToString();
        }

        if (_timer >= PlayerPrefs.GetFloat("Timer"))
        {
            _timerBestText.text = _timer.ToString();
        }
    }

    public void Continue()
    {
        _pauseScreen.SetActive(false);
        _isPause = false;
        Time.timeScale = 1f;
        _musicController.PlayMusic();

        _musicController.SoundsSettings();
    }

    public void Settings()
    {
        _sliderMusic.value = PlayerPrefs.GetFloat("Music");
        _sliderEffects.value = PlayerPrefs.GetFloat("Effects");

        _buttons1.SetActive(false);
        _buttons2.SetActive(true);
    }

    public void Music()
    {
        PlayerPrefs.SetFloat("Music", _sliderMusic.value);
    }

    public void Effects()
    {
        PlayerPrefs.SetFloat("Effects", _sliderEffects.value);
    }

    public void Back()
    {
        _buttons1.SetActive(true);
        _buttons2.SetActive(false);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Death()
    {
        _isGameOver = true;

        _musicController.StopMusic();
        _musicController.PlayEffect();

        if (_timer >= PlayerPrefs.GetFloat("Timer"))
        {
            PlayerPrefs.SetFloat("Timer", _timer);
            _finalResult.text = "New record!";
        }
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.7f);
        _gameOverScreen.SetActive(true);
    }
}

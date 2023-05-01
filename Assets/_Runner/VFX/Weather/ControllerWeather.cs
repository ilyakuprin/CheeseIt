using UnityEngine;

public class ControllerWeather : MonoBehaviour
{
    [SerializeField] ParticleSystem _rain;

    private float _timeGame;
    private readonly int _minTimeRain = 20;
    private readonly int _maxTimeRain = 35;
    private int _minimumTimeWithoutRain = 20;
    private int _maximumTimeWithoutRain = 30;
    

    private int _startTimeRain;
    private int _timeRain;

    private bool _isPlaying = false;

    private void Start()
    {
        var random = new System.Random();
        _startTimeRain = random.Next(_minimumTimeWithoutRain, _maximumTimeWithoutRain);
        _timeRain = random.Next(_minTimeRain, _maxTimeRain);
    } 

    private void Update()
    {
        _timeGame += Time.deltaTime;

        if (!_isPlaying && _timeGame > _startTimeRain)
        {
            _rain.Play();
            _isPlaying = true;
            _timeRain += _startTimeRain;
        }
        else if (_isPlaying && _timeGame > _timeRain)
        {
            _rain.Stop();
            _isPlaying = false;

            var random = new System.Random();
            _startTimeRain = _timeRain + random.Next(_minimumTimeWithoutRain, _maximumTimeWithoutRain);
            _timeRain = random.Next(_minTimeRain, _maxTimeRain); ;
        }
    }
}

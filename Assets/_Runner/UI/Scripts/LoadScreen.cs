using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] GameObject _canvasController;
    
    private readonly int _startFontSize = 70;
    private readonly int _speed = 1;
    private readonly int _timeSec = 3;

    private int _fontSize;

    private void Start()
    {
        _canvasController.SetActive(false);
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        for (int i = _timeSec; i > 0; i--)
        {
            _fontSize = _startFontSize;
            _text.text = i.ToString();

            for (float j = 0; j < 1f; j += Time.deltaTime)
            {
                _text.fontSize = _fontSize;
                _fontSize += _speed;
                yield return null;
            }
        }

        _canvasController.SetActive(true);
        gameObject.SetActive(false);
        _playerMovement.enabled = true;
    }
}

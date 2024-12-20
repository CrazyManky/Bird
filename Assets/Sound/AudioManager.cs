using Project.Screpts.SO;
using UnityEngine;

public class AudioManager : MonoBehaviour, IService
{
    [SerializeField] private SounConfig _sounConfig;
    [SerializeField] private AudioSource _audioMenuMusick;
    [SerializeField] private AudioSource _audioGameMusick;
    [SerializeField] private AudioSource _buttonClick;
    [SerializeField] private AudioSource _colisionEnter;


    public void PlayButtonClick()
    {
        if (_sounConfig.AudioActive)
        {
            _buttonClick.Play();
        }
    }

    public void PlayMenuMusick()
    {
        if (_sounConfig.AudioActive)
        {
            _audioGameMusick.Stop();
            _audioMenuMusick.Play();
        }
    }

    public void Update()
    {
        if (!_sounConfig.AudioActive)
        {
            _audioGameMusick.volume = 0f;
            _audioMenuMusick.volume = 0f;
            _buttonClick.volume = 0f;
        }
        else
        {
            _audioGameMusick.volume = 0.01f;
            _audioMenuMusick.volume = 0.01f;
            _buttonClick.volume = 0.1f;
        }
    }

    public void PlayColisionActive()
    {
        if (_sounConfig.AudioActive)
        {
            _colisionEnter.Play();
        }
    }

    public void PlayGame()
    {
        if (_sounConfig.AudioActive)
        {
            _audioMenuMusick.Stop();
            _audioGameMusick.Play();
        }
    }
}
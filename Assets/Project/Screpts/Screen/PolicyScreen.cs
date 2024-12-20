using Services;
using UnityEngine;

public class PolicyScreen : MonoBehaviour
{
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = ServiceLocator.Instance.GetService<AudioManager>();
    }

    public void AcceptPolicy()
    {
        _audioManager.PlayButtonClick();
        Destroy(gameObject);
    }
}
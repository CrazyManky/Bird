using UnityEngine;

namespace Project.Screpts.SO
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "SoundConfig")]
    public class SounConfig : ScriptableObject
    {
        [SerializeField] private bool _audioActive;

        public bool AudioActive => _audioActive;

        public void SwitchAudio()
        {
            var value = !_audioActive;
            _audioActive = !_audioActive;
        }
    }
}
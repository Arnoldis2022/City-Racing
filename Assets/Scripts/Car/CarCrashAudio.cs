using UnityEngine;

public class CarCrashAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _crashClip;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Car _car;
    [SerializeField] private float _crashHeight;
    [SerializeField] private float _targetSpeed = 30f;

    private void Awake()
    {
        _car.Collision += PlayCrashAudio;
    }

    private void PlayCrashAudio(Collision collision)
    {
        foreach(var contact in collision.contacts)
        {
            if (contact.point.y >= _crashHeight && _car.Speed > _targetSpeed)
            {
                _audioSource.PlayOneShot(_crashClip);
                break;
            }
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DestroyableTraffic : MonoBehaviour
{
    [SerializeField] private AudioSource _explosionSource;
    [SerializeField] private AudioClip _explosionClip;
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _explosiveObject;
    [SerializeField] private float _rewardCredits = 100;
    [SerializeField] private Rigidbody _rigidbody;
    private string _targetTag;

    private void Awake()
    {
        if(_player == null)
            _player = FindObjectOfType<Player>();
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
        _targetTag = _player.tag;
    }

    private void OnEnable()
    {
        _explosiveObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == _targetTag)
        {
            if(_player.Car.Speed >= 100f)
            {
                _player.IncreaseCredits(_rewardCredits);
                Explosion();
            }
        }
    }

    private void Explosion()
    {
        _explosionSource.PlayOneShot(_explosionClip);
        _explosionParticle.Play();
        _explosiveObject.SetActive(false);
        _rigidbody.velocity = Vector3.zero;
    }
}

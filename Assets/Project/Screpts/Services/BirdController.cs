using Project.Screpts.Services;
using Services;
using UnityEngine;

public class BirdController : MonoBehaviour, IPauseItem
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private AudioManager _audioManager;

    private float _jumpForce = 5f;

    public Rigidbody2D RB => _rb;

    private void Start()
    {
        _audioManager = ServiceLocator.Instance.GetService<AudioManager>();
    }

    public void Pause()
    {
        _rb.simulated = false;
    }

    public void Continue()
    {
        _rb.simulated = true;
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
            Jump();
    }

    public void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        _audioManager.PlayColisionActive();
    }

    public void FlipSprite(float horizontalDirection)
    {
        _spriteRenderer.flipX = horizontalDirection > 0;
    }
}
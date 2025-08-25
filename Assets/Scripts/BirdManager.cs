using UnityEngine;

public class BirdManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _flapForce = 5f;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _rigidbody2D.simulated = false;
    }

    private void Update()
    {
        // Start the flapping animation on first Space press
        if (GameManager.Instance.CurrentState == GameState.Ready && Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("StartFlap");
            GameManager.Instance.StartGame();

            _rigidbody2D.simulated = true;
        }

        if (GameManager.Instance.CurrentState == GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Flap();
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentState == GameState.Playing)
        {
            float targetAngle = Mathf.Lerp(-90, 35, Mathf.InverseLerp(-5, 5, _rigidbody2D.linearVelocity.y));
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 8f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // You can check the tag or name of the collided object if needed
        if (GameManager.Instance.CurrentState == GameState.Playing)
        {
            GameManager.Instance.GameOver();
            _animator.enabled = false;
        }
    }

    private void Flap()
    {
        _rigidbody2D.linearVelocity = Vector2.up * _flapForce;
    }


}

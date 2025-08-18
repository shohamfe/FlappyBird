using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _birdPrefab;

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 200f;
    [SerializeField] private float _jumpForce = 10f;

    public static GameManager Instance { get; private set; }
    public float Speed { get { return _moveSpeed * Time.deltaTime; } }
    public float JumpForce { get { return _jumpForce; } }
    public GameObject BirdPrefab { get { return _birdPrefab; } }


    void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}

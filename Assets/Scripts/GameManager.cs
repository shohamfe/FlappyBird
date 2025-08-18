using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]

    [SerializeField] private GameObject _birdPrefab;
    [SerializeField] private GameObject _floorPrefab;
    [SerializeField] private GameObject _pipePrefab;

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 200f;
    [SerializeField] private float _jumpForce = 10f;

    public static GameManager Instance { get; private set; }
    public float Speed { get { return _moveSpeed * Time.deltaTime; } }
    public float JumpForce { get { return _jumpForce; } }
    public GameObject BirdPrefab { get { return _birdPrefab; } }
    public GameObject FloorPrefab { get { return _floorPrefab; } }
    public GameObject PipePrefab { get { return _pipePrefab; } }


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

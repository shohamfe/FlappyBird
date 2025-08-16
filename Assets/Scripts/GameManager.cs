using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]

    [SerializeField] private GameObject _birdPrefab;
    [SerializeField] private GameObject _floorPrefab;

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 200f;
    [SerializeField] private float _jumpForce = 10f;

    public static GameManager Instance { get; private set; }
    public float MoveSpeed { get { return _moveSpeed; } }
    public float JumpForce { get { return _jumpForce; } }
    public GameObject BirdPrefab { get { return _birdPrefab; } private set { _birdPrefab = value; } }
    public GameObject FloorPrefab { get { return _floorPrefab; } private set { _floorPrefab = value; } }

    void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        // BirdPrefab = _birdPrefab;
        // FloorPrefab = _floorPrefab;
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

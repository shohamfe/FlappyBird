using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _birdPrefab;

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 150f;

    [Header("Text")]
    [SerializeField] private GameObject _gameOverText;

    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; } = GameState.Ready;

    public float Speed { get { return _moveSpeed * Time.deltaTime; } }
    public GameObject BirdPrefab { get { return _birdPrefab; } }

    public void StartGame()
    {
        CurrentState = GameState.Playing;
    }

    public void GameOver()
    {
        CurrentState = GameState.GameOver;
        _gameOverText.SetActive(true);
    }

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
        if (CurrentState == GameState.GameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

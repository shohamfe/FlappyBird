using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private GameObject _floorPrefab;
    [SerializeField] private Collider2D _floorCollider;
    public static FloorManager Instance { get; private set; }
    private List<GameObject> _floors = new();
    public Vector3 Size { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        Size = _floorPrefab.GetComponent<SpriteRenderer>().bounds.size;

        BuildLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.GameOver)
            ScrollFloors();
    }

    private void BuildLevel()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject floor = Instantiate(_floorPrefab, new Vector3(i * Size.x - Size.x / 2, ScreenManager.ScreenBottomEdge, 0), Quaternion.identity);
            _floors.Add(floor);
        }
    }

    private void ScrollFloors()
    {
        // Move left by a speed value if in view, else move to the right of the other floor
        int i = 0;
        _floors.ForEach(floor =>
        {
            Vector3 pos = floor.transform.position;

            // Calculate the right edge of the floor
            float floorRightEdge = pos.x + Size.x;

            // Screen left edge in world coordinates
            if (floorRightEdge > ScreenManager.ScreenLeftEdge)
            {
                // Move left
                floor.transform.position += new Vector3(-GameManager.Instance.Speed, 0, 0);
            }
            else
            {
                // Move to the right of the other floor
                int otherIdx = (i + 1) % 2;
                GameObject otherFloor = _floors[otherIdx];
                Vector3 otherPos = otherFloor.transform.position;
                floor.transform.position = new Vector3(otherPos.x + Size.x - GameManager.Instance.Speed, pos.y, pos.z);
            }
            i++;
        });
    }
}

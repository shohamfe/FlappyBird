using System.Collections.Generic;
using UnityEngine;

public class FloorScroller : MonoBehaviour
{
    private GameObject _floorPrefab;
    private List<GameObject> _floors = new();
    private float _floorWidth;

    void Awake()
    {

        BuildLevel();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!_floorPrefab)
            BuildLevel();
        ScrollFloors();
    }

    private void BuildLevel()
    {
        if (!_floorPrefab && GameManager.Instance?.FloorPrefab)
        {
            _floorPrefab = GameManager.Instance.FloorPrefab;
            _floorWidth = _floorPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        }

        if (_floorPrefab)
            for (int i = 0; i < 2; i++)
            {
                GameObject floor = Instantiate(_floorPrefab, new Vector3(i * _floorWidth - _floorWidth / 2, -256, 0), Quaternion.identity);
                _floors.Add(floor);
            }
    }

    private void ScrollFloors()
    {
        // Move left by a speed value if in view, else move to the right of the other floor
        if (_floorPrefab)
        {
            if (_floors.Count == 0)
                BuildLevel();

            float speed = GameManager.Instance.MoveSpeed * Time.deltaTime;


            int i = 0;
            _floors.ForEach(floor =>
            {
                Vector3 pos = floor.transform.position;

                // Calculate the right edge of the floor
                float floorRightEdge = pos.x + _floorWidth;

                // Camera left edge in world coordinates
                float cameraLeftEdge = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;

                if (floorRightEdge > cameraLeftEdge)
                {
                    // Move left
                    floor.transform.position = new Vector3(pos.x - speed, pos.y, pos.z);
                }
                else
                {
                    // Move to the right of the other floor
                    int otherIdx = (i + 1) % 2;
                    GameObject otherFloor = _floors[otherIdx];
                    Vector3 otherPos = otherFloor.transform.position;
                    floor.transform.position = new Vector3(otherPos.x + _floorWidth - speed, pos.y, pos.z);
                }
                i++;
            });
        }
    }
}

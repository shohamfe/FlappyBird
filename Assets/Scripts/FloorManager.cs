using System.Collections.Generic;
using UnityEngine;

public class FloorScroller : MonoBehaviour
{
    private GameObject _floorPrefab;
    private List<GameObject> _floors = new();
    private float _floorWidth;

    void Awake()
    {
    }

    void Start()
    {
        BuildLevel();
    }

    // Update is called once per frame
    void Update()
    {
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
                GameObject floor = Instantiate(_floorPrefab, new Vector3(i * _floorWidth - _floorWidth / 2, ScreenManager.ScreenBottomEdge, 0), Quaternion.identity);
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
            float floorRightEdge = pos.x + _floorWidth;

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
                floor.transform.position = new Vector3(otherPos.x + _floorWidth - GameManager.Instance.Speed, pos.y, pos.z);
            }
            i++;
        });
    }
}

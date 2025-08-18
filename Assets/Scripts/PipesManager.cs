using UnityEngine;
using System.Collections.Generic;

public class PipesManager : MonoBehaviour
{
    private float _pipeWidth;
    private float _pipeHeight;
    private float _pipeSpacing;
    private float _floorHeight;
    private readonly List<GameObject> _bottomPipes = new();
    private readonly List<GameObject> _topPipes = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var size = GameManager.Instance.PipePrefab.GetComponent<SpriteRenderer>().bounds.size;
        _pipeWidth = size.x;
        _pipeHeight = size.y;
        _pipeSpacing = _pipeHeight + 120f;
        _floorHeight = GameManager.Instance.FloorPrefab.GetComponent<SpriteRenderer>().bounds.size.y;

        // Spawn 2 pairs only
        for (int i = 0; i < 2; i++)
            SpawnPipe(ScreenManager.ScreenRightEdge + _pipeWidth + i * (_pipeWidth + 200f));
    }

    // Update is called once per frame
    void Update()
    {
        float fixedSpacing = _pipeWidth + 200f; // Adjust 200f as needed for your game
        for (int i = 0; i < _bottomPipes.Count; i++)
        {
            var bottomPipe = _bottomPipes[i];
            var topPipe = _topPipes[i];
            Vector3 pos = bottomPipe.transform.position;
            float pipeRightEdge = pos.x + _pipeWidth;

            if (pipeRightEdge > ScreenManager.ScreenLeftEdge)
            {
                var interval = new Vector3(-GameManager.Instance.Speed, 0, 0);
                bottomPipe.transform.position += interval;
                topPipe.transform.position += interval;

                // Check if bird passed the pipe
                var bird = GameManager.Instance.BirdPrefab; // Assuming this is the bird instance
                if (bird != null)
                {
                    float birdX = bird.transform.position.x;
                    // Use a flag to ensure score is only added once per pipe
                    if (!bottomPipe.TryGetComponent<PassedPipeFlag>(out var flag))
                    {
                        flag = bottomPipe.AddComponent<PassedPipeFlag>();
                    }
                    if (!flag.passed && birdX > pos.x)
                    {
                        ScoreManager.Instance.AddScore(1);
                        flag.passed = true;
                    }
                }
            }
            else
            {
                // Find the other pipe pair
                int otherIdx = (i + 1) % 2;
                var otherPipe = _bottomPipes[otherIdx];
                float newX = otherPipe.transform.position.x + fixedSpacing;
                var randomY = Random.Range(0, _pipeHeight / 2);
                var y = ScreenManager.ScreenBottomEdge + _pipeHeight / 2 - randomY;
                var position = new Vector3(newX, y, 0);
                bottomPipe.transform.position = position;
                topPipe.transform.position = position + new Vector3(0, _pipeSpacing, 0);

                // Reset passed flag for reused pipe
                if (bottomPipe.TryGetComponent<PassedPipeFlag>(out var flag))
                {
                    flag.passed = false;
                }
            }
        }
    }

    private void SpawnPipe(float xPos)
    {
        var randomY = Random.Range(_floorHeight, _pipeHeight / 2);
        var x = xPos;
        var y = ScreenManager.ScreenBottomEdge + _pipeHeight / 2 - randomY;
        var position = new Vector3(x, y, 0);

        GameObject bottomPipe = Instantiate(GameManager.Instance.PipePrefab, position, Quaternion.identity);
        GameObject topPipe = Instantiate(GameManager.Instance.PipePrefab, position + new Vector3(0, _pipeSpacing, 0), Quaternion.Euler(0, 0, 180));
        _bottomPipes.Add(bottomPipe);
        _topPipes.Add(topPipe);
    }
}
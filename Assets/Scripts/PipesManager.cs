using UnityEngine;
using System.Collections.Generic;

public class PipesManager : Singleton<PipesManager>
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _pipePairPrefab;

    private readonly List<GameObject> _pipePairs = new();
    private Vector3 _size;

    private float _pipesSpacing = 200f;


    private bool pipesCreated = false;

    void Update()
    {
        if (GameManager.Instance.CurrentState == GameState.Playing)
        {
            if (!pipesCreated)
            {
                CreatePipes();
                pipesCreated = true;
            }
            else
            {
                UpdatePipes();
            }
        }
    }

    private void CreatePipes()
    {
        StartCoroutine(CreatePipesWithDelay());
    }

    private System.Collections.IEnumerator CreatePipesWithDelay()
    {
        yield return new WaitForSeconds(2f);

        _size = _pipePairPrefab.GetComponentInChildren<SpriteRenderer>().bounds.size;
        _pipesSpacing = _size.x + 200f;

        for (int i = 0; i < 2; i++)
            SpawnPipePair(ScreenManager.ScreenRightEdge + _size.x + i * _pipesSpacing);
    }

    private void UpdatePipes()
    {
        for (int i = 0; i < _pipePairs.Count; i++)
        {
            var pipePair = _pipePairs[i];
            Vector3 pos = pipePair.transform.position;
            float pipeRightEdge = pos.x + _size.x;

            if (pipeRightEdge > ScreenManager.ScreenLeftEdge)
            {
                var interval = new Vector3(-GameManager.Instance.Speed, 0, 0);
                pipePair.transform.position += interval;
            }
            else
            {
                int otherIdx = (i + 1) % 2;
                var otherPipePair = _pipePairs[otherIdx];
                float newX = otherPipePair.transform.position.x + _pipesSpacing;
                var y = GetRandomY();
                var position = new Vector3(newX, y, 0);
                pipePair.transform.position = position;

                // Reset gap trigger score flag
                var gapTrigger = pipePair.transform.Find("GapTrigger");
                if (gapTrigger != null)
                {
                    var gapScript = gapTrigger.GetComponent<PipeGapTrigger>();
                    if (gapScript != null)
                        gapScript.ResetScoreFlag();
                }
            }
        }
    }

    private float GetRandomY()
    {
        return _size.y + Random.Range(-_size.y / 2, _size.y / 10);
    }

    private void SpawnPipePair(float xPosition)
    {
        var y = GetRandomY();
        var pipePair = Instantiate(_pipePairPrefab, new Vector3(xPosition, y, 0), Quaternion.identity);
        _pipePairs.Add(pipePair);

    }
}
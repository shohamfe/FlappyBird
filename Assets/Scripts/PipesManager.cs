using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PipesManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _pipePairPrefab;

    private readonly List<GameObject> _pipePairs = new();
    private Vector3 _size;

    private float _pipesSpacing = 200f;
    public static PipesManager Instance { get; private set; }



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _size = _pipePairPrefab.GetComponentInChildren<SpriteRenderer>().bounds.size;
        _pipesSpacing = _size.x + 200f;

        for (int i = 0; i < 2; i++)
            SpawnPipePair(ScreenManager.ScreenRightEdge + _size.x + i * _pipesSpacing);
    }

    // Update is called once per frame
    void Update()
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
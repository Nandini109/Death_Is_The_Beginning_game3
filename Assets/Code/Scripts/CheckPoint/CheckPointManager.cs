using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager Instance { get; private set; }
    private Vector2 checkPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCurrentCheckpoint(Vector2 newCheckpoint)
    {
        checkPoint = newCheckpoint;
    }

    public Vector2 GetCurrentCheckpoint()
    {
        return checkPoint;
    }
}

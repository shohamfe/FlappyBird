using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static float ScreenLeftEdge { get { return Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect; } }
    public static float ScreenRightEdge { get { return Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect; } }
    public static float ScreenTopEdge { get { return Camera.main.transform.position.y + Camera.main.orthographicSize; } }
    public static float ScreenBottomEdge { get { return Camera.main.transform.position.y - Camera.main.orthographicSize; } }
    public static float ScreenWidth { get { return Camera.main.orthographicSize * Camera.main.aspect * 2; } }
    public static float ScreenHeight { get { return Camera.main.orthographicSize * 2; } }
}

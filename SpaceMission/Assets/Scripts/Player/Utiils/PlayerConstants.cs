using UnityEngine;

public class PlayerConstants : MonoBehaviour
{
    public static PlayerConstants Instance { get; private set; }
    public int points;

    void Start()
    {

    }

    private void Awake()
    {
        Instance = this;
    }

    public void AddPoint()
    {
        points++;
    }
}
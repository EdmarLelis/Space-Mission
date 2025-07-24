using TMPro;
using UnityEngine;

public class ScoreMarker : MonoBehaviour
{
    public TextMeshProUGUI pointsText;

    void Start()
    {
        
    }

    void Update()
    {
        pointsText.text = PlayerConstants.Instance.points.ToString() + " X";
    }
}

using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float additionalScrollSpeed;
    [SerializeField] private GameObject[] backgrounds;
    [SerializeField] float[] scrollSpeed;

    void FixedUpdate()
    {
        for (int background = 0; background < backgrounds.Length; background++)
        {
            Renderer render = backgrounds[background].GetComponent<Renderer>();
            float offset = Time.time * (scrollSpeed[background] + additionalScrollSpeed);
            render.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
    }
}

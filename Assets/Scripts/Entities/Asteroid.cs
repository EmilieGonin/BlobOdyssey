using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private void Awake()
    {
        SetPositionOutsideViewport();
    }

    private void SetPositionOutsideViewport()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        int randomSide = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;

        switch (randomSide)
        {
            case 0: // bottom
                spawnPosition = new Vector3(Random.Range(bottomLeft.x, topRight.x), bottomLeft.y - 1, 0);
                break;
            case 1: // top
                spawnPosition = new Vector3(Random.Range(bottomLeft.x, topRight.x), topRight.y + 1, 0);
                break;
            case 2: // left
                spawnPosition = new Vector3(bottomLeft.x - 1, Random.Range(bottomLeft.y, topRight.y), 0);
                break;
            case 3: // right
                spawnPosition = new Vector3(topRight.x + 1, Random.Range(bottomLeft.y, topRight.y), 0);
                break;
        }

        transform.position = spawnPosition;
    }
}
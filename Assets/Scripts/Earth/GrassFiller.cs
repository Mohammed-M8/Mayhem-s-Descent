using UnityEngine;

public class GrassFiller : MonoBehaviour
{
    public GameObject grassPrefab;
    public Vector2 areaSize = new Vector2(10, 10);
    public float spacing = 1.0f;
    public bool randomize = true;

    void Start()
    {
        FillWithGrass();
    }

    void FillWithGrass()
    {
        Vector3 startPos = transform.position - new Vector3(areaSize.x / 2, 0, areaSize.y / 2);

        for (float x = 0; x < areaSize.x; x += spacing)
        {
            for (float z = 0; z < areaSize.y; z += spacing)
            {
                Vector3 spawnPos = startPos + new Vector3(x, 0, z);
                if (randomize)
                {
                    spawnPos += new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));
                }
                GameObject grass = Instantiate(grassPrefab, spawnPos, Quaternion.Euler(0, Random.Range(0, 360), 0), transform);
            }
        }
    }
}

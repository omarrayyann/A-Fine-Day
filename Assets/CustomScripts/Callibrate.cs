using UnityEngine;

public class Callibrate : MonoBehaviour
{
    private double startX = 0;
    private double startY = 0;
    private double startZ = 0;
    [SerializeField]
    private Spawner spawner;
    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = this.transform.position;
        startX = this.transform.position.x;
        startY = this.transform.position.y;
        startZ = this.transform.position.z;
    }

    void Update()
    {
        if (isReady())
        {
            spawner.spawn();
            Debug.Log("Goodbye World");
            enabled = false;
        }
    }

    public bool isReady()
    {
        Debug.Log(previousPosition + " " + this.transform.position);
        bool isReady = this.transform.position.x > previousPosition.x + 0.2 || this.transform.position.y > previousPosition.y + 0.2 || this.transform.position.z > previousPosition.z + 0.2;
        previousPosition = this.transform.position;
        return isReady;
    }
}

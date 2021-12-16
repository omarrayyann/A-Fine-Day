using UnityEngine;

public class Callibrate : MonoBehaviour
{
    [SerializeField]
    private Spawner spawner;
    private Vector3 previousPosition; 


    // Start is called before the first frame update
    void Start()
    {
        previousPosition = this.transform.position;
    }

    void Update()
    {
        if (isReady())
        {
            spawner.callibrated = true;
            Debug.Log("Goodbye World");
            enabled = false;
        }
    }

    public bool isReady()
    {
        bool isReady = this.transform.position.x > previousPosition.x + 0.2 || this.transform.position.y > previousPosition.y + 0.2 || this.transform.position.z > previousPosition.z + 0.2;
        previousPosition = this.transform.position;
        return isReady;
    }
}

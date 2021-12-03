using UnityEngine;

public class WAMSpawner : Spawner
{
    [SerializeField]
    private float armLength;
    [SerializeField]
    private float difficultyRadius;

    private void Start()
    {
        spawn();
    }

    public override void spawn()
    {
        zPos = Random.Range(difficultyRadius, armLength);
        float X2 = Mathf.Pow(armLength, 2) - Mathf.Pow(zPos, 2);
        xPos = Random.Range(-Mathf.Sqrt(X2), Mathf.Sqrt(X2));
        yPos = Random.Range(-Mathf.Sqrt(X2 - Mathf.Pow(xPos, 2)), Mathf.Sqrt(X2 - Mathf.Pow(xPos, 2)));
        target.transform.position = new Vector3(xPos, yPos, zPos) + cameraTransform.position;
        Debug.Log("Moved the ball to " + xPos + ", " + yPos + ", " + zPos + " " + target.transform.position);
    }
}

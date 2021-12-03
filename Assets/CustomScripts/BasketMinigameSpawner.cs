using UnityEngine;
using System.Collections.Generic;

public class BasketMinigameSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    private float xPos;
    private float yPos; 
    private float zPos;
    [SerializeField]
    float armLength;
    [SerializeField]
    private float difficultyRadius;
    [SerializeField]
    private float treeHeight;
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private int numberOfTargets;


    public void spawn(int numberOfTargets)
    {
        for (int i = 0; i < numberOfTargets; i++)
        {
            zPos = Random.Range(difficultyRadius, armLength);
            float X2 = Mathf.Pow(armLength, 2) - Mathf.Pow(zPos, 2);
            xPos = Random.Range(-Mathf.Sqrt(X2), Mathf.Sqrt(X2));
            yPos = Random.Range(treeHeight, Mathf.Sqrt(X2 - Mathf.Pow(xPos, 2)));
            Instantiate(target, new Vector3(xPos, yPos, zPos) + cameraTransform.position, Quaternion.identity);
            Debug.Log("Created target at " + xPos + ", " + yPos + ", " + zPos + " " + gameObject.transform.position);
        }
    }

    public int getNumTargets()
    {
        return numberOfTargets;
    }
}

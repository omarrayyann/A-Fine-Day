using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField]
    protected GameObject target;
    protected float xPos;
    protected float yPos;
    protected float zPos;
    [SerializeField]
    protected Transform cameraTransform;

    public abstract void spawn();
}

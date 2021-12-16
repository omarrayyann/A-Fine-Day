using UnityEngine;

// Abstract spawner class: Any subclass of this handles target spawning
public abstract class Spawner : MonoBehaviour
{
    [SerializeField]
    protected GameObject target;
    protected float xPos;
    protected float yPos;
    protected float zPos;
    [SerializeField]
    protected Transform cameraTransform;
    protected double armLength;
    protected double difficultyPercentage;
    public bool callibrated = false;

    private void Start()
    {
        armLength = Levels.armLength;
    }

    public abstract void spawn();
}

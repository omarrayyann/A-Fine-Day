using UnityEngine;using TMPro;
using TMPro;



public class Basket : MonoBehaviour
{
    [SerializeField]
    public AudioSource appleInBasket;
    [SerializeField]
    public TextMeshProUGUI applesCount;
    public int score = 0;
    [SerializeField]
    public Transform explosion;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Target")
        {
            collider.attachedRigidbody.useGravity = false;
            collider.attachedRigidbody.velocity = new Vector3(0, 0, 0);
            appleInBasket.Play();
            collider.enabled = false;
            score++;
            applesCount.text = "Apples Picked: " + score;
            GameObject exploder = ((Transform)Instantiate(explosion, collider.transform.position, Quaternion.identity)).gameObject;
            Destroy(exploder, 2.0f);
        }
    }

}
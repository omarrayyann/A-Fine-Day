using System.Collections.Generic;
using UnityEngine;
using Leap;


// Most of the apple behaviour is handled in the interactionBehaviour script from ultraleap
public class Apple : MonoBehaviour
{
    // Calls onto basket to log that a hand has caught it
    public void callBasket()
    {
        GameObject basket  = GameObject.FindGameObjectWithTag("Basket Volume");
        basket.GetComponent<Basket>().logCollision(this.transform.position);
    }
}

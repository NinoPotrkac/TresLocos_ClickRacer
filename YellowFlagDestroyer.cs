using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowFlagDestroyer : MonoBehaviour
{
    [SerializeField]
    int carCounter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Level 1 car")
        {
            Destroy(other.gameObject);
            carCounter++;
        }
    }

    private void Update()
    {
        if (carCounter >= 3)
        {
            carCounter = 0;
            this.gameObject.SetActive(false);
        }
    }
}

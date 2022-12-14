using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    private int destroyTime = 1;


    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}

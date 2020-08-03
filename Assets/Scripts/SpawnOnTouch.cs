using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnTouch : MonoBehaviour
{
    public GameObject baseParticle;

    // Update is called once per frame
    void Update()
    {
        var touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            GameObject simulation = Instantiate(baseParticle, transform.position, transform.rotation);
        }
    }
}

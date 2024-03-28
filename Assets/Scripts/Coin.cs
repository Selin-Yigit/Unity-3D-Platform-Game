using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float speedx;
    [SerializeField] private float speedy;
    [SerializeField] private float speedz;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(360*speedx*Time.deltaTime,360*speedy*Time.deltaTime, 360*speedz*Time.deltaTime);
    }
}

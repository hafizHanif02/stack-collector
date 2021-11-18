using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpStackColor : MonoBehaviour
{
    [SerializeField] int value;
    [SerializeField] Color pickUpColor;
    [SerializeField] Renderer rend;
    void Start()
    {
        //Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("color", pickUpColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

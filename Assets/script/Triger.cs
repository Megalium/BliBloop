using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// code pour faire casser le bloc pour faire tomber la boule 
public class Triger : MonoBehaviour
{
    public GameObject Cubedestroy;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // si le joueur au tag player rentre dans la zone de trigger
        {
            Destroy(Cubedestroy); // ca detruit le bloc et fait tomber la boule 
        }
    }

}



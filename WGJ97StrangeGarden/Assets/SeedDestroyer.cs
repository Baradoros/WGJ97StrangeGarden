using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SeedController seed = other.GetComponent<SeedController>();
        if (seed != null)
            Destroy(seed.gameObject);
    }
}

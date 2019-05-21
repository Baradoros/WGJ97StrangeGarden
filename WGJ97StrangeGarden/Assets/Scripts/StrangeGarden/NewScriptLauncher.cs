using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScriptLauncher : MonoBehaviour
{
    private Rigidbody2D rgbd;
    private GameObject seed;
    [SerializeField] private float force;
    [SerializeField] private float speed;
    [SerializeField] private float inaccuracy;

    // Start is called before the first frame update
    private void Start()
    {
        seed = Resources.Load("seed") as GameObject;
        rgbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            ShootSeed();
        }
    }

    public void ShootSeed()
    {
        Vector3 direction = transform.forward + Random.insideUnitSphere * inaccuracy;

        GameObject projectile = Instantiate(seed, GetComponent<Rigidbody2D>().transform.position, Quaternion.LookRotation(direction)) as GameObject;
        //projectile.transform.rotation = Quaternion.LookRotation(direction);
        projectile.GetComponent<Rigidbody2D>().velocity = (projectile.transform.forward * speed);
        projectile.GetComponent<Rigidbody2D>().AddForce(Vector3.up * force);
    }
}

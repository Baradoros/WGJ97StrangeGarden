using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedLauncher : MonoBehaviour
{
    private Rigidbody2D rgbd;
    private GameObject seed;
    private GameObject shootingDir;
    [SerializeField] private float force;
    [SerializeField] private float speed;
    [SerializeField] private float inaccuracy;

    // Start is called before the first frame update
    private void Start()
    {
        seed = Resources.Load("seed") as GameObject;
        rgbd = GetComponent<Rigidbody2D>();
        shootingDir = transform.GetChild(0).gameObject;
        Debug.Log(shootingDir);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            ShootSeed();
        }
    }

    public void ShootSeed()
    {
        Debug.Log(transform.forward);
        Vector3 direction = transform.forward + Random.insideUnitSphere * inaccuracy;

        Vector3 forceV = (shootingDir.transform.position.normalized * force);
        GameObject projectile = Instantiate(seed, GetComponent<Rigidbody2D>().transform.position, Quaternion.LookRotation(direction)) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = (projectile.transform.forward * speed);
        Debug.Log(Vector3.up);
        projectile.GetComponent<Rigidbody2D>().AddForce(new Vector3(0.45f, 0.45f, 0f) * force);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedLauncher : MonoBehaviour
{
    private Rigidbody2D rgbd;
    [SerializeField] private GameObject seed;
    private Animator anim;
    [SerializeField] private float force;
    [SerializeField] private float speed;
    [SerializeField] private float inaccuracy;
    [SerializeField] protected float shotDelay = 1;
    //private float time;

    // Start is called before the first frame update
    private void Start()
    {
        //seed = Resources.Load("seed") as GameObject;
        rgbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        /*RuntimeAnimatorController ac = anim.runtimeAnimatorController;
        Debug.Log(ac);
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == "SeedLaunchShoot")        //If it has the same name as your clip
            {
                time = ac.animationClips[i].length;
            }
        }*/

        StartCoroutine(InitialSeedShot(shotDelay));

    }

    public void ShootSeed()
    {
        Debug.Log(transform.forward);
        Vector3 direction = transform.forward + Random.insideUnitSphere * inaccuracy;

        GameObject projectile = Instantiate<GameObject>(seed, transform.position, Quaternion.LookRotation(direction));
        
        Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();
        projRb.velocity = (projectile.transform.forward * speed);
        
        projRb.AddForce(new Vector3(0.45f, 0.45f, 0f) * force);
    }
    
    IEnumerator InitialSeedShot(float time)
    {
        yield return new WaitForSeconds(time);
        ShootSeed();
        anim.SetBool("shoot", true);
            
        StartCoroutine(ShootingAnim(anim.GetCurrentAnimatorStateInfo(0).length));
    }

    IEnumerator ShootingAnim(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("shoot", false);
    }
}

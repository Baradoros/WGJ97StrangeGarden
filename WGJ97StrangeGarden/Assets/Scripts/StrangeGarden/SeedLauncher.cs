using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedLauncher : MonoBehaviour
{
    private Rigidbody2D rgbd;
    private GameObject seed;
    private Animator anim;
    [SerializeField] private float force;
    [SerializeField] private float speed;
    [SerializeField] private float inaccuracy;
    //private float time;

    // Start is called before the first frame update
    private void Start()
    {
        seed = Resources.Load("seed") as GameObject;
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

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            ShootSeed();
            anim.SetBool("shoot", true);
            
            StartCoroutine(ShootingAnim(anim.GetCurrentAnimatorStateInfo(0).length));
        }
    }

    public void ShootSeed()
    {
        Debug.Log(transform.forward);
        Vector3 direction = transform.forward + Random.insideUnitSphere * inaccuracy;

        GameObject projectile = Instantiate(seed, GetComponent<Rigidbody2D>().transform.position, Quaternion.LookRotation(direction)) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = (projectile.transform.forward * speed);
        Debug.Log(Vector3.up);
        projectile.GetComponent<Rigidbody2D>().AddForce(new Vector3(0.45f, 0.45f, 0f) * force);
    }
    
    IEnumerator ShootingAnim(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("shoot", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeaspoonTools;

public class SeedHitter : MonoBehaviour2D
{
    [SerializeField] Collider2D _hitbox;
    [SerializeField] AttackAngler _attackAngler;
    [SerializeField] float _force =                 100;

    public Collider2D hitbox                        { get { return _hitbox; } }
    public AttackAngler attackAngler                { get { return _attackAngler; } }
    public float force                              { get { return _force; } }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        HandleControls();
    }

    protected virtual void HandleControls()
    {
        if (Input.GetButtonDown("MeleeAttack"))
        {
            Debug.Log("Tried to hit something!");

            // See if any Seeds are within the hitbox
            RaycastHit2D[] hits =                   Physics2D.BoxCastAll(hitbox.transform.position, hitbox.bounds.size, 
                                                    0, Vector2.zero);
            SeedController seed =                   null;

            foreach (RaycastHit2D hit in hits)
            {
                seed =                              hit.transform.GetComponent<SeedController>();

                if (seed != null)
                    HitSeed(seed);
            }
        }
    }

    protected virtual void HitSeed(SeedController seedToHit)
    {
        // Set up the attack force based on the attack angler's orbiter and orbitee
        Vector2 impact =                            attackAngler.orbiter.position - attackAngler.orbitee.position;
        impact.Normalize();
        impact *=                                   force;

        Debug.Log("Hit " + seedToHit.name);
        seedToHit.rigidbody.AddForce(impact);

    }
}

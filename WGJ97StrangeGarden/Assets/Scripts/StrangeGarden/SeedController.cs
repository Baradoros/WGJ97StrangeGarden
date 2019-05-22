using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeaspoonTools;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class SeedController : MonoBehaviour2D
{
    [SerializeField] UnityEvent _Hit;
    [SerializeField] AudioClip hitSfx;

    AudioSource sfxPlayer;
    public UnityEvent Hit               { get { return _Hit; } protected set { _Hit = value; } }

    protected override void Awake()
    {
        base.Awake();
        sfxPlayer = GameObject.Find("SFXPlayer").GetComponent<AudioSource>();
        Hit.AddListener(OnHit);
    }

    protected virtual void OnHit()
    {
        sfxPlayer.PlayOneShot(hitSfx);
    }
}

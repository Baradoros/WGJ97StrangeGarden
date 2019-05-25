using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeaspoonTools;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class SeedController : MonoBehaviour2D
{
    [SerializeField] UnityEvent _Hit;
    [SerializeField] UnityEvent _Gone;
    [SerializeField] AudioClip hitSfx;

    public static UnityAction<SeedController> AnyCreate { get; set; } = delegate {};

    AudioSource sfxPlayer;
    public UnityEvent Hit               { get { return _Hit; } protected set { _Hit = value; } }
    public UnityEvent Gone { get { return _Gone; } }

    protected override void Awake()
    {
        base.Awake();
        sfxPlayer = GameObject.Find("SFXPlayer").GetComponent<AudioSource>();
        Hit.AddListener(OnHit);
        AnyCreate.Invoke(this);
    }

    protected virtual void OnHit()
    {
        sfxPlayer.PlayOneShot(hitSfx);
    }

    protected virtual void OnDestroy()
    {
        this._Gone.Invoke();
    }
}

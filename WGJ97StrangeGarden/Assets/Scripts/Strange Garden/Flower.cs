using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeaspoonTools;
using UnityEngine.Events;

public class Flower : MonoBehaviour2D, ITimerHandler
{
    #region In the Inspector
    [SerializeField]
    int _points;

    [SerializeField]
    float _lifetime;

    #endregion

    #region Events
    public static UnityAction<Flower> AnyDie    { get; protected set; } = delegate {};

    #endregion

    #region Properties

    public int points                           { get { return _points; } }
    public float lifetime 
    { 
        get                                     { return _lifetime; } 
        protected set                           { _lifetime = value; }
    }

    public bool isAging                         { get; set; }

    /// <summary>
    /// The time this has left to live.
    /// </summary>
    public float timer                         { get; set; }
    #endregion

    #region Other Fields
    public static int amountActive              { get; protected set; }
    

    #endregion

    #region Methods
    protected override void Awake()
    {
        base.Awake();
        RefreshLifetime();
        isAging =                               true;
    }

    #region Main Functionality
    public void RefreshLifetime()
    {
        timer =                                 lifetime;
    }

    protected override void Update()
    {
        base.Update();

        if (isAging)
        {
            timer -=                            Time.deltaTime;
            if (timer <= 0)
                Die();
        }
    }

    public void Die()
    {
        AnyDie.Invoke(this);
        isAging =                               false;
        Destroy(this.gameObject);
    }

    #endregion

    #region Update Amount Active
    void Start()
    {
        amountActive++;
    }

    void OnDisable()
    {
        amountActive--;
    }

    void OnEnable()
    {
        amountActive++;
    }

    void OnDestroy()
    {
        amountActive--;
    }

    #endregion

    #endregion
}

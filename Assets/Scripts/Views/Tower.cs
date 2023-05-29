using CanasSource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public TowerModel model { get; private set; }
    public TowerStat stat { get; private set; }
    public Transform target { get; protected set; }
    public Cooldown attackCooldown { get; protected set; }
    private bool isStop;
    public void Init(TowerModel _model, TowerStat _stat)
    {
        model = _model;
        stat = _stat;
    }

    private void Update()
    {
        if (isStop) return;
        LogicUpdate(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (isStop) return;
        LogicUpdate(Time.fixedDeltaTime);
    }

    protected virtual void LogicUpdate(float deltaTime)
    {

    }

    protected virtual void PhysicUpdate(float deltaTime)
    {

    }
}

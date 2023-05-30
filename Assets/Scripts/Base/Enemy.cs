using CanasSource;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EnemyState
{
    idle,
    moveTo,
    moveBack,
    skill,
    die
}

public class EnemyStat
{
    public StatInt maxHP = new();
    public StatInt currentHP = new();
    public StatInt armor = new();
    public StatFloat moveSpeed = new();
    public StatInt coin = new();

    public EnemyStat(int _hp, int _armor, float _moveSpeed, int _coin)
    {
        maxHP.BaseValue = _hp;
        currentHP.BaseValue = _hp;
        armor.BaseValue = _armor;
        moveSpeed.BaseValue = _moveSpeed;
        coin.BaseValue = _coin;
    }
}

public abstract class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D theRB;
    //public HealthBar healthBar { get ; protected set; }
    public Transform target { get ; protected set; }
    public EnemyState state {  get; protected set; }
    public EnemyStat stat { get; private set; }
    public bool isAlive => stat.currentHP.Value > 0;
    public bool isFullHp => stat.currentHP.Value >= stat.maxHP.Value;

    public GameController gameController => Singleton<GameController>.Instance;

    private bool isStop;
    protected bool isPicked;

    private void Awake()
    {
        var testStat = new EnemyStat(10, 10, 10, 10);
        stat = testStat;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        theRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isStop) return;
        LogicUpdate(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (isStop) return;
        PhysicUpdate(Time.deltaTime);
    }

    protected virtual void LogicUpdate(float deltaTime)
    {
        
    }

    protected virtual void PhysicUpdate(float deltaTime)
    {

    }

    public void Init()
    {
        UpdateStat();
    }

    private void UpdateStat()
    {

    }

    public void TakeDamage(int dmg)
    {
        if (!isAlive) return;

        var lossHP = AddHp(-dmg);
        if (stat.currentHP.Value == 0)
        {
            Die();
        }

        SpawnEffectTakeDamage();
    }

    private int AddHp(int amount)
    {
        var remain = stat.currentHP.Value + amount;
        var newValue = Mathf.Clamp(remain, 0, stat.maxHP.Value);
        var oldValue = stat.currentHP.Value;
        stat.currentHP.AddModifier(new StatModifier<int>(newValue, StatModifierType.Add, this));
        return newValue - oldValue;
    }

    protected void Die()
    {
        anim?.SetTrigger("Die");
        PlayeSound(EnemyState.die);
        state = EnemyState.die;
        Singleton<GameController>.Instance.EnemyDie(this);
    }

    public void Stop()
    {
        isStop = true;
    }

    protected void SpawnEffectTakeDamage()
    {

    }

    protected void PlayeSound(EnemyState enemyState)
    {

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ConstFields.Food))
        {
            Debug.Log("Touched");
            target = collision.transform;
            //collision.transform.SetParent(transform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicTower : Tower
{
    public List<Transform> listEnemy = new();

    protected override void LogicUpdate(float deltaTime)
    {
        base.LogicUpdate(deltaTime);
        attackCooldown.Update(deltaTime);

        switch (state)
        {
            case TowerState.Idle:
                UpdateIdle();
                break;
            case TowerState.Attack:
                UpdateAttack(deltaTime);
                break;
        }
    }

    private void Awake()
    {
        Init(new TowerStat(10, 10, 1));
    }

    private void UpdateIdle()
    {
        if (target == null)
        {
            target = GetFirstEnemy();
        }
        if (target != null)
        {
            state = TowerState.Attack;
        }
    }

    private Transform GetFirstEnemy()
    {
        if(listEnemy.Count != 0)
        {
            return listEnemy.Where(enemy => enemy != null).First();
        }
        return null;
    }

    private void UpdateAttack(float deltaTime)
    {
        if (target == null)
        {
            state = TowerState.Idle;
            return;
        }

        var distance = Vector3.Distance(transform.position, target.position);
        if (distance > stat.atkRange.Value)
        {
            state = TowerState.Idle;
            target = null;
            return;
        }

        if (attackCooldown.isFinished)
        {
            attackCooldown.Restart(stat.atkSpeed.Value);
            Attack();
        }

        
    }

    private void Attack()
    {
        SpawnBullet();
    }

    public Bullet SpawnBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab, firePointPos.position, Quaternion.identity, firePointPos);
        bullet.Init(this, new BulletStat(stat.atk.Value, stat.ProjectileSpeed.Value), target);
        return bullet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            listEnemy.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            listEnemy.Remove(collision.transform);
        }
    }
}

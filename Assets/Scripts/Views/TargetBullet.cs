using CanasSource;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TargetBullet : Bullet
{
    private Cooldown despawnCooldown = new Cooldown();
    private Vector3 targetPos;

    public override void LogicUpdate(float deltaTime)
    {
        switch (state)
        {
            case BulletState.idle:
                {
                    UpdateIdle(deltaTime);
                    break;
                }
            case BulletState.move:
                {
                    UpdateMove(deltaTime);
                    break;
                }
            case BulletState.explose:
                {
                    UpdateExplose(deltaTime);
                    break;
                }
            case BulletState.despawn:
                {
                    UpdateDespawn(deltaTime);
                    break;
                }
        }
    }

    private void UpdateIdle(float deltaTime)
    {
        state = BulletState.move;
    }

    private void UpdateMove(float deltaTime)
    {
        if (target != null)
        {
            targetPos = target.transform.position;
        }
        
        Moving();

        if (bulletDirectionType == BulletDirectionType.lookAt)
        {
            Vector3 vectorToTarget = targetPos - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
            Quaternion quater = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, quater, deltaTime * 20f);
        }

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            state = BulletState.explose;
        }
    }

    private void Moving()
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        transform.Translate(stat.moveSpeed.Value * Time.deltaTime * direction, Space.World);
    }

    private void UpdateExplose(float deltaTime)
    {
        if(bulletExploseType == BulletExploseType.target)
        {
            ExploseTarget();
        }
        else if(bulletExploseType == BulletExploseType.single)
        {
            //ExploseSingle();
        }
        else if (bulletExploseType == BulletExploseType.aoe)
        {
            ExploseAoe(transform.position);
        }
        despawnCooldown.Restart(0);
        state = BulletState.despawn;
    }
    private void UpdateDespawn(float deltaTime)
    {
        despawnCooldown.Update(deltaTime);
        if (!despawnCooldown.isFinished)
            return;

        Destroy(gameObject);
    }

    public void ExploseAoe(Vector3 position)
    {
        
    }

    private void ExploseSingle(Enemy enemy)
    {
        enemy?.TakeDamage(stat.atk.Value);
    }

    private void ExploseTarget()
    {
        target?.TakeDamage(stat.atk.Value);
    }
}

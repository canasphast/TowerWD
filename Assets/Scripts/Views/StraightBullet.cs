using CanasSource;
using UnityEngine;

public class StraightBullet : Bullet
{
    private Vector3 startPosition;
    private Vector3 startVelocity;
    private Vector3 targetPosition;

    private Cooldown lifeTimeCooldown = new Cooldown();
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
        startPosition = transform.position;
        targetPosition = target.transform.position;

        if (bulletDirectionType == BulletDirectionType.lookAt)
        {
            Vector3 vectorToTarget = targetPos - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
            Quaternion quater = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, quater, deltaTime * 20f);
        }

        state = BulletState.move;
    }

    private void UpdateMove(float deltaTime)
    {
        var direction = (targetPosition - startPosition).normalized;
        transform.Translate(stat.moveSpeed.Value * Time.deltaTime * direction, Space.World);
        
        if(Vector3.Distance(transform.position, owner.transform.position) > owner.stat.atkRange.Value)
        {
            state = BulletState.explose;
        }
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

    private Vector3 CalculateProjectile(Vector2 startVelocity, Vector2 startPosition, float t)
    {
        float x = startVelocity.x * t;
        float y = startVelocity.y * t;
        return new Vector2((x + startPosition.x) , (y + startPosition.y));
    }

    private Vector2 GetStartVelocity(Vector2 startPos, Vector2 targetPos, float lifeTime)
    {
        float velocityY = (targetPos.y - startPos.y) / lifeTime;
        float velocityX = (targetPos.x - startPos.x) / lifeTime;
        return new Vector2(velocityX, velocityY);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy)/*collision.CompareTag(ConstFields.Food)*/)
        {
            ExploseSingle(enemy);
            state = BulletState.explose;
            //collision.transform.SetParent(transform);
        }
    }
}

using CanasSource;
using UnityEngine;

public class ParabolaBullet : Bullet
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
        var distance = Vector3.Distance(startPosition, targetPosition);
        var lifeTime = distance / 8f;
        startVelocity = GetStartVelocity(startPosition, targetPosition, lifeTime);
        lifeTimeCooldown.Restart(lifeTime * 1.5f);
        state = BulletState.move;
    }

    private void UpdateMove(float deltaTime)
    {
        lifeTimeCooldown.Update(deltaTime);

        Vector3 newPosition = CalculateProjectile(startVelocity, startPosition, lifeTimeCooldown.elapse);

        if (bulletDirectionType == BulletDirectionType.lookAt)
        {
            Vector3 direction = newPosition - transform.position;
            direction.Normalize();
            float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotationZ);
            transform.rotation = targetRotation;
        }
        
        transform.position = newPosition;

        if (lifeTimeCooldown.isFinished)
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
        float y = startVelocity.y * t + 0.5f * -9.81f * t * t;
        return new Vector2(x + startPosition.x, y + startPosition.y);
    }

    private Vector2 GetStartVelocity(Vector2 startPos, Vector2 targetPos, float lifeTime)
    {
        float velocityY = (targetPos.y - startPos.y - 0.5f * -9.81f * lifeTime * lifeTime) / lifeTime;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    private int index;

    protected override void LogicUpdate(float deltaTime)
    {
        base.LogicUpdate(deltaTime);
        if (!isAlive) return;
        switch(state)
        {
            case EnemyState.idle:
                UpdateIdle(deltaTime);
                break;
            case EnemyState.moveTo:
                UpdateMoveTo(deltaTime);
                break;
            case EnemyState.moveBack:
                UpdateMoveBack(deltaTime);
                break;
            case EnemyState.skill:
                UpdateSkill(deltaTime);
                break;

        }
    }

    private void UpdateIdle(float deltaTime)
    {
        state = EnemyState.moveTo;
    }

    private void UpdateMoveTo(float deltaTime)
    {
        var mapPoint = gameController.GetMapPoint(index);
        if(mapPoint == null)
        {
            theRB.velocity = Vector2.zero;
            return;
        }
        if(target != null)
        {
            mapPoint = target;
            if(Vector2.Distance(target.position, transform.position) < 0.1f)
            {
                state = EnemyState.moveBack;
                target.transform.SetParent(transform);
                index--;
                return;
            }
        }
        Moving(mapPoint);
        if (Vector2.Distance(mapPoint.position, transform.position) < 0.1f)
        {
            index++;
        }
        //if touching Food
        //state = EnemyState.moveBack;
    }

    private void UpdateMoveBack(float deltaTime)
    {
        var mapPoint = gameController.GetMapPoint(index);
        if (mapPoint == null)
        {
            theRB.velocity = Vector2.zero;
            return;
        }
        Moving(mapPoint);
        if (Vector2.Distance(mapPoint.position, transform.position) < 0.1f)
        {
            index--;
        }
    }

    private void Moving(Transform mapPoint)
    {
        Vector3 _direction = mapPoint.position - transform.position;
        theRB.velocity = new Vector2(_direction.x, _direction.y).normalized * stat.moveSpeed.Value;
    }

    private void UpdateSkill(float deltaTime)
    {

    }
}
;
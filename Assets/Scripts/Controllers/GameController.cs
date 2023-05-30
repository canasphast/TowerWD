using CanasSource;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Awake()
    {
        Singleton<GameController>.Instance = this;
    }

    public List<Transform> mapPoints = new();
    public List<Enemy> enemies = new();

    public string SetIdForEnemy()
    {
        //chua viet
        return "";
    }

    public void EnemyDie(Enemy enemy, bool isDestroyObject = true)
    {
        enemies.Remove(enemy);
        if(isDestroyObject) Destroy(enemy.gameObject);
    }

    public Transform GetMapPoint(int index)
    {
        if (mapPoints.Count > index && index >= 0)
        {
            return mapPoints[index];
            
        }
        return null;
    }
}

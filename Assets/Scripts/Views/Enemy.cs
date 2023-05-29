using CanasSource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class Enemy : MonoBehaviour
{
    public EnemyStat stat { get; private set; }
    public bool isAlive => stat.currentHP.Value > 0;
    public bool isFullHp => stat.currentHP.Value >= stat.maxHP.Value;
    private bool isStop;
}

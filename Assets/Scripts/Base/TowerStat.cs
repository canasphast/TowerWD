using CanasSource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStat
{
    public StatInt level = new();
    public StatInt exp = new();
    public StatInt atk = new();
    public StatFloat atkRange = new();
    public StatFloat atkSpeed = new();
    public StatFloat ProjectileSpeed = new();
    public StatFloat ProjectileCount = new();

    public TowerStat(int _level, int _exp, int _atk, float _atkRange, float _atkSpeed, float _PS, float _PC)
    {
        level.BaseValue = _level;
        exp.BaseValue = _exp;
        atk.BaseValue = _atk;
        atkRange.BaseValue = _atkRange;
        atkSpeed.BaseValue = _atkSpeed;
        ProjectileSpeed.BaseValue = _PS;
        ProjectileCount.BaseValue = _PC;
    }
}

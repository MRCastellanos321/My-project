using UnityEngine;

public class Fantasma : characterInterface
{
    private int attack = 2;
    private int turnsPassed = 0;
    private int attackCoolDown = 0;
    private int collectedShards = 0;
    private int skillCoolDown = 0;
    public int GetAttack()
    {
        return attack;
    }

    public int GetTurnsPassed()
    {
        return turnsPassed;
    }
    public void SetTurnsPassed(int number)
    {
        turnsPassed += number;
    }

    public int GetAttackCoolDown()
    {
        return attackCoolDown;
    }
    public void SetAttackCoolDown(int number)
    {
        attackCoolDown += number;
    }

    public int GetCollectedShards()
    {
        return collectedShards;
    }

    public void SetCollectedShards(int number)
    {
        collectedShards += number;
    }
    public int GetSkillCoolDown()
    {
        return skillCoolDown;
    }
    public void SetSkillCoolDown(int number)
    {
        skillCoolDown += number;
    }

}

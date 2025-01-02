using UnityEngine;

public interface characterInterface
{
    public int GetAttack();
    public int GetTurnsPassed();
    public void SetTurnsPassed(int number);
    public int GetAttackCoolDown();
    public void SetAttackCoolDown(int number);
    public int GetCollectedShards();
    public void SetCollectedShards(int number);
    public int GetSkillCoolDown();
    public void SetSkillCoolDown(int number);
}


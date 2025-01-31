using UnityEngine;
namespace Tablero
{
    public class Dragon : characterInterface
    {
        private int turnsPassed = 0;
        private int attackCoolDown = 0;
        private int collectedShards = 0;
        private int skillCoolDown = 0;
        private int attackInmunity = 0;
        private int trapInmunity = 0;
        private int doorKeysCollected = 0;
        private int mazeVisibility = 0;
        private int diceEffect = 0;
        private int positionVisibility = 0;

        public int GetTrapInmunity()
        {
            return trapInmunity;
        }
        public void SetTrapInmunity(int value)
        {
            trapInmunity += value;
        }
        public int GetAttackInmunity()
        {
            return attackInmunity;
        }
        public void SetAttackInmunity(int value)
        {
            attackInmunity += value;
        }

        public int GetAttack()
        {
            return 2;
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
            if (collectedShards == 3)
            {
                Manager.Instancia.TurnInHuman();
            }
        }
        public int GetSkillCoolDown()
        {
            return skillCoolDown;
        }
        public void SetSkillCoolDown(int number)
        {
            skillCoolDown += number;
        }
        public void SetCollectedKeys(int value)
        {
            doorKeysCollected += value;
        }
        public int GetCollectedKeys()
        {
            return doorKeysCollected;
        }
        public int GetMazeVisibility()
        {
            return mazeVisibility;
        }
        public void SetMazeVisibility(int value)
        {
            mazeVisibility += value;
        }
        public int GetDiceEffect()
        {
            return diceEffect;
        }
        public void SetDiceEffect(int value)
        {
            diceEffect += value;
        }
          public int GetPositionVisibility()
        {
            return positionVisibility;
        }
        public void SetPositionVisibility(int value)
        {
            positionVisibility += value;
        }

        public void Skill()
        {
            if (skillCoolDown == 0)
            {
                Manager.diceNumber *= 2;
                skillCoolDown += 7;
                Manager.ChangeMessage("Has duplicado tus movimientos!", Manager.Instancia.skillEffectText);
            }
        }
    }
}

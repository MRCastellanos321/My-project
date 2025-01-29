namespace Tablero
{
    public class Vampiro : characterInterface
    {
        private int attack = 2;
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
            diceEffect = +value;
        }
        public int GetPositionVisibility()
        {
            return positionVisibility;
        }
        public void SetPositionVisibility(int value)
        {
            positionVisibility = +value;
        }

        public void Skill()
        {
            // attack inmunity funciona segun los ataques que recibe y no los turnos. A los jugadores se les aumenta
            //el attack cooldown incluso aunque no lo hayan podido atacar
            attackInmunity += 2;
            skillCoolDown += 3;
            Manager.ChangeMessage("Inmunidad a ataques: 2 oportunidades", Manager.Instancia.skillEffectText);
        }
    }

}

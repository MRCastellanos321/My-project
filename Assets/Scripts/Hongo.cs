
namespace Tablero
{
    public class Hongo : characterInterface
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
            bool canAttack = false;
            //Revisa las posiciones en un radio de 4 casillas desde el hongo e incapacita a todos los jugadores "atacables" que encuentre
            int f = Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][0] - 4;
            int c = Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][1] - 4;

            while (f <= Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][0] + 4)
            {
                while (c <= Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][1] + 4)
                {
                    for (int i = 0; i < Manager.FilasColumnas.Length; i++)
                    {
                        if (f == Manager.FilasColumnas[i][0] && c == Manager.FilasColumnas[i][1] && i != Manager.Instancia.currentPlayerIndex - 1)
                        {
                            if (Manager.playersType[i].GetTurnsPassed() == 0 && Manager.playersType[i].GetAttackInmunity() == 0)
                            {
                                canAttack = true;
                                Manager.playersType[i].SetTurnsPassed(2);
                            }
                        }
                    }
                    c++;
                }
                c = 0;
                f++;
            }

            //no debe importar si f y c se salen del tablero porque no las estoy usando para acceder a nada, simplemente la comprobacion no va a dar igual
            if (canAttack)
            {
                skillCoolDown += 5;
                Manager.ChangeMessage("Atacaste los jugadores!", Manager.Instancia.skillEffectText);
            }
            else
            {
                Manager.ChangeMessage("No hay jugadores atacables", Manager.Instancia.skillEffectText);
            }

        }
        //no hay que analizar si skillcooldown es 0 pq en ese caso el boton simplemente no va a aparecer
    }
}
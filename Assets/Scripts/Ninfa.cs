
namespace Tablero
{
    public class Ninfa : characterInterface
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
        public void Skill()
        {
            //Revisa las posiciones en un radio de 2 casillas y les roba un shard
            bool canSteal = false;
            int f = Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][0] - 2;
            int c = Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][1] - 2;

            while (f <= Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][0] + 2)
            {
                while (c <= Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][1] + 2)
                {
                    for (int i = 0; i < Manager.FilasColumnas.Length; i++)
                    {
                        if (f == Manager.FilasColumnas[i][0] && c == Manager.FilasColumnas[i][1] && i != Manager.Instancia.currentPlayerIndex - 1 && !Manager.Instancia.Human[i])
                        {
                            if (Manager.playersType[i].GetCollectedShards() != 0)
                            {
                                canSteal = true;
                                Manager.playersType[i].SetCollectedShards(-1);
                                if (collectedShards < 3)
                                {
                                    collectedShards++;
                                }
                            }
                        }
                    }
                    c++;
                }
                c = 0;
                f++;
            }

            //no debe importar si f y c se salen del tablero porque no las estoy usando para acceder a nada, simplemente la comprobacion no va a dar igual
            if (canSteal == true)
            {
                skillCoolDown += 5;
                Manager.ChangeMessage("Has robado fragmentos a los jugadores!", Manager.Instancia.skillEffectText);
                if (collectedShards == 3)
                {
                    Manager.Instancia.TurnInHuman();
                }
            }
            else
            {
                Manager.ChangeMessage("No has podido robar fragmentos", Manager.Instancia.skillEffectText);
            }

        }
    }
}
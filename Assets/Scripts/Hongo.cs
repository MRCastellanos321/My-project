using UnityEngine;
namespace Tablero
{
    public class Hongo : characterInterface
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
            turnsPassed = +number;
        }

        public int GetAttackCoolDown()
        {
            return attackCoolDown;
        }
        public void SetAttackCoolDown(int number)
        {
            attackCoolDown = +number;
        }

        public int GetCollectedShards()
        {
            return collectedShards;
        }
        public void SetCollectedShards(int number)
        {
            collectedShards = +number;
        }
        public int GetSkillCoolDown()
        {
            return skillCoolDown;
        }
        public void SetSkillCoolDown(int number)
        {
            skillCoolDown = +number;
        }

        public void HongoSkill()
        {

            //Revisa las posiciones en un radio de 4 casillas desde el hongo e incapacita a todos los jugadores "atacables" que encuentre
            if (skillCoolDown == 0)
            {
                bool canAttack = false;
                int f = Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][0] - 4;
                int c = Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][1] - 4;

                while (f <= Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][0] + 4)
                {
                    while (c <= Manager.FilasColumnas[Manager.Instancia.currentPlayerIndex - 1][1] + 4)
                    {
                        for (int i = 0; i < Manager.FilasColumnas.Length; i++)
                        {
                            if (f == Manager.FilasColumnas[i][0] && c == Manager.FilasColumnas[i][1])
                            {
                                if (Manager.playersType[i].GetTurnsPassed() == 0)

                                    canAttack = true;
                                Manager.playersType[i].SetTurnsPassed(2);
                            }
                            i++;
                        }
                        c++;
                    }
                    c = 0;
                    f++;
                }

                //no debe importar si f y c se salen del tablero porque no las estoy usando para acceder a nada, simplemente la comprobacion no va a dar igual
                if (canAttack == true)
                {
                    skillCoolDown += 10;
                }
            }
        }
    }
}
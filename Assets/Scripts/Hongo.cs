using UnityEngine;
namespace Tablero
{
    public class Hongo : MonoBehaviour
    {
        public static int attack = 2;

        public static int turnsPassed;

        public static int attackCoolDown;

        public static int skillCoolDown;

        public static int collectedShards;


        public void HongoSkill()
        {

            //Revisa las posiciones en un radio de 4 casillas desde el hongo e incapacita a todos los jugadores "atacables" que encuentre
            if (skillCoolDown == 0)
            {
                bool canAttack = false;
                int f = Manager.FilasColumnas[Manager.currentPlayerIndex - 1][0] - 4;
                int c = Manager.FilasColumnas[Manager.currentPlayerIndex - 1][1] - 4;

                while (f <= Manager.FilasColumnas[Manager.currentPlayerIndex - 1][0] + 4)
                {
                    while (c <= Manager.FilasColumnas[Manager.currentPlayerIndex - 1][1] + 4)
                    {
                        for (int i = 0; i < Manager.FilasColumnas.Length; i++)
                        {
                            if (f == Manager.FilasColumnas[i][0] && c == Manager.FilasColumnas[i][1])
                            {
                                if (i == 1 && Bruja.turnsPassed == 0)
                                {
                                    canAttack = true;
                                    Bruja.turnsPassed += 2;
                                }
                                else if (i == 2 && Fantasma.turnsPassed == 0)
                                {
                                    canAttack = true;
                                    Fantasma.turnsPassed += 2;
                                }
                                else if (i == 4 && Ninfa.turnsPassed == 0)
                                {
                                    canAttack = true;
                                    Ninfa.turnsPassed += 2;
                                }
                                i++;
                            }
                        }
                        c++;
                    }
                    c = 0;
                    f++;
                    //no debe importar si f y c se salen del tablero porque no las estoy usando para acceder a nada, simplemente la comprobacion no va a dar igual
                }

                if (canAttack == true)
                {
                    skillCoolDown += 10;
                }
            }
        }
    }
}
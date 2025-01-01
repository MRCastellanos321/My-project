using Unity.VisualScripting;
using UnityEngine;
namespace Tablero
{
   public class Bruja : MonoBehaviour
   {
      public static int turnsPassed;

      public static int attack = 2;

      public static int attackCoolDown;
      public static int skillCoolDown;

      public static int collectedShards;

       public void BrujaSkill()
        {
            if (Bruja.skillCoolDown == 0)
            {
                Manager.diceNumber = Manager.diceNumber * 2;
                Bruja.skillCoolDown = 7;

            }
        }
   }
}

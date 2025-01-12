using Tablero;
using UnityEngine;
public class CollectibleSkillBoost : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            if (Manager.playersType[Manager.Instancia.currentPlayerIndex - 1].GetSkillCoolDown() > 0 && !Manager.Instancia.Human[Manager.Instancia.currentPlayerIndex - 1])
            {
                Manager.playersType[Manager.Instancia.currentPlayerIndex - 1].SetSkillCoolDown(-1);
                Destroy(gameObject);
            }
        }
    }
}


using Tablero;
using UnityEngine;
public class CollectibleAttackBoost : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            if (Manager.playersType[Manager.Instancia.currentPlayerIndex - 1].GetAttackCoolDown() > 0)
            {
                Manager.playersType[Manager.Instancia.currentPlayerIndex - 1].SetAttackCoolDown(-1);
                Destroy(gameObject);
            }
        }
    }
}

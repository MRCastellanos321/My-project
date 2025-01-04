using Tablero;
using UnityEngine;

public class CollectibleShard : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            Debug.Log("colisione");
            if (Manager.playersType[Manager.Instancia.currentPlayerIndex - 1].GetCollectedShards() != 3)
            {
                Manager.playersType[Manager.Instancia.currentPlayerIndex - 1].SetCollectedShards(1);            
                Destroy(gameObject);

            }
        }
    }
}

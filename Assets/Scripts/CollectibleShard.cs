using Tablero;
using TMPro;
using UnityEngine;

public class CollectibleShard : MonoBehaviour
{

    public TextMeshProUGUI ShardCollectionText;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            Debug.Log("colisione");
            if (Manager.playersType[Manager.Instancia.currentPlayerIndex - 1].GetCollectedShards() != 3)
            {
                Manager.playersType[Manager.Instancia.currentPlayerIndex - 1].SetCollectedShards(1);
                MessageManager.MessageShowing(ShardCollectionText, "Tienes"  + Manager.playersType[Manager.Instancia.currentPlayerIndex - 1].GetCollectedShards());
                Destroy(gameObject);
            }
        }
    }
}

using Tablero;
using UnityEngine;

public class CollectibleShard : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            Debug.Log("colisione");
            if (Manager.playersType[Manager.currentPlayerIndex - 1] == "Vampiro" && Vampiro.collectedShards != 3)
            {
                Vampiro.collectedShards++;
                Destroy(gameObject);
            }

            if (Manager.playersType[Manager.currentPlayerIndex - 1] == "Bruja" && Bruja.collectedShards != 3)
            {
                Bruja.collectedShards++;
                Destroy(gameObject);
            }

            if (Manager.playersType[Manager.currentPlayerIndex - 1] == "Fantasma" && Fantasma.collectedShards != 3)
            {
                Fantasma.collectedShards++;
                Destroy(gameObject);
            }

            if (Manager.playersType[Manager.currentPlayerIndex - 1] == "Hongo" && Hongo.collectedShards != 3)
            {
                Hongo.collectedShards++;
                Destroy(gameObject);
            }

            if (Manager.playersType[Manager.currentPlayerIndex - 1] == "Ninfa" && Ninfa.collectedShards != 3)
            {
                Ninfa.collectedShards++;
                Destroy(gameObject);
            }
        }


    }
}

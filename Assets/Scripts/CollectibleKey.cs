using Tablero;
using UnityEngine;
public class CollectibleKey : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            Debug.Log("colisione");
            Manager.playersType[Manager.Instancia.currentPlayerIndex - 1].SetCollectedKeys(1);
            Destroy(gameObject);
        }
    }
}

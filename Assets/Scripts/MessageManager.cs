using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Tablero
{
    public class MessageManager : MonoBehaviour
    {
         //Start is called once before the first execution of Update after the MonoBehaviour is created

        static TextMeshPro[] textObject;

        void Start()
        {
            textObject[0] = GameObject.Find("Text1").GetComponent<TextMeshPro>();
            textObject[1] = GameObject.Find("Text2").GetComponent<TextMeshPro>();
            textObject[2] = GameObject.Find("Text3").GetComponent<TextMeshPro>();
            textObject[2] = GameObject.Find("Text4").GetComponent<TextMeshPro>();


            MessageShowing(false, 0);
            MessageShowing(false, 1);
            MessageShowing(false, 2);
            MessageShowing(false, 3);
        }

        public void MessageShowing(bool show, int currentPlayerMessage)
        {
            textObject[currentPlayerMessage].gameObject.SetActive(show);
        }


        public void ChangeMessage(string message, int currentPlayerMessage)
        {
            MessageShowing(true, currentPlayerMessage);
            textObject[currentPlayerMessage].text = message;
        }


        // Update is called once per frame
        void Update()
        {
            if (Manager.diceNumber == 0)
            {
                ChangeMessage("Presiona espacio para pasar el turno", Manager.currentPlayerIndex - 1);
            }
        }
    }
}
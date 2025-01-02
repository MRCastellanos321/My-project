
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
namespace Tablero
{
    public class MessageManager : MonoBehaviour
    {
        //Start is called once before the first execution of Update after the MonoBehaviour is created

        //static TextMeshProUGUI[] textObject;
        public static TextMeshProUGUI changeTurnText;

        public static TextMeshProUGUI validAttackText;


        void Start()
        {

            changeTurnText.gameObject.SetActive(false);
            validAttackText.gameObject.SetActive(false);
        }


        public static void MessageShowing(bool show, TextMeshProUGUI textObject)
        {
            textObject.gameObject.SetActive(show);
        }

        public static void ChangeMessage(string message, TextMeshProUGUI textObject)
        {
            textObject.gameObject.SetActive(true);
            textObject.text = message;
        }

        // Update is called once per frame
        void Update()
        {
            if (Manager.diceNumber == 0)
            {
                ChangeMessage("Presiona espacio para pasar el turno", changeTurnText);
            }
            else
            {
                MessageShowing(false, changeTurnText);
            }

        }
    }
}
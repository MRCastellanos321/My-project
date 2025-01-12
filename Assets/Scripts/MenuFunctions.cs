using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
public class MenuFunctions : MonoBehaviour
{
  //Lista de todos los posibles sprite, son 4 diferentes porque cada una tiene un numero indicando el jugador
  public List<Sprite> skins1 = new List<Sprite>();
  public List<Sprite> skins2 = new List<Sprite>();

  public List<Sprite> skins3 = new List<Sprite>();

  public List<Sprite> skins4 = new List<Sprite>();

  //Los preview del jugador que van a aparecer en pantalla
  public Image canvasImage1;
  public Image canvasImage2;
  public Image canvasImage3;
  public Image canvasImage4;

  //El int que va a dar la posicion en la lista donde esta el sprite seleccionado para cada jugador
  public static int selectedType1;
  public static int selectedType2;
  public static int selectedType3;
  public static int selectedType4;

  public static bool EnteredMenu = false;
  //para obligar a a que la primera escena sea el menu

  public TextMeshProUGUI CharacterInfoText1;
  public TextMeshProUGUI CharacterInfoText2;
  private List<string> InfoTexts;


  public void NextButton1()
  {
    selectedType1++;
    if (selectedType1 == skins1.Count)
    {
      selectedType1 = 0;
    }
    canvasImage1.sprite = skins1[selectedType1];
    CharacterInfoText1.gameObject.SetActive(false);
    CharacterInfoText2.gameObject.SetActive(false);
  }
  public void BackButton1()
  {
    selectedType1 -= 1;
    if (selectedType1 == -1)
    {
      selectedType1 = skins1.Count - 1;
    }
    canvasImage1.sprite = skins1[selectedType1];
    CharacterInfoText1.gameObject.SetActive(false);
    CharacterInfoText2.gameObject.SetActive(false);
  }

  public void NextButton2()
  {
    selectedType2++;
    if (selectedType2 == skins2.Count)
    {
      selectedType2 = 0;
    }
    canvasImage2.sprite = skins2[selectedType2];
    CharacterInfoText1.gameObject.SetActive(false);
    CharacterInfoText2.gameObject.SetActive(false);
  }

  public void BackButton2()
  {
    selectedType2 -= 1;
    if (selectedType2 == -1)
    {
      selectedType2 = skins2.Count - 1;
    }
    canvasImage2.sprite = skins2[selectedType2];
    CharacterInfoText1.gameObject.SetActive(false);
    CharacterInfoText2.gameObject.SetActive(false);
  }

  public void NextButton3()
  {
    selectedType3++;
    if (selectedType3 == skins3.Count)
    {
      selectedType3 = 0;
    }
    canvasImage3.sprite = skins3[selectedType3];
    CharacterInfoText1.gameObject.SetActive(false);
    CharacterInfoText2.gameObject.SetActive(false);
  }

  public void BackButton3()
  {
    selectedType3 -= 1;
    if (selectedType3 == -1)
    {
      selectedType3 = skins3.Count - 1;
    }
    canvasImage3.sprite = skins3[selectedType3];
    CharacterInfoText1.gameObject.SetActive(false);
    CharacterInfoText2.gameObject.SetActive(false);
  }

  public void NextButton4()
  {
    selectedType4++;
    if (selectedType4 == skins4.Count)
    {
      selectedType4 = 0;
    }
    canvasImage4.sprite = skins4[selectedType4];
    CharacterInfoText1.gameObject.SetActive(false);
    CharacterInfoText2.gameObject.SetActive(false);
  }

  public void BackButton4()
  {
    selectedType4 -= 1;
    if (selectedType4 == -1)
    {
      selectedType4 = skins4.Count - 1;
    }
    canvasImage4.sprite = skins4[selectedType4];
    CharacterInfoText1.gameObject.SetActive(false);
    CharacterInfoText2.gameObject.SetActive(false);
  }

  public void InfoButton1()
  {
    CharacterInfoText2.gameObject.SetActive(false);
    CharacterInfoText1.text = InfoTexts[selectedType1];
    CharacterInfoText1.gameObject.SetActive(true);
  }
  public void InfoButton2()
  {
    CharacterInfoText1.gameObject.SetActive(false);
    CharacterInfoText2.text = InfoTexts[selectedType2];
    CharacterInfoText2.gameObject.SetActive(true);
  }
  public void InfoButton3()
  {
    CharacterInfoText2.gameObject.SetActive(false);
    CharacterInfoText1.text = InfoTexts[selectedType3];
    CharacterInfoText1.gameObject.SetActive(true);
  }
  public void InfoButton4()
  {
    CharacterInfoText1.gameObject.SetActive(false);
    CharacterInfoText2.text = InfoTexts[selectedType4];
    CharacterInfoText2.gameObject.SetActive(true);
  }


  public void StartButton()
  {
    SceneManager.LoadScene("GameScene");
  }
  void Start()
  {
    EnteredMenu = true;
    selectedType1 = 0;
    selectedType2 = 0;
    selectedType3 = 0;
    selectedType4 = 0;

    canvasImage1.sprite = skins1[selectedType1];
    canvasImage2.sprite = skins2[selectedType2];
    canvasImage3.sprite = skins3[selectedType3];
    canvasImage4.sprite = skins4[selectedType4];

    InfoTexts = new List<string>();
    InfoTexts.Add("Vampiro:Ser casi inmortal tiene sus ventajas: Dos oportunidades de evadir ataques");
    InfoTexts.Add("Bruja:Con su magia, puede teletransportarse a una casilla al azar");
    InfoTexts.Add("Fantasma:Es muy dificil de detectar!: Dos oportunidades de evadir trampas");
    InfoTexts.Add("Hongo viviente:Libera esporas venenosas que le permiten un ataque a distancia");
    InfoTexts.Add("Ninfa:Esta astuta ladrona puede robar shards a los jugadores cercanos");
    InfoTexts.Add("Dragon:Es mucho mas facil moverte cuando tiene alas! Puede duplicar su tirada de dados");
    CharacterInfoText1.gameObject.SetActive(false);
    CharacterInfoText1.gameObject.SetActive(false);
  }

}


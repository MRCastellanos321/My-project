using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Generic;
public class MenuFunctionsPrueba : MonoBehaviour
{
   

//el "component"que hace el sprite del objeto visible
  public SpriteRenderer sr;
  

//Lista de todos los posibles sprite
  public List<Sprite> skins = new List<Sprite>();
  
//Los GameObject que van a aparecer en pantalla
  public GameObject playerSkin;
 

//El int que va a dar la posicion en la lista donde esta el sprite seleccionado para cada jugador
  private int selectedSkin = 0;

  public string AssetPath;
  


  public void NextButton()
  {
    selectedSkin++;
    if (selectedSkin == skins.Count)
    {
      selectedSkin = 0;
    }

    sr.sprite = skins[selectedSkin];

  }
  public void BackButton1()
  {
    selectedSkin = selectedSkin - 1;
    if (selectedSkin == -1)
    {
      selectedSkin = skins.Count - 1;
    }

    sr.sprite = skins[selectedSkin];

  }

  
 // "Assets/Prefabs/selectedSkin.prefab"
  public void StartButton()
  {
    PrefabUtility.SaveAsPrefabAsset(playerSkin, AssetPath);
    SceneManager.LoadScene("SampleScene");
   // Manager.player1Type = selectedSkin + 1;
  }
  void Start()
  {
    //playerSkin = skins[selectedSkin].SpriteGetComponent<SpriteRenderer>().sprite;
  }
}



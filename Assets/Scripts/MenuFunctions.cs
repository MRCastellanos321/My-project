using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using Tablero;


public class MenuFunctions : MonoBehaviour
{
  //public GameObject selectedType;
  // public GameObject player1;

  //private Sprite player1sprite;

  
  public SpriteRenderer sr;
  public List<Sprite> skins = new List<Sprite>();
  public GameObject playerSkin;

  
  private int selectedSkin = 0;
  

  public void NextButton()
  {
    selectedSkin ++;
    if (selectedSkin == skins.Count)
    {
      selectedSkin = 0;
    }

    sr.sprite = skins[selectedSkin];

  }
  public void BackButton()
  {
    selectedSkin = selectedSkin - 1;
    if (selectedSkin == -1)
    {
      selectedSkin = skins.Count - 1;
    }

    sr.sprite = skins[selectedSkin];

  }
  public void StartButton()
  {
    PrefabUtility.SaveAsPrefabAsset(playerSkin, "Assets/Prefabs/selectedSkin.prefab");
    SceneManager.LoadScene("SampleScene");
    Manager.player1Type = selectedSkin + 1;
  }
  void Start()
  {
    //playerSkin = skins[selectedSkin].SpriteGetComponent<SpriteRenderer>().sprite;

  }
}

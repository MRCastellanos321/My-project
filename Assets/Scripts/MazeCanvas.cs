using Tablero;
using UnityEngine;
using UnityEngine.UI;

public class MazeCanvas : MonoBehaviour
{
    public GameObject imagePrefab;
    public Transform canvas;

    void Start()
    {
        var laberinto = Laberinto.ElLaberinto;
        CreateMazeGuideCanvas(laberinto);
    }
    void CreateMazeGuideCanvas(Laberinto laberinto)
    {
    float size = 2f;

        for (int i = 0; i < laberinto.Lado(); i++)
        {
            for (int j = 0; j < laberinto.Lado(); j++)
            {
                GameObject newImage = Instantiate(imagePrefab, canvas);
                RectTransform rt = newImage.GetComponent<RectTransform>();

                // +161 y =231 funcionan para 3 pixeles
                rt.anchoredPosition = new Vector2(j * size + 211, -i * size + 232); // a medida que aumentan las filas de la matriz, la coordenada Y diminuye, por eso el -i
                rt.sizeDelta = new Vector2(size, size);
                Image img = newImage.GetComponent<Image>();
                img.color = laberinto.Leer(i, j) == 2 ? Color.black : Color.white;
            }
        }
    }
}

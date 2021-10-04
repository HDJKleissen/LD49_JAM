using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceFloorTile : MonoBehaviour
{
    Material tileMaterial;
    Light tileLight;
    // Start is called before the first frame update
    void Start()
    {
        tileLight = GetComponent<Light>();
        tileMaterial = GetComponent<Renderer>().material;
        tileMaterial.EnableKeyword("_EMISSION");
        Color randomColor = RandomColor();
        tileMaterial.SetColor("_EmissionColor", randomColor);
        tileLight.color = randomColor;
        StartCoroutine(UnceUnceUnce());
    }

    IEnumerator UnceUnceUnce()
    {
        while (true)
        {
            Color randomColor = RandomColor();
            tileMaterial.SetColor("_EmissionColor", randomColor);
            tileLight.color = randomColor;
            yield return new WaitForSeconds(0.4348f); // 138bpm
        }
    }

    Color RandomColor()
    {
        return Color.HSVToRGB(Random.Range(0, 1f), 1, 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arreglarTubos : MonoBehaviour
{
    // Referencia al nuevo material verde (para los tubos reparados)
    public Material materialVerde;

    // Esta función se llama desde el botón para cambiar el material de los tubos rotos
    public void CambiarTubosRotos()
    {
        // Encontrar todos los objetos con la etiqueta "TuboRoto"
        GameObject[] tubosRotos = GameObject.FindGameObjectsWithTag("TuboRoto");

        // Cambiar el material de cada tubo roto al material verde
        foreach (GameObject tubo in tubosRotos)
        {
            Renderer renderer = tubo.GetComponent<Renderer>();

            if (renderer != null)
            {
                // Cambiar el material al nuevo material verde
                renderer.material = materialVerde;
            }
        }

        Debug.Log("Material de tubos rotos cambiado a verde");
    }
}


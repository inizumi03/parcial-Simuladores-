using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificarEstadoTubos : MonoBehaviour
{
    public Material materialVerde;            // Material verde que indica que el tubo está reparado
    public List<GameObject> objetosParaCambiar; // Lista de objetos cuyo material cambiará cuando todos los tubos estén reparados
    public Material nuevoMaterial;            // Material a aplicar cuando todos los tubos estén correctos

    private float anguloTolerancia = 1f;      // Ángulo de tolerancia para la verificación de rotación

    // Función que se ejecuta al presionar el botón de Canvas para verificar los tubos
    public void VerificarTubos()
    {
        // Verificar si todos los tubos rotos han sido reparados y si no hay tubos para destruir
        if (TodosLosTubosReparados() && !HayTubosParaDestruir())
        {
            CambiarMaterialObjetos();
        }
        else
        {
            Debug.Log("Algunos tubos no están reparados o todavía hay tubos para destruir.");
        }
    }

    // Función que cambia el material de todos los objetos en la lista
    void CambiarMaterialObjetos()
    {
        foreach (GameObject objeto in objetosParaCambiar)
        {
            Renderer renderer = objeto.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = nuevoMaterial;
                Debug.Log("Material cambiado en objeto: " + objeto.name);
            }
        }
    }

    // Función que verifica si todos los tubos rotar tienen el material y la rotación correctos
    bool TodosLosTubosReparados()
    {
        // Encontrar todos los tubos rotar dinámicamente
        GameObject[] tubosRotar = GameObject.FindGameObjectsWithTag("TuboRotar");

        foreach (GameObject tubo in tubosRotar)
        {
            Renderer renderer = tubo.GetComponent<Renderer>();

            // Verificar si el tubo tiene el material verde y está rotado correctamente
            if (renderer != null)
            {
                if (renderer.material != materialVerde || !EstaRotadoCorrectamente(tubo.transform))
                {
                    // Si algún tubo no cumple con la condición, retornar falso
                    return false;
                }
                else
                {
                    // Cambiar la etiqueta a "tubo" cuando esté reparado y rotado correctamente
                    tubo.tag = "tubo";
                }
            }
        }

        // Si todos los tubos cumplen las condiciones, retornar verdadero
        return true;
    }

    // Función que verifica si un tubo está rotado correctamente (aproximadamente 90 grados)
    bool EstaRotadoCorrectamente(Transform tuboTransform)
    {
        float anguloY = tuboTransform.localEulerAngles.y;
        return Mathf.Abs(anguloY - 90f) < anguloTolerancia;
    }

    // Función que verifica si hay tubos con la etiqueta "TuboDestruir" en la escena
    bool HayTubosParaDestruir()
    {
        GameObject[] tubosDestruir = GameObject.FindGameObjectsWithTag("TuboDestruir");
        return tubosDestruir.Length > 0;
    }
}

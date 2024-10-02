using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificarEstadoTubos : MonoBehaviour
{
    public Material materialVerde;            // Material verde que indica que el tubo est� reparado
    public List<GameObject> objetosParaCambiar; // Lista de objetos cuyo material cambiar� cuando todos los tubos est�n reparados
    public Material nuevoMaterial;            // Material a aplicar cuando todos los tubos est�n correctos

    private float anguloTolerancia = 1f;      // �ngulo de tolerancia para la verificaci�n de rotaci�n

    // Funci�n que se ejecuta al presionar el bot�n de Canvas para verificar los tubos
    public void VerificarTubos()
    {
        // Verificar si todos los tubos rotos han sido reparados y si no hay tubos para destruir
        if (TodosLosTubosReparados() && !HayTubosParaDestruir())
        {
            CambiarMaterialObjetos();
        }
        else
        {
            Debug.Log("Algunos tubos no est�n reparados o todav�a hay tubos para destruir.");
        }
    }

    // Funci�n que cambia el material de todos los objetos en la lista
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

    // Funci�n que verifica si todos los tubos rotar tienen el material y la rotaci�n correctos
    bool TodosLosTubosReparados()
    {
        // Encontrar todos los tubos rotar din�micamente
        GameObject[] tubosRotar = GameObject.FindGameObjectsWithTag("TuboRotar");

        foreach (GameObject tubo in tubosRotar)
        {
            Renderer renderer = tubo.GetComponent<Renderer>();

            // Verificar si el tubo tiene el material verde y est� rotado correctamente
            if (renderer != null)
            {
                if (renderer.material != materialVerde || !EstaRotadoCorrectamente(tubo.transform))
                {
                    // Si alg�n tubo no cumple con la condici�n, retornar falso
                    return false;
                }
                else
                {
                    // Cambiar la etiqueta a "tubo" cuando est� reparado y rotado correctamente
                    tubo.tag = "tubo";
                }
            }
        }

        // Si todos los tubos cumplen las condiciones, retornar verdadero
        return true;
    }

    // Funci�n que verifica si un tubo est� rotado correctamente (aproximadamente 90 grados)
    bool EstaRotadoCorrectamente(Transform tuboTransform)
    {
        float anguloY = tuboTransform.localEulerAngles.y;
        return Mathf.Abs(anguloY - 90f) < anguloTolerancia;
    }

    // Funci�n que verifica si hay tubos con la etiqueta "TuboDestruir" en la escena
    bool HayTubosParaDestruir()
    {
        GameObject[] tubosDestruir = GameObject.FindGameObjectsWithTag("TuboDestruir");
        return tubosDestruir.Length > 0;
    }
}

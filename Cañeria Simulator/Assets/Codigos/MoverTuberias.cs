using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverTuberias : MonoBehaviour
{
    // Variable para controlar si la rotación está habilitada
    private bool rotacionHabilitada = false;

    void Update()
    {
        // Solo permitir la rotación si la variable está habilitada
        if (rotacionHabilitada)
        {
            // Detectar si se ha hecho clic con el botón izquierdo del mouse
            if (Input.GetMouseButtonDown(0))
            {
                // Crear un rayo desde la cámara hacia donde está apuntando el mouse
                Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                // Verificar si el rayo golpea un objeto
                if (Physics.Raycast(rayo, out hitInfo))
                {
                    // Verificar si el objeto tiene la etiqueta "Tubo"
                    if (hitInfo.transform.CompareTag("tubo"))
                    {
                        // Obtener la rotación actual del objeto en el eje Y
                        float rotacionActualY = hitInfo.transform.localEulerAngles.y;

                        // Rotar en incrementos exactos de 90 grados
                        float nuevaRotacionY = Mathf.Round((rotacionActualY + 90f) / 90f) * 90f;

                        // Aplicar la nueva rotación al objeto en el eje Y
                        hitInfo.transform.localEulerAngles = new Vector3(
                            hitInfo.transform.localEulerAngles.x,
                            nuevaRotacionY,
                            hitInfo.transform.localEulerAngles.z
                        );
                    }
                }
            }
        }
    }

    // Esta función se llamará desde el botón en el canvas para alternar la rotación
    public void AlternarRotacion()
    {
        // Cambiar el estado de la rotación habilitada/deshabilitada
        rotacionHabilitada = !rotacionHabilitada;
        Debug.Log("Rotación habilitada: " + rotacionHabilitada);
    }
}




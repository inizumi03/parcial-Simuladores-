using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverTuberias : MonoBehaviour
{
    // Variable para controlar si la rotaci�n est� habilitada
    private bool rotacionHabilitada = false;

    void Update()
    {
        // Solo permitir la rotaci�n si la variable est� habilitada
        if (rotacionHabilitada)
        {
            // Detectar si se ha hecho clic con el bot�n izquierdo del mouse
            if (Input.GetMouseButtonDown(0))
            {
                // Crear un rayo desde la c�mara hacia donde est� apuntando el mouse
                Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                // Verificar si el rayo golpea un objeto
                if (Physics.Raycast(rayo, out hitInfo))
                {
                    // Verificar si el objeto tiene la etiqueta "Tubo"
                    if (hitInfo.transform.CompareTag("tubo"))
                    {
                        // Obtener la rotaci�n actual del objeto en el eje Y
                        float rotacionActualY = hitInfo.transform.localEulerAngles.y;

                        // Rotar en incrementos exactos de 90 grados
                        float nuevaRotacionY = Mathf.Round((rotacionActualY + 90f) / 90f) * 90f;

                        // Aplicar la nueva rotaci�n al objeto en el eje Y
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

    // Esta funci�n se llamar� desde el bot�n en el canvas para alternar la rotaci�n
    public void AlternarRotacion()
    {
        // Cambiar el estado de la rotaci�n habilitada/deshabilitada
        rotacionHabilitada = !rotacionHabilitada;
        Debug.Log("Rotaci�n habilitada: " + rotacionHabilitada);
    }
}




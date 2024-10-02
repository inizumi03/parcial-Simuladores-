using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionarTubos : MonoBehaviour
{
    // Variables para controlar si la rotaci�n, cambio de color y destrucci�n est�n habilitados
    private bool rotacionHabilitada = false;
    private bool cambioColorHabilitado = false;
    private bool destruccionHabilitada = false;

    // Referencia al nuevo material verde (para los tubos reparados)
    public Material materialVerde;

    void Update()
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
                // Verificar si el objeto tiene la etiqueta "Tubo", "TuboRoto", "TuboDestruir" o "TuboRotar"
                if (hitInfo.transform.CompareTag("tubo") ||
                    hitInfo.transform.CompareTag("TuboRoto") ||
                    hitInfo.transform.CompareTag("TuboDestruir") ||
                    hitInfo.transform.CompareTag("TuboRotar"))
                {
                    // Si la rotaci�n est� habilitada, realizar la rotaci�n
                    if (rotacionHabilitada)
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

                        // Si es un tubo con la etiqueta "TuboRotar" y ha sido rotado, cambiar la etiqueta a "Tubo"
                        if (hitInfo.transform.CompareTag("TuboRotar"))
                        {
                            hitInfo.transform.tag = "tubo";
                            Debug.Log("Tubo rotado correctamente y su etiqueta ha sido cambiada a 'Tubo'");
                        }
                    }

                    // Si el cambio de color est� habilitado y es un "TuboRoto", cambiar su material a verde
                    if (cambioColorHabilitado && hitInfo.transform.CompareTag("TuboRoto"))
                    {
                        Renderer renderer = hitInfo.transform.GetComponent<Renderer>();

                        if (renderer != null)
                        {
                            // Cambiar el material al nuevo material verde
                            renderer.material = materialVerde;
                            Debug.Log("Material del tubo roto cambiado a verde");
                        }
                    }

                    // Si la destrucci�n est� habilitada y el objeto es "TuboDestruir", destruir el objeto
                    if (destruccionHabilitada && hitInfo.transform.CompareTag("TuboDestruir"))
                    {
                        // Destruir el objeto
                        Destroy(hitInfo.transform.gameObject);
                        Debug.Log("Tubo destruido");
                    }
                }
            }
        }
    }

    // Esta funci�n se llama desde el bot�n para alternar la rotaci�n
    public void AlternarRotacion()
    {
        // Cambiar el estado de la rotaci�n habilitada/deshabilitada
        rotacionHabilitada = !rotacionHabilitada;
        Debug.Log("Rotaci�n habilitada: " + rotacionHabilitada);
    }

    // Esta funci�n se llama desde el bot�n para alternar el cambio de color
    public void AlternarCambioColor()
    {
        // Cambiar el estado del cambio de color habilitado/deshabilitado
        cambioColorHabilitado = !cambioColorHabilitado;
        Debug.Log("Cambio de color habilitado: " + cambioColorHabilitado);
    }

    // Esta funci�n se llama desde el bot�n para alternar la destrucci�n de tubos
    public void AlternarDestruccion()
    {
        // Cambiar el estado de la destrucci�n habilitada/deshabilitada
        destruccionHabilitada = !destruccionHabilitada;
        Debug.Log("Destrucci�n habilitada: " + destruccionHabilitada);
    }
}

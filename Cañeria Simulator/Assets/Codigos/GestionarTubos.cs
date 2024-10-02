using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionarTubos : MonoBehaviour
{
    // Variables para controlar si la rotación, cambio de color y destrucción están habilitados
    private bool rotacionHabilitada = false;
    private bool cambioColorHabilitado = false;
    private bool destruccionHabilitada = false;

    // Referencia al nuevo material verde (para los tubos reparados)
    public Material materialVerde;

    void Update()
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
                // Verificar si el objeto tiene la etiqueta "Tubo", "TuboRoto", "TuboDestruir" o "TuboRotar"
                if (hitInfo.transform.CompareTag("tubo") ||
                    hitInfo.transform.CompareTag("TuboRoto") ||
                    hitInfo.transform.CompareTag("TuboDestruir") ||
                    hitInfo.transform.CompareTag("TuboRotar"))
                {
                    // Si la rotación está habilitada, realizar la rotación
                    if (rotacionHabilitada)
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

                        // Si es un tubo con la etiqueta "TuboRotar" y ha sido rotado, cambiar la etiqueta a "Tubo"
                        if (hitInfo.transform.CompareTag("TuboRotar"))
                        {
                            hitInfo.transform.tag = "tubo";
                            Debug.Log("Tubo rotado correctamente y su etiqueta ha sido cambiada a 'Tubo'");
                        }
                    }

                    // Si el cambio de color está habilitado y es un "TuboRoto", cambiar su material a verde
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

                    // Si la destrucción está habilitada y el objeto es "TuboDestruir", destruir el objeto
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

    // Esta función se llama desde el botón para alternar la rotación
    public void AlternarRotacion()
    {
        // Cambiar el estado de la rotación habilitada/deshabilitada
        rotacionHabilitada = !rotacionHabilitada;
        Debug.Log("Rotación habilitada: " + rotacionHabilitada);
    }

    // Esta función se llama desde el botón para alternar el cambio de color
    public void AlternarCambioColor()
    {
        // Cambiar el estado del cambio de color habilitado/deshabilitado
        cambioColorHabilitado = !cambioColorHabilitado;
        Debug.Log("Cambio de color habilitado: " + cambioColorHabilitado);
    }

    // Esta función se llama desde el botón para alternar la destrucción de tubos
    public void AlternarDestruccion()
    {
        // Cambiar el estado de la destrucción habilitada/deshabilitada
        destruccionHabilitada = !destruccionHabilitada;
        Debug.Log("Destrucción habilitada: " + destruccionHabilitada);
    }
}

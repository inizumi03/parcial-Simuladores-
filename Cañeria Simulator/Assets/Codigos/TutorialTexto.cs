using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTexto : MonoBehaviour
{
    // Lista de mensajes del tutorial
    [TextArea(3, 10)]
    public string[] mensajes;

    // Referencia al componente de texto donde se mostrará el mensaje
    public Text textoTutorial; // Si usas Text
    // public TMP_Text textoTutorial; // Descomentar si usas TextMeshPro

    // Índice actual del mensaje
    private int indiceMensaje = 0;

    // Objeto que será desactivado cuando el tutorial termine
    public GameObject objetoParaDesactivar;

    void Start()
    {
        // Mostrar el primer mensaje si hay mensajes en la lista
        if (mensajes.Length > 0)
        {
            textoTutorial.text = mensajes[indiceMensaje];
        }
    }

    void Update()
    {
        // Detectar clic izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            MostrarSiguienteMensaje();
        }
    }

    // Función para mostrar el siguiente mensaje en la lista
    void MostrarSiguienteMensaje()
    {
        // Incrementar el índice del mensaje
        indiceMensaje++;

        // Si aún hay mensajes por mostrar
        if (indiceMensaje < mensajes.Length)
        {
            // Mostrar el siguiente mensaje
            textoTutorial.text = mensajes[indiceMensaje];
        }
        else
        {
            // Cuando se acaben los mensajes, desactivar el objeto
            objetoParaDesactivar.SetActive(false);

            // Opcional: ocultar el texto del tutorial
            textoTutorial.gameObject.SetActive(false);
        }
    }
}

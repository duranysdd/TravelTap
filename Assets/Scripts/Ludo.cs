using UnityEngine;
using TMPro;
using System.Collections;

public class Ludo : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject iconoInteraccion;
    public GameObject panelDialogo;
    public TextMeshProUGUI textoDialogo;

    [Header("Diálogo")]
    [TextArea(2, 5)]
    public string[] lineas;

    [Header("Configuración")]
    public float rangoInteraccion = 2f;
    public LayerMask capaJugador;
    public Collider2D colisionNPC;

    [Header("Efecto Máquina de Escribir")]
    public float velocidadEscritura = 0.03f;
    public AudioSource sonidoLetra;

    private int indiceLinea = 0;
    private bool jugadorCerca = false;
    private bool dialogoAbierto = false;
    private bool escribiendo = false;

    private Coroutine rutinaEscritura;

    void Start()
    {
        if (iconoInteraccion != null) iconoInteraccion.SetActive(false);
        if (panelDialogo != null) panelDialogo.SetActive(false);
    }

    void Update()
    {
        Collider2D jugador = Physics2D.OverlapCircle(
            transform.position, rangoInteraccion, capaJugador);
        jugadorCerca = jugador != null;

        if (iconoInteraccion != null)
            iconoInteraccion.SetActive(jugadorCerca && !dialogoAbierto);

        if (jugadorCerca && Input.GetKeyDown(KeyCode.E) && !dialogoAbierto)
        {
            IniciarDialogo();
        }

        if (!dialogoAbierto) return;

        if (escribiendo && Input.GetKeyDown(KeyCode.E))
        {
            CompletarTexto();
            return;
        }

        if (!escribiendo && Input.GetKeyDown(KeyCode.E))
        {
            SiguienteLinea();
        }
    }

    void IniciarDialogo()
    {
        if (lineas.Length == 0)
        {
            Debug.LogWarning("Ludo no diagolo");
            return;
        }

        dialogoAbierto = true;
        panelDialogo.SetActive(true);
        indiceLinea = 0;

        MostrarLineaActual();
    }

    void MostrarLineaActual()
    {
        textoDialogo.text = "";

        if (rutinaEscritura != null)
            StopCoroutine(rutinaEscritura);

        rutinaEscritura = StartCoroutine(MaquinaDeEscribir(lineas[indiceLinea]));
    }

    IEnumerator MaquinaDeEscribir(string texto)
    {
        escribiendo = true;

        foreach (char letra in texto.ToCharArray())
        {
            textoDialogo.text += letra;

            if (sonidoLetra != null)
                sonidoLetra.Play();

            yield return new WaitForSeconds(velocidadEscritura);
        }

        escribiendo = false;
    }

    void CompletarTexto()
    {
        if (rutinaEscritura != null)
            StopCoroutine(rutinaEscritura);

        textoDialogo.text = lineas[indiceLinea];
        escribiendo = false;
    }

    void SiguienteLinea()
    {
        indiceLinea++;

        if (indiceLinea >= lineas.Length)
        {
            TerminarDialogo();
            return;
        }

        MostrarLineaActual();
    }

    void TerminarDialogo()
    {
        dialogoAbierto = false;
        panelDialogo.SetActive(false);

        if (colisionNPC != null)
            colisionNPC.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoInteraccion);
    }
}

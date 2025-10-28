using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Ludo : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject iconoInteraccion;  
    public GameObject panelDialogo;      
    public TextMeshProUGUI textoDialogo; 
    public string mensaje = "¡Hola aventurero! Ten cuidado más adelante...";

    [Header("Configuración")]
    public float rangoInteraccion = 2f; 
    public LayerMask capaJugador;     
    public Collider2D colisionNPC; 

    private bool jugadorCerca = false;
    private bool yaHablo = false;

    void Start()
    {
        iconoInteraccion.SetActive(false);
        panelDialogo.SetActive(false);
    }

    void Update()
    {
        Collider2D jugador = Physics2D.OverlapCircle(transform.position, rangoInteraccion, capaJugador);
        jugadorCerca = jugador != null;

        iconoInteraccion.SetActive(jugadorCerca && !yaHablo);

        if (jugadorCerca && Input.GetKeyDown(KeyCode.F))
        {
            MostrarDialogo();
        }
    }

    void MostrarDialogo()
    {
        panelDialogo.SetActive(true);
        textoDialogo.text = mensaje;
        yaHablo = true;

        if (colisionNPC != null)
        {
            colisionNPC.enabled = false;
        }

        iconoInteraccion.SetActive(false);
    }

    void LateUpdate()
    {
        if (panelDialogo.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            panelDialogo.SetActive(false);
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoInteraccion);
    }
}

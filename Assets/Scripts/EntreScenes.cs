using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntreScenes : MonoBehaviour
{
    // Start is called before the first frame update


    public static EntreScenes Instance;

    public string NombreJugador;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
    public void NewNombreJugador(string nombre)
    {
        EntreScenes.Instance.NombreJugador = nombre;
    }
}

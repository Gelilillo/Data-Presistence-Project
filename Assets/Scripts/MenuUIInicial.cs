using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIInicial : MonoBehaviour
{
    // Start is called before the first frame update

    public InputField inputField;
    public Button BotonStart;

    public static EntreScenes Instance;


    void Start()
    {
        inputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void star()
    {

        EntreScenes.Instance.NombreJugador = inputField.text;
        SceneManager.LoadScene(1);

    }
    public void Exit()
    {
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit(); // original code to quit Unity player
    #endif
    }
    void ValueChangeCheck()
    {
        if (inputField.text.Length > 0)
        {
            BotonStart.interactable = true;
        }
        else
        {
            BotonStart.interactable = false;
        }
    }
}

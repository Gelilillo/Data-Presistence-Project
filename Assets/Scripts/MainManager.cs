using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text NombreJugador;
    public Text recordText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public static EntreScenes Instance;

    private string nombre_record;
    private int puntos_record;

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        //Carga el nombre del jugador de los datos del inicio

        if (EntreScenes.Instance != null)
        {

            //Debug.Log("El nombre del jugador es " + EntreScenes.Instance.NombreJugador);
            NombreJugador.text= EntreScenes.Instance.NombreJugador;
        }

        //Carga el record existente

        LoadRecord();

        recordText.text = "Best Score : " + nombre_record + " : " + puntos_record;



    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        if (m_Points > puntos_record)
        {
            SaveRecord();
        }

        
        GameOverText.SetActive(true);
        


    }

    [System.Serializable]
    class Puntuacion_jugador
    {
        public string nombre;
        public int puntos;
    }

    public void SaveRecord()
    {
        Puntuacion_jugador data = new Puntuacion_jugador();
        data.nombre = NombreJugador.text;
        data.puntos = m_Points;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadRecord()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Puntuacion_jugador data = JsonUtility.FromJson<Puntuacion_jugador>(json);
            //Trae los datos guardados
            nombre_record = data.nombre;
            puntos_record = data.puntos;
        }
    }

}

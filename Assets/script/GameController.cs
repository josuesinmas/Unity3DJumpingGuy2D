using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {Idle,Playing,Die,Redy}

public class GameController : MonoBehaviour
{
    //variables de numeros reales
    [Range(0.0f, 0.20f)]
    public float parallaxSpeed =0.02f;
    public float Scaletime=6f;
    public float ScaleInt=.25f;
    private int Points=0;
    //objetos de imagenes
    public RawImage background;
    public RawImage platform;
    //objetos de texto
    public Text PointText;
    public Text RecordText;
    //estados del juego
    public GameState gamestate=GameState.Idle;
    //objetos del juego
    public GameObject EnemiGenerator;
    public GameObject player;
    public GameObject uiIdle;
    public GameObject uiScore;
    //objeto de audio
    private AudioSource musicPlayer;
    public string x="Alejandro";


    // Start is called before the first frame update
    void Start()
    {
        musicPlayer=GetComponent<AudioSource>();
        RecordText.text = "Best: "+ GetMaxScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
        bool Imput = (Input.GetKeyDown("up")||Input.GetMouseButtonDown(0));
       
        // Empieza el juego
        if (gamestate==GameState.Idle&&Imput){
            gamestate=GameState.Playing;
            uiIdle.SetActive(false);
            uiScore.SetActive(true);
            player.SendMessage("UpdateState","PlayerRun");
            player.SendMessage("PolvoPlay");
            EnemiGenerator.SendMessage("StartGeneration");
            musicPlayer.Play();
            InvokeRepeating("GameTimeScale",Scaletime,Scaletime);
        }

        //juego en marcha
        else if(gamestate==GameState.Playing){
        Paralax();
        }

        //juego preparado para reiniciarse
         else if(gamestate==GameState.Redy){
            if (Imput){
                RestartGame();
            }
        }
    }
    //movimiento del forndo y la plataforma
    void Paralax(){
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect=new Rect(background.uvRect.x+finalSpeed,0f,1f,1f);
        platform.uvRect=new Rect(platform.uvRect.x+finalSpeed,0f,1f,1f);
    }

    public void RestartGame(){
        ResetTimeScale();
        SceneManager.LoadScene("Principal");
    }

    void GameTimeScale(){
        Time.timeScale+=ScaleInt;
        Debug.Log(Time.timeScale);
    }

    public void ResetTimeScale(float NewTimeScale = 1f){
            CancelInvoke("GameTimeScale");
            Time.timeScale=NewTimeScale;
            Debug.Log("dificultad reiniciada");

    }

    //Funciones de puntuaje
    public void IncreasePoints(){
        PointText.text=(++Points).ToString();
        if (Points >= GetMaxScore()){
            RecordText.text = "BEST: "+ Points.ToString(); 
            SaveScore(Points);
        }

    }
    public int GetMaxScore(){
        return PlayerPrefs.GetInt("Max Points",0);
    }
    public void SaveScore(int currentPoints){
        PlayerPrefs.SetInt("Max Points", currentPoints);
    }
    public string hola(string x){
        return x;
    }
}

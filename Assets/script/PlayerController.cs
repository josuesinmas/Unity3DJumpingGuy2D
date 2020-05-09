                                                                                                        using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{   
    public GameObject game;
    private Animator animator;
    public AudioClip jumclip;
    public AudioClip dieclip;
    public AudioClip pointClip;
    public GameObject EnemiGenerator;
    private AudioSource audioPlayer;
    private float startY;
    public ParticleSystem polvo;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        audioPlayer=GetComponent<AudioSource>();
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        bool imput = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);
        bool isgrounder = transform.position.y==startY;
        bool gamePlaying = game.GetComponent<GameController>().gamestate == GameState.Playing;
        
       if(gamePlaying && imput && isgrounder){
           UpdateState("PlayerJum");
           audioPlayer.clip=jumclip;
           audioPlayer.Play();

       }
    }
    public void UpdateState(string state=null){
        if (state!=null){
            animator.Play(state);

        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Enemi"){
             UpdateState("PlayerDie");
             game.GetComponent<GameController>().gamestate = GameState.Die;
             EnemiGenerator.SendMessage("CancelGeneration");
             game.GetComponent<AudioSource>().Stop();
             audioPlayer.clip=dieclip;
             audioPlayer.Play();
             game.SendMessage("ResetTimeScale",0.5f);
             PolvoStop();
        }
        else if (other.gameObject.tag=="Point"){
            audioPlayer.clip=pointClip;
            audioPlayer.Play();
            game.SendMessage("IncreasePoints");
            
        }
    }  
    void gameRedy(){
        game.GetComponent<GameController>().gamestate = GameState.Redy;

    }
    void PolvoPlay(){
        polvo.Play();
    }
    void PolvoStop(){
        polvo.Stop();

    }
}

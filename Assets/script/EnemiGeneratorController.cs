using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiGeneratorController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemiPrefab;
    public float GeneratorTime=1.75f;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CreateEnemi(){
        Instantiate(enemiPrefab,transform.position,Quaternion.identity);
    }
    public void StartGeneration(){
        InvokeRepeating("CreateEnemi",0f,GeneratorTime);
    }
    public void CancelGeneration(){
        CancelInvoke("CreateEnemi");
    }
}

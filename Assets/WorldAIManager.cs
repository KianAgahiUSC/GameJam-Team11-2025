using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldAIManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int initialSpawnPeriod;
    [SerializeField] int basicSpawnTime;    
    void Start()
    {
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private GameObject enemy;
    private GameObject[] enemies;
    [SerializeField] GameObject iguanaPrefab;
    int iguanas = 0;
    private GameObject iguana;
    [SerializeField] int iguanaCount;
    private int spawnMover = 0;
    [SerializeField] private UIController ui;
    private int score = 0;
    
    private int currentEnemies;


    private Vector3 spawnPoint = new Vector3(0, 0, 5);
    private Vector3 iguanaSpawnPoint = new Vector3(0, 0, -5);

    public void OnRestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void OnPlayerDead()
    {
        ui.ShowGameOverPopup();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentEnemies = 1;
        enemies = new GameObject[currentEnemies];
        
        
        score = 0;

    }

    private void OnDifficultyChanged(int newDifficulty) {
        Debug.Log("Scene.OnDifficultyChanged(" + newDifficulty + ")");
        for (int i = 0; i < enemies.Length; i++)
        {
            WanderingAI ai = enemies[i].GetComponent<WanderingAI>();
            ai.SetDifficulty(newDifficulty);
        }
    }

    int GetDifficulty()
    {
        return PlayerPrefs.GetInt("difficulty", 1);
       
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < currentEnemies; i++)
        {
            if (!enemies[i])
            {
                enemies[i] = Instantiate(enemyPrefab) as GameObject;
                enemies[i].transform.position = spawnPoint;
                float angle = Random.Range(0, 360);
                enemies[i].transform.Rotate(0, angle, 0);
                WanderingAI wander = enemies[i].GetComponent<WanderingAI>();
                wander.SetDifficulty(GetDifficulty());
            }
        } 

        

    }

    private void FixedUpdate()
    {

        if (iguanas < iguanaCount)
        {
            spawnMover ++;
            iguanas++;
            Vector3 iguanaSpawnPoint = new Vector3(0, 0 + spawnMover, -5 );
            
            
            iguana = Instantiate(iguanaPrefab) as GameObject;
            iguana.transform.position = iguanaSpawnPoint;
            float angle = Random.Range(0, 360);
            iguana.transform.Rotate(0, angle, 0);
        }
            
        
    }

    void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_DEAD, this.OnEnemyDead);
        Messenger<int>.AddListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
        Messenger.AddListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.AddListener(GameEvent.RESTART_GAME, OnRestartGame);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_DEAD, this.OnEnemyDead);
        Messenger<int>.RemoveListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
        Messenger.RemoveListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.RemoveListener(GameEvent.RESTART_GAME, OnRestartGame);
    }

    void OnEnemyDead()
    {
        score++;
        ui.UpdateScore(score);
    }


}


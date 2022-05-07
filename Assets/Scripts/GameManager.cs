using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int curLevel;
    public bool isEndGame { get; set; }
    
    public Animator animator;
    private AudioSource m_audio;

    public GameObject PlayerA;
    public GameObject PlayerB;
    private Transform spawnPosA;
    private Transform spawnPosB;

    private bool isLoadScene;

    public bool canRandomItem;
    public bool hasPlayerDead;
    // Start is called before the first frame update
    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        isEndGame = false;
        instance = this;
        curLevel = 1;
        
        StartCoroutine(loadFirstScence());
    }

    // Update is called once per frame
    void Update()
    {

        if (isEndGame )
        {
            isEndGame = false;
            StartCoroutine(EndGame());
        }
        if (isLoadScene)
        {
            //Player spawn
            spawnPosA = GameObject.FindGameObjectWithTag("Spawn").transform;
            Instantiate(PlayerA, spawnPosA.position, spawnPosA.rotation);

            //Bot spawn
            spawnPosB = GameObject.FindGameObjectWithTag("SpawnB").transform;
            Instantiate(PlayerB, spawnPosB.position, spawnPosB.rotation);

            canRandomItem = true;
            isLoadScene = false;
            hasPlayerDead = false;
        }
    }
    
    IEnumerator loadFirstScence ()
    {
        SceneManager.LoadScene(curLevel, LoadSceneMode.Additive);
        yield return new WaitForSeconds(1f);
        isLoadScene = true;
        m_audio.Play();
    }
    int getNextLevel ()
    {
        return Random.Range(1, 3);
    }
    public IEnumerator EndGame ()
    {
        canRandomItem = false;

        yield return new WaitForSeconds(1f);

        animator.SetTrigger("FadeIn");

        foreach (var bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            //bullet.SetActive(false);
            Destroy(bullet);
        }

        foreach (var exploFx in GameObject.FindGameObjectsWithTag("Explosion"))
        {
            Destroy(exploFx);
        }

        foreach (var item in GameObject.FindGameObjectsWithTag("Item"))
        {
            Destroy(item);
        }

        var playerA = GameObject.FindGameObjectWithTag("PlayerA");
        var playerB = GameObject.FindGameObjectWithTag("PlayerB");
        Destroy(playerA);
        Destroy(playerB);

        
        yield return new WaitForSeconds(0.75f);

        AsyncOperation gameUnload = SceneManager.UnloadSceneAsync(curLevel);
        
        while (!gameUnload.isDone)
        {
            yield return new WaitForSeconds(0.1f);
        }

        curLevel = getNextLevel();
       
        SceneManager.LoadScene(curLevel, LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.75f);
        isLoadScene = true;
        
        animator.SetTrigger("FadeOut");
    }
}

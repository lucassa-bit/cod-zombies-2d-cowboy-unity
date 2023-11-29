using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private int QuantityToIncreaseEnemiesPerRound;
    [SerializeField]
    private int StartEnemies;
    [SerializeField]
    private int MaxSpawnEnemies;
    [SerializeField]
    private int CooldownToSpawn;
    [SerializeField]
    private GameObject EnemyGeneratorsGO;
    [SerializeField]
    private GameObject PauseMenu;
    [SerializeField]
    private GameObject GameOverMenu;
    [SerializeField]
    private TextMeshProUGUI Health;
    [SerializeField]
    private TextMeshProUGUI Money;
    [SerializeField]
    private float SpawnDetectionSize;
    [SerializeField]
    private TextMeshProUGUI RoundText;

    private PlayerStats PlayerStats;
    private List<EnemyGenerator> EnemyGenerators;

    private int Enemies;
    private int EnemyKilledRound;
    private int EnemiesToBeAlive;
    private bool CanGoToNextRound = false;
    private bool CanSpawn = true;

    public int Round { get; private set; }

    private void Awake()
    {
        EnemyGenerators = new(EnemyGeneratorsGO.GetComponentsInChildren<EnemyGenerator>());

        foreach (EnemyGenerator gen in EnemyGenerators)
        {
            gen.SizeToCheckIfCanSpawn = SpawnDetectionSize;
            gen.Player = Player;
        }

        PlayerStats = Player.GetComponent<PlayerStats>();
    }

    private void Start()
    {
        Time.timeScale = 1f;

        Enemies = 0;
        Round = 1;
        RoundText.text = "ROUND " + Round;
        EnemiesToBeAlive = StartEnemies;
        StartCoroutine(SpawnAllEnemiesOfTheRound(CooldownToSpawn));
    }

    private void Update()
    {
        UpdatePlayerStatus();

        if (CanGoToNextRound)
        {
            EnemiesToBeAlive += QuantityToIncreaseEnemiesPerRound;
            Round++;
            RoundText.text = "ROUND " + Round;

            CanGoToNextRound = false;
            CanSpawn = true;

            Enemies = 0;
            EnemyKilledRound = 0;
        }

        if (CanSpawn)
            StartCoroutine(SpawnAllEnemiesOfTheRound(CooldownToSpawn));

    }

    private void UpdatePlayerStatus()
    {
        Health.text = PlayerStats.Health + "";
        Money.text = PlayerStats.GetAmount() + "";
    }

    private IEnumerator SpawnAllEnemiesOfTheRound(float cooldown)
    {
        CanSpawn = false;
        List<EnemyGenerator> enemyGeneratorsOnRange = GetEnemyGenerators(EnemyGenerators, new List<EnemyGenerator>());

        if (enemyGeneratorsOnRange.Count == 0)
        {
            CanSpawn = true;
            yield break;
        }

        while (Enemies < EnemiesToBeAlive - EnemyKilledRound)
        {
            enemyGeneratorsOnRange.AddRange(GetEnemyGenerators(EnemyGenerators, enemyGeneratorsOnRange));

            int randomIndex = Random.Range(0, enemyGeneratorsOnRange.Count);
            int spawnMax = (Enemies + EnemyKilledRound < EnemiesToBeAlive - MaxSpawnEnemies) ? MaxSpawnEnemies : EnemiesToBeAlive - Enemies - EnemyKilledRound;
            
            Enemies += enemyGeneratorsOnRange[randomIndex].CreateNewEnemies(this, 1, spawnMax);

            yield return new WaitForSeconds(cooldown);
        }
    }

    private List<EnemyGenerator> GetEnemyGenerators(List<EnemyGenerator> allGenerators, List<EnemyGenerator> alreadyOnRange)
    {
        List<EnemyGenerator> enemyGenerators = new();

        foreach (EnemyGenerator gen in allGenerators)
        {
            if(gen.IsOnRangeToSpawnEnemies() && !alreadyOnRange.Contains(gen))
            {
                enemyGenerators.Add(gen);
            }
        }

        return enemyGenerators.Count == 0 && alreadyOnRange.Count == 0 ? GetEmergencyGenerator(allGenerators) : enemyGenerators;
    }

    public void EnemyKilled()
    {
        Enemies--;
        EnemyKilledRound++;

        if (EnemyKilledRound >= EnemiesToBeAlive) CanGoToNextRound = true;
    }

    public List<EnemyGenerator> GetEmergencyGenerator(List<EnemyGenerator> allGenerators)
    {
        EnemyGenerator enemyGenerator = null;
        float returnGenDistance = 100;
        foreach (EnemyGenerator gen in allGenerators)
        {
            float distancePlayerGen = Vector3.Distance(gen.transform.position, Player.transform.position);
            if (distancePlayerGen < returnGenDistance)
            {
                enemyGenerator = gen;
                returnGenDistance = distancePlayerGen;
            }
        }

        return new List<EnemyGenerator>() { enemyGenerator };
    } 

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void RestartGame()
    {
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver()
    {
        GameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}

using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject Enemy;

    [SerializeField]
    private float VariationDistanceToSpawn;

    private Vector3 DontSpawn;

    public float SizeToCheckIfCanSpawn;
    public Transform Player;

    private void InstatiateEnemy(GameController controller) {
        GameObject enemy = Instantiate(Enemy, RandomPositionForSpawn(transform.position, VariationDistanceToSpawn), Quaternion.identity);
        enemy.GetComponent<AIDestinationSetter>().target = Player;
        enemy.GetComponent<EnemyStats>().Controller = controller;
    }

    private Vector3 RandomPositionForSpawn(Vector3 originalPos, float variation)
    {
        Vector3 newRandom;

        do {
            newRandom = new Vector3(RandomRangeAxis(originalPos.x, variation), RandomRangeAxis(originalPos.y, variation));
        } while (DontSpawn == newRandom);

        DontSpawn = newRandom;

        return newRandom;
    }

    private float RandomRangeAxis(float axis, float variation)
    {
        return Random.Range(axis - variation, axis + variation);
    }

    public int CreateNewEnemies(GameController controller, int minCreated, int maxCreated)
    {
        int quantityCreated = Random.Range(minCreated, maxCreated + 1);

        for (int index = 0; index < quantityCreated; index++)
            InstatiateEnemy(controller);

        return quantityCreated;
    }

    public bool IsOnRangeToSpawnEnemies()
    {
        return Physics2D.CircleCast(transform.position, SizeToCheckIfCanSpawn, Vector2.zero, 0, LayerMask.GetMask("Player"));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SizeToCheckIfCanSpawn);
    }
}

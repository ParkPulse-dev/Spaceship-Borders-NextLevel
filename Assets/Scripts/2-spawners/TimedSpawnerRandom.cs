using System.Collections;
using UnityEngine;

public class TimedSpawnerRandom : MonoBehaviour
{
    private const string PlayerSpaceshipTag = "Player";
    private const string toNextLevelTag = "NextLevel";

    [SerializeField] int maxScoreThreshold = 50;
    [SerializeField] Mover prefabToSpawn;
    [SerializeField] Vector3 velocityOfSpawnedObject;
    [Tooltip("Minimum time between consecutive spawns, in seconds")][SerializeField] float minTimeBetweenSpawns = 0.2f;
    [Tooltip("Maximum time between consecutive spawns, in seconds")][SerializeField] float maxTimeBetweenSpawns = 1.0f;
    [Tooltip("Maximum distance in X between spawner and spawned objects, in meters")][SerializeField] float maxXDistance = 1.5f;

    void Start()
    {
        // Deactivate ToNextLevel at the beginning
        GameObject toNextLevelObject = GameObject.FindWithTag(toNextLevelTag);
        if (toNextLevelObject != null)
        {
            toNextLevelObject.SetActive(false);
        }
        StartCoroutine(SpawnRoutine(toNextLevelObject));
    }

    IEnumerator SpawnRoutine(GameObject toNextLevelObject)
    {
        // Declare scoreField outside of the if block
        NumberField scoreField = null;

        while (true)
        {
            // Find the PlayerSpaceship GameObject
            GameObject playerSpaceship = GameObject.FindWithTag(PlayerSpaceshipTag);

            // Check if the playerSpaceship is found
            if (playerSpaceship != null)
            {
                // Get the NumberField component from the PlayerSpaceship
                scoreField = playerSpaceship.GetComponentInChildren<NumberField>();
            }
            else
            {
                yield return null;
            }

            // Check if the scoreField is not null before using it
            if (scoreField != null)
            {
                // Check if the score is below the threshold before spawning
                if (scoreField.GetNumber() < maxScoreThreshold)
                {
                    float timeBetweenSpawnsInSeconds = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
                    yield return new WaitForSeconds(timeBetweenSpawnsInSeconds);

                    // Check the ScoreField score again in case it changed during the wait
                    if (scoreField.GetNumber() <= maxScoreThreshold)
                    {
                        Vector3 positionOfSpawnedObject = new Vector3(
                            transform.position.x + Random.Range(-maxXDistance, +maxXDistance),
                            transform.position.y,
                            transform.position.z);

                        GameObject newObject = Instantiate(prefabToSpawn.gameObject, positionOfSpawnedObject, Quaternion.identity);
                        newObject.GetComponent<Mover>().SetVelocity(velocityOfSpawnedObject);
                    }
                }
                else
                {
                    if (toNextLevelObject != null)
                    {
                        toNextLevelObject.SetActive(true);
                    }
                }
            }

            yield return null;
        }
    }
}

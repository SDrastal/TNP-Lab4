using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float minSpawnRadius = 2f;
    public float maxSpawnRadius = 5f;
    private float orbitAngle;    // Current angle in radians
    private float orbitRadius;   // Distance from player

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        // Pick a random angle and spawn radius
        orbitAngle = Random.Range(0f, Mathf.PI * 2f);
        orbitRadius = Random.Range(minSpawnRadius, maxSpawnRadius);

        // Set initial position around the player
        SetOrbitPosition();
    }

    void Update()
    {
        if (player == null) return;

        // Purely distance-based orbit speed (controlled using a curve)
        float orbitSpeed = CalculateOrbitSpeed(orbitRadius);

        // Update orbit angle
        orbitAngle += orbitSpeed * Time.deltaTime;

        // Calculate new position
        SetOrbitPosition();
    }

    float CalculateOrbitSpeed(float radius)
    {
        // Example: speed grows slowly as radius increases
        return Mathf.Sqrt(radius) * 0.5f;
    }

    void SetOrbitPosition()
    {
        float x = Mathf.Cos(orbitAngle) * orbitRadius;
        float y = Mathf.Sin(orbitAngle) * orbitRadius;

        transform.position = new Vector3(player.position.x + x, player.position.y + y, transform.position.z);
    }
}

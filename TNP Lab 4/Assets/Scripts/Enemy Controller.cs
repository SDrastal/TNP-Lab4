using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;         // Player Transform reference (assign in Inspector or find in Start)
    public float orbitSpeed = 2f;   // Orbit speed in radians/sec
    public float orbitAngle;       // Current orbit angle (radians)
    public float orbitRadius;      // Distance from player

    void Start()
    {
        if (player == null)
        {
            // Try to find player by tag
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                Debug.LogError("Player GameObject not found!");
        }

        // Calculate initial offset and radius relative to player's position at start
        Vector3 offset = transform.position - player.position;
        orbitRadius = new Vector2(offset.x, offset.y).magnitude;
        orbitAngle = Mathf.Atan2(offset.y, offset.x);
    }

    void Update()
    {
        if (player == null) return;

        // Increment orbit angle over time
        orbitAngle += orbitSpeed * Time.deltaTime;

        // Calculate the new position around player's current position
        float x = Mathf.Cos(orbitAngle) * orbitRadius;
        float y = Mathf.Sin(orbitAngle) * orbitRadius;

        // **Always calculate relative to player's current position!**
        Vector3 orbitPos = new Vector3(player.position.x + x, player.position.y + y, transform.position.z);

        // Move meteor to orbit position
        transform.position = orbitPos;

        // Debug: confirm player position updates
        Debug.Log($"Player Position: {player.position} | Meteor Position: {transform.position}");
    }
}

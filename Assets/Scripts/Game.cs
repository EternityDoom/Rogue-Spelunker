using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab = null;
    public Player player { get; set; }
    public List<Enemy> enemies { get; set; }
    public List<Tile> tiles { get; set; }
    public List<Powerup> powerups { get; set; }
    [SerializeField] public bool[] powerFound = null;
    public Vector2 respawnPoint { get; set; } = new Vector2(0, 0);

    private void Awake()
    {
        enemies.AddRange(gameObject.GetComponentsInChildren<Enemy>());
        tiles.AddRange(gameObject.GetComponentsInChildren<Tile>());
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            GameObject spawn = Instantiate(playerPrefab, this.gameObject.transform);
            spawn.transform.position = respawnPoint;
            player = spawn.GetComponent<Player>();
        }
        Camera.main.transform.position = player.gameObject.transform.position;
    }
}

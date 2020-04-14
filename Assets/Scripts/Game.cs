using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    //public Player player { get; set; }
    //public List<Enemy> enemies { get; set; }
    //public List<Tile> tiles { get; set; }
    //public List<Powerup> powerups { get; set; }
    public bool[] powerFound { get; set; } = 
    public Vector2 respawnPoint { get; set; } = new Vector2(0, 0);



    // Update is called once per frame
    void Update()
    {
        // If player is null (meaning it died), create a new one at the respawn point
        // Center the camera on the player
    }
}

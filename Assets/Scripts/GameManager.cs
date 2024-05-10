using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    static public Tilemap WallTilemap;

    private void Start()
    {
        WallTilemap = GameObject.FindObjectOfType<WallTileMap>().GetComponent<Tilemap>();
    }
    /*//Declare our references to the common game objects in the game that we will need to use
     public Enemy[] enemies;
    public Player player;
    public Pellet[] pellets;
    public int score { get; private set; } 
    public int lives { get; private set; }

    // Might do a lot of things like keep track of our score for example, but for now as of creation
    // gives the other objects the ability to ask the game manager for a quick and easy lookup of what they
    // want (at this point, it is the wall tilemap)
    void Start()
    {
        //FOR NOW -- this will change to support level loading.
        WallTilemap = GameObject.FindObjectOfType<WallTileMap>().GetComponent<Tilemap>();
        NewGame();
    }

    //Might want to make this protected with a public setter and getter instead so that the other objects
    //can't change the link to the tilemap.
    //Now, this consideration of using the GameManager might actually be better, because regardless of the
    //implementation here (whether we use getters and setters and all that), in the rest of the code (i.e. in
    //MazeMover) the reference to the tilemap is always the same (it will always look like GameManager.WallTileMap).
    static public Tilemap WallTilemap;

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        foreach (Pellet p in pellets)
        {
            p.gameObject.SetActive(true);
        } 

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            this.enemies[i].gameObject.SetActive(true);
        } 

        this.player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            this.enemies[i].gameObject.SetActive(false);
        }

        this.player.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

     public static void IncrementScore(int points)
    {
        score++;
    } 

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void EnemyEaten(Enemy enemy)
    {
        SetScore(score + enemy.points);
    }

    public void PlayerEaten()
    {
        this.player.gameObject.SetActive(false);
        SetLives(this.lives - 1);
        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        } 
        else
        {
            GameOver();
        } 
    }*/
    
}

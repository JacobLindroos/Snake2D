using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
	/* Todo:
	 * 
	 * public & private fields:
	 * [x] int x, y - holds the x- and y-position of the spawned food 
	 * [x] Gameobject Food - holds the food object
	 * [x] float updateTime - time set by user how often the food shall spawn
	 * [x] int borderTop, borderBot, borderRight, borderLeft - sets the values for the food to be spawned within
	 * [x] float spawnFoodTimer = 0 - stores the time for last spawned food
	 * [x] float spawnFoodTimerMax - time max to next spawned food
	 * 
	 * Method:
	 * [x] private void spawnHandler() - Handles the spawning of food in the game
	 * 
	 */


	#region Fields

	//int variables that stores x- and y-positions
	private int x;
	private int y;

	//holds a gameobject w. id Food
	public GameObject Food;

	//float variable w. id updatetime that holds a value of time until next spawn to happen, set by user in editor
	public float updateTime;

	//int variables that holds the borders of where gameobject food should be spawned within, to be set by user in editor
	public int BorderTop;
	public int BorderBot;
	public int BorderRight;
	public int BorderLeft;

	//float variable w. id spawnfoodtimer that stores the time to next food gameobject spawn
	private float spawnFoodTimer = 0;
	//float variable w. id spawnfoodtimermax that stores the time for when the next spawn should instantiate 
	private float spawnFoodTimerMax;

	#endregion


	private void Awake()
	{
		//spawnfoodtimermax sets equal to updatetime, updatetime is set by user in editor. So the user can decide how often food gameobject should be spawned
		spawnFoodTimerMax = updateTime;
	}


	private void Update() 
	{
			//Sets int x value to a random number between input value from user of borderleft to borderright, which means the food will get a random position to be spawned upon
			x = Random.Range(BorderLeft, BorderRight);
			//Sets int y value to a random number between input value from user of borderbot to bordertop, which means the food will get a random position to be spawned upon
			y = Random.Range(BorderBot, BorderTop);

			//Calls the method spawnHandler
			spawnHandler();
	}


	/// <summary>
	/// Handles when a new food gameobject should be spawned
	/// </summary>
	private void spawnHandler()
	{
		//Sets spawnfoodtimer plus equal to time passed since the last frame update
		spawnFoodTimer += Time.deltaTime;
		//IF spawnfoodtimer is greater or equal to spawnfoodtimermax
		if(spawnFoodTimer >= spawnFoodTimerMax)
		{
			//a new food gameobject is created and that food is spawned at a random x- and y-position
			GameObject f = Instantiate(Food, new Vector2(x, y), Quaternion.identity);
			//resets the spawnfoodtimer
			spawnFoodTimer -= spawnFoodTimerMax;
		}
	}
}


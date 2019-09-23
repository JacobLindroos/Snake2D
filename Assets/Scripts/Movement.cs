using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
	/* Todo:
	 * 
	 * private & public fields:
	 * [x] public Gameobject snakeTail - stores the gameobject which builds the tail of the snake
	 * [x] public float updateTime - stores the time for when to update the movedirection of the snake
	 * [x] private Vector2 gapPosition - stores the position where the snakehead was one frame ago when moving to a new position
	 * [x] private Vector2 gridMoveDirection - which position the snake will move to next
	 * [x] private Vector2 gridPosition - stores a position in the grid
	 * [x] private float gridMoveTimer - holds the time since the last fram update
	 * [x] private float gridMoveTimerMax - holds the max time until the next position update
	 * [x] privat bool ate - true/false depending if the snake ate food or not
	 * [x] private bool game - true/false dedpending if the snake crashed w. walls or it self
	 * [x] MyLinkedList<GameObject> tail - linked list that handles the snaketail gameobjects
	 * 
	 * Methods:
	 * [x] private void SnakeMove() - takes a input from user on which direction the snake should go next
	 * [x] private void gridHandler() - changes the movement of the snake on the grid and handles adding/removing snaketail from the list
	 * [x] private void OnTriggerEnter2D() - handles the snake´s collisions w. other gameobjects
	 * 
	 */


	#region Fields

	//creates a gameobject w. id snaketail
	public GameObject SnakeTail;
	//creates a data type of float w. id updatetime
	public float updateTime;

	//creates a new vector w. id gapPos
	private Vector2 gapPosition;
	//creates a new vector w. id gridmovedirection
	private Vector2 gridMoveDirection;
	//creates a new vector w. id gridposition
	private Vector2 gridPosition;
	
	//creates float variable w. id gridmovetimer that stores the time to next food gameobject spawn
	private float gridMoveTimer;
	// creates a float variable w. id gridmovetimermax that stores the time for when the next spawn should instantiate 
	private float gridMoveTimerMax;
	//creates a bool variable w. id ate that sets equal to false
	private bool ate = false;
	//creates a bool variable w. id game that sets equal to true
	private bool game = true;

	MyLinkedList<GameObject> tail = new MyLinkedList<GameObject>();

	#endregion


	private void Awake()
	{
		//snake´s start position, gets the position of gridPosition
		gridPosition = new Vector2(0, 0);
		//gridmovetimermax sets equal to updatetime, updatetime is set by user in editor
		gridMoveTimerMax = updateTime;
		//gridmovetimer sets equal to gridmovetimermax, which sets the snake to move every frame update
		gridMoveTimer = gridMoveTimerMax;
		//sets the movedirection of snake to the right at the beginning of the game
		gridMoveDirection = Vector2.right;
	}


	private void Update()
	{
		//calls the snakeMove method
		SnakeMove();

		//IF game is true
		if (game)
		{
			//the method gridhandler is called
			gridHandler();
		}
		else //ELSE
		{
			//the snake´s movement is set to zero, is paused
			gridMoveDirection = Vector2.zero;
		}

		//sets the new position of the snake 
		transform.position = new Vector3(gridPosition.x, gridPosition.y);
	}


	#region Methods for handling the snake

	/// <summary>
	/// Sets the movedirection of the snake
	/// </summary>
	private void SnakeMove()
	{
		#region Inputs the new direction of gameobject
		
		if (Input.GetKeyDown(KeyCode.UpArrow)) //IF input from user is up arrow
		{
			//IF not the movedirectin of the snake is moving down/negative on the y-axis, then the snake movedirection is changed uppwards
			if (gridMoveDirection.y != -1)
			{
				gridMoveDirection = Vector2.up;
			}
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow)) //ELSE IF input from user is down arrow
		{
			//IF not the movedirectin of the snake is moving up/positive on the y-axis, then the snake movedirection is changed downwards
			if (gridMoveDirection.y != +1)
			{
				gridMoveDirection = Vector2.down;
			}
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow)) //ELSE IF input from user is right arrow
		{
			//IF not the movedirectin of the snake is moving left/negative on the x-axis, then the snake movedirection is changed right
			if (gridMoveDirection.x != -1)
			{
				gridMoveDirection = Vector2.right;
			}
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow)) //ELSE IF input from user is left arrow
		{
			//IF not the movedirectin of the snake is moving right/positive on the x-axis, then the snake movedirection is changed left
			if (gridMoveDirection.x != +1)
			{
				gridMoveDirection = Vector2.left;
			}
		}
		#endregion
	}


	/// <summary>
	/// Handles the gridmovement of the snake
	/// </summary>
	private void gridHandler()
	{
		//Sets gridmovetimer plus equal to time passed since the last updated frame
		gridMoveTimer += Time.deltaTime;
		//IF gridmovetimer is greater or equal to gridmovetimermax
		if (gridMoveTimer >= gridMoveTimerMax)
		{
			//sets the gapPos to the snakehead position
			gapPosition = transform.position;
			//Sets gridposition plus equal to gridmovedir, snakehead gets a new position
			gridPosition += gridMoveDirection;
			//resets the gridmovetimer
			gridMoveTimer -= gridMoveTimerMax;

			//IF it´s true that the snake ate food
			if (ate)
			{
				//a new gameobject w. id snaketail is created in the linked list at the position where the snakehead was, lastly ate is set to false again
				GameObject g = Instantiate(SnakeTail, gapPosition, Quaternion.identity);
				tail.Add(0, g);
				ate = false;
			}
			else if (tail.Count > 0) //ELSE IF the linked list size is greater then zero, which means if there´s already exsist snaketails
			{
				//we get the last snaketail in the list, adds it first in the list, removes the reference to the last snaketail in the list and puts the last snaketail in the gapPosition the snakehead left behind
				GameObject g = tail.GetLast(tail.Count);
				tail.Add(0, tail.GetLast(tail.Count));
				tail.Remove(tail.Count - 1);
				g.transform.position = gapPosition;
			}
		}
	}


	/// <summary>
	/// Checks if player collides with other gameobjects
	/// </summary>
	/// <param name="collision"> w. walls, food or it´s tail </param>
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//IF the player collides w. gameobject Food
		if(collision.gameObject.tag == "Food")
		{
			//bool variable ate is set to true, Food gameobject is destroyed, text output in console that indicates that the player ate food
			ate = true;
			Destroy(collision.gameObject);
			Debug.Log("Snake ate some food");
		}
		else if (collision.gameObject.tag == "BorderTop") //ELSE IF player collides w. gameobject BorderTop
		{
			//time is paused, bool variable game is set to false which stops the game, text output in console that says game over
			Time.timeScale = 0;
			game = false;
			Debug.Log("GAME OVER - Border top crash");
		}
		else if (collision.gameObject.tag == "BorderBot") //ELSE IF player collides w. gameobject BorderBot
		{
			//time is paused, bool variable game is set to false which stops the game, text output in console that says game over
			game = false;
			Time.timeScale = 0;
			Debug.Log("GAME OVER - Border bot crash");
		}
		else if (collision.gameObject.tag == "BorderRight") //ELSE IF player collides w. gameobject BorderRight
		{
			//time is paused, bool variable game is set to false which stops the game, text output in console that says game over
			game = false;
			Time.timeScale = 0;
			Debug.Log("GAME OVER - Border right crash");
		}
		else if (collision.gameObject.tag == "BorderLeft") //ELSE IF player collides w. gameobject BorderLeft
		{
			//time is paused, bool variable game is set to false which stops the game, text output in console that says game over
			game = false;
			Time.timeScale = 0;
			Debug.Log("GAME OVER - Border left crash");
		}
		else if (collision.gameObject.tag == "SnakeTail") //ELSE IF player collides w. gameobject snakeTail
		{
			//time is paused, bool variable game is set to false which stops the game, text output in console that says game over
			game = false;
			Time.timeScale = 0;
			Debug.Log("GAME OVER - Snake crashed with it´s tail");
		}
	}

	#endregion

}


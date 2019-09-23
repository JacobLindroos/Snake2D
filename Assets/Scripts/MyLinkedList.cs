using System;
using System.Collections.Generic;
using UnityEngine;

public class MyLinkedList<T>
{
	/* Todo
	 * 
	 * Private fields:
	 * [x] Int count - keeps track of the size of the list
	 * [x] Node head - creates a node that represent the head-node/start-node
	 * 
	 * Public Constructor:
	 * [x] LinkedList() - creates a new list with count set to 0 and head to null
	 * 
	 * Public Properties:
	 * [x] Empty - returns true/false if list is empty or not
	 * [x] Count - gives the count value as output
	 * 
	 * Public Methods:
	 * [x] Add(int index, object o) - inserts a GameObject at the specified int index 
	 * [x] RemoveLast(int index, object o) - removes the GameObject in the END of the list
	 * [x] GetLast(int index) - gets the object in the END of the list
	 * 
	 */


	#region Fields

	//Node head reference to the first node in the list, int count keeps track of the list size
	private Node head;
	private int count;

	#endregion


	/// <summary>
	/// Constructor to MyLinkedList class, creates a new linked list
	/// </summary>
	public MyLinkedList()
	{
		//Sets the head-node to null and the count to 0
		this.head = null;
		this.count = 0;
	}


	#region Properties

	/// <summary>
	/// Checks if the list is empty or not, returns true/false depending if the list is empty or not
	/// </summary>
	public bool Empty
	{
		get { return this.count == 0; }
	}

	/// <summary>
	/// Gets the int count value, which size the list have
	/// </summary>
	public int Count
	{
		get { return this.count; }
	}

	#endregion


	#region Methods for handling linked list

	/// <summary>
	/// Adds an gameobject at a specified index
	/// </summary>
	/// <param name="index"> where to insert gameobject </param>
	/// <param name="g"> ref. to gameobject to insert </param>
	/// <returns> the inserted gameobject </returns>
	public GameObject Add(int index, GameObject g)
	{
		//IF index is less then zero, a exeption is thrown beacuse the index is outside the list count
		if (index < 0)
		{ throw new ArgumentOutOfRangeException($"Index {index}"); }

		//IF index is equal to zero OR the list is empty
		if(index == 0 || this.Empty)
		{
			//a new node is created an sets equal to the head-node and it´s link is connected to the old head-node
			this.head = new Node(g, this.head);
		}
		//Adds an value to the size count variable
		count++;
		//returns the added gameobject
		return g;
	}


	/// <summary>
	/// Removes ref. to a node at specified index
	/// </summary>
	/// <param name="index"> of node to remove ref. to </param>
	public void Remove(int index)
	{
		//IF index is less then zero, a exeption is thrown beacuse the index is outside the list count
		if (index < 0)
		{ throw new ArgumentOutOfRangeException($"Index {index}"); }

		//If index is greater or equal to int count value, the index value is set equal to count value minus one. Beacuse the list elemnet starts at 0
		if(index >= this.count)
		{ index = this.count - 1; }

		//creates a new node w. id current and sets equal to the head-node, gets the first node in the list
		Node current = this.head;

		//for-loop that gets the node before the one node to be removed, that´s why index is subtracted by 1
		for (int i = 0; i < index - 1; i++)
		{
			//sets the current node to the next node
			current = current.NextNode;
		}
		//sets current nodes link to the next next node, ref. to removed node is gone
		current.NextNode = current.NextNode.NextNode;
		//subtracts a value from count variable
		count--;
	}


	/// <summary>
	/// Gets a node at specified index in the list
	/// </summary>
	/// <param name="index"> of which node to get </param>
	/// <returns> node from index </returns>
	public GameObject GetLast(int index)
	{
		//IF index is less then zero, a exeption is thrown beacuse the index is outside the list count
		if (index < 0)
		{ throw new ArgumentOutOfRangeException($"Index {index}"); }

		//IF the list is empty then we can´t get any node and instead we return null
		if(Empty)
		{ return null; }

		//IF index is greater or equal to list size, index value is set to the list size value minus one. Beacuse we start at element 0
		if(index >= this.count)
		{ index = this.count - 1; }

		//creates a new node and sets equal to the head-node, gets the first first node in the list
		Node current = this.head;

		//for-loop that loops to the specified index and gets that node
		for (int i = 0; i < index; i++)
		{
			//current node is set to be equal to the next node
			current = current.NextNode;
		}
		//returns the datatype of the found node
		return current.Data;
	}

	#endregion

}


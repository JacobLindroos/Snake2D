using System.Collections.Generic;
using UnityEngine;
using System;

public class Node
{
	/* Todo
	 * Private fields:
	 * [x] Object data - holds the data type of the node
	 * [x] Node nextnode - holds the link to the next node
	 * 
	 * Public Constructor:
	 * [x] Node(object data, Node nextNode) - takes a data type and a node
	 * 
	 * Public properties:
	 * [x] Object Data - get/set the object data
	 * [x] Node NextNode - get/set the link node
	 */


	#region Fields

	//variable gameobject w. id data that stores the datatype of the node in the linked list
	private GameObject data;
	//variable Node w. id nextNode that stores the link to the next node in the linked list
	private Node nextNode;

	#endregion


	/// <summary>
	/// Constructor for Node class, creates a new node
	/// </summary>
	/// <param name="data"> gameobject connected to node </param>
	/// <param name="nextNode"> link to next node in list </param>
	public Node(GameObject data, Node nextNode)
	{
		this.data = data;
		this.nextNode = nextNode;
	}


	/// <summary>
	/// Get/set the datatype of the node
	/// </summary>
	public GameObject Data
	{
		get { return this.data; }
		set { this.data = null; }
	}


	/// <summary>
	/// Get/sets the link to the next node in the list
	/// </summary>
	public Node NextNode
	{
		get { return this.nextNode; }
		set { this.nextNode = value; }
	}
}


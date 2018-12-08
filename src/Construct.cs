using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construct : MonoBehaviour
{

	public Game Game;
	public Inventory InventoryInput; // Private inventory, this is also where stuff gets dumped
	public Inventory InventoryOutput; // Public inventory, everyone can take out of here

	public bool PrivateInventory = true; // Use InventoryInput or not, se above
	public bool Interactable = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetGame(Game game)
	{
		Game = game;
	}
	
	public virtual void UpdateNeighbours()
	{
		
	}

	public virtual bool OnConstructRightClick(Player player)
	{
		return false;
	}

	public virtual bool OnConstructLeftClick(Player player)
	{
		Game.RemoveConstruct(this);
		return false;
	}
}

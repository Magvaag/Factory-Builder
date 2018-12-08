using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Construct
{
	
	// Use this for initialization
	void Start ()
	{
		PrivateInventory = false;
		Interactable = true;
		InventoryOutput = new Inventory(15);
		InventoryOutput.AddItemStack(new ItemStack(Item.STONE, 20));
		Create();
	}

	private void Create()
	{
		GameObject.Find("World").GetComponent<Game>().PlaceConstruct(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override bool OnConstructRightClick(Player player)
	{
		Game.UiInventoryChest.Show(InventoryOutput);
		return true;
	}
}

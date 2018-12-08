using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConveyorBelt : Construct
{

	// The construct connected behind this conveyor belt
	private Construct ConstructFrom;
	private Construct ConstructTo;
	
	// Use this for initialization
	void Start () {
		InventoryInput = new Inventory(5);
		InventoryOutput = new Inventory(5);
		Create();
	}
	
	// Update is called once per frame
	void Update ()
	{
		RetrieveItems();
		PushItems();
		UpdateItems();
	}

	private void Create()
	{
		GameObject.Find("World").GetComponent<Game>().PlaceConstruct(this);
	}

	private void RetrieveItems()
	{
		if (ConstructFrom == null) return;
		
		// Finds items from the construct behind this conveyor belt, and takes them into the input inventory.
		var itemStack = ConstructFrom.InventoryOutput.GetFirstItem();
		if (itemStack != null)
		{
			InventoryInput.AddItemStack(itemStack);
		}
	}

	private void PushItems()
	{
		if (ConstructTo == null) return;
		
		// Finds items from the construct behind this conveyor belt, and takes them into the input inventory.
		var itemStack = InventoryOutput.GetFirstItem();
		if (itemStack != null)
		{
			if (ConstructTo.PrivateInventory) ConstructTo.InventoryInput.AddItemStack(itemStack);
			else ConstructTo.InventoryOutput.AddItemStack(itemStack);
		}
	}

	private void UpdateItems()
	{
		InventoryOutput.AddItemStacks(InventoryInput.ItemStacks.ToList());
		InventoryInput.Clear();
	}

	public override void UpdateNeighbours()
	{
		// Find the object behind the conveyor belt
		var construct = Game.GetConstructAt(transform.position - transform.forward);
		ConstructFrom = construct;
		
		// Find the object behind the conveyor belt
		construct = Game.GetConstructAt(transform.position + transform.forward);
		ConstructTo = construct;
	}
}

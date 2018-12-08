using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public List<Construct> Constructs = new List<Construct>();

	public Player Player;
	public Prefabs Prefabs;
	
	public UIInventoryChest UiInventoryChest;
	public UIPlayerInventory UiPlayerInventory;
	
	// Use this for initialization
	void Start ()
	{
		// UiPlayerInventory.Player = Player;
		Prefabs = GetComponent<Prefabs>();
		Item.CONVEYOR_BELT.SetPrefab(Prefabs.PrefabConveyorBelt);
		Item.CONVEYOR_BELT.SetRenderObject(Prefabs.PrefabConveyorBeltRenderObject);
		Item.CHEST.SetPrefab(Prefabs.PrefabChest);
		Item.CHEST.SetRenderObject(Prefabs.PrefabChestRenderObject);
		Item.STONE.SetRenderObject(Prefabs.PrefabStoneRenderObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaceConstruct(Construct construct)
	{
		construct.SetGame(this);
		Constructs.Add(construct);
		UpdateNeighbours(construct);
	}

	public void RemoveConstruct(Construct construct)
	{
		Constructs.Remove(construct);
		Destroy(construct.gameObject);
	}

	public void UpdateNeighbours(Construct construct)
	{
		var c1 = GetConstructAt(construct.transform.position + construct.transform.forward);
		if (c1 != null) c1.UpdateNeighbours();
		
		c1 = GetConstructAt(construct.transform.position - construct.transform.forward);
		if (c1 != null) c1.UpdateNeighbours();
		
		c1 = GetConstructAt(construct.transform.position + construct.transform.right);
		if (c1 != null) c1.UpdateNeighbours();
		
		c1 = GetConstructAt(construct.transform.position - construct.transform.right);
		if (c1 != null) c1.UpdateNeighbours();
		
		c1 = GetConstructAt(construct.transform.position + construct.transform.up);
		if (c1 != null) c1.UpdateNeighbours();
		
		c1 = GetConstructAt(construct.transform.position - construct.transform.up);
		if (c1 != null) c1.UpdateNeighbours();
		
		construct.UpdateNeighbours();
	}

	public Construct GetConstructAt(Vector3 position)
	{
		foreach (var construct in Constructs)
		{
			if (construct.transform.position == position)
			{
				return construct;
			}
		}
		
		return null;
	}
}

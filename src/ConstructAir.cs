using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructAir : Construct {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override bool OnConstructRightClick(Player player)
	{
		var item = player.Inventory.ItemStacks[player.GetSelectedSlot()];
		if (item != null && item.item != null && item.item.Prefab != null)
		{
			var go = Instantiate(item.item.Prefab);
			go.transform.position = transform.position;

			var direction = player.transform.rotation.eulerAngles;
			direction.x = 0;
			direction.y /= 90;
			direction.y = (float) Math.Round(direction.y);
			direction.y *= 90;
			direction.z = 0;
			go.transform.rotation = Quaternion.Euler(direction);
		}
		return true;
	}
	
	public override bool OnConstructLeftClick(Player player)
	{
		return true;
	}
}

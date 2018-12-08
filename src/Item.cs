using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item {

    public static Item EMPTY = new Item("Empty", new Color(.2f, .2f, .2f, 1));
    public static Item WHEAT = new Item("Wheat", new Color(0.8679245f, 0.761427f, 0f, 1));
    public static Item BREAD = new Item("Bread", new Color(1, 1, 1, 1));
    public static Item WOOD = new Item("Wood", new Color(0.6320754f, 0.3827362f, 0.134167f, 1));
    public static Item STONE = new Item("Stone", new Color(.5f, .5f, .5f, 1));
	public static Item PLANKS = new Item("Planks", new Color(0.8207547f, 0.6782169f, 0.1897027f, 1));
	public static Item CONVEYOR_BELT = new Item("Conveyor Belt", new Color(.2f, .4f, .8f));
	public static Item CHEST = new Item("Chest", new Color(.2f, .4f, .8f));
	
    public string name;
	public Color Color;
	public GameObject Prefab;
	public GameObject RenderObject;

    public Item(string name, Color color)
    {
        this.name = name;
	    Color = color;
    }

	public void SetPrefab(GameObject prefab)
	{
		Prefab = prefab;
	}
	
	public void SetRenderObject(GameObject renderObject)
	{
		RenderObject = renderObject;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

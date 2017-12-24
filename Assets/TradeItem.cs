using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TradeItem {
	public ItemType type;
	public int localCost;
}

public enum ItemType {Hydrogen,Nitrogen,Oxygen,MetalOre,Steel,Electronics,Organics,Plutonium,Waste,Gold}
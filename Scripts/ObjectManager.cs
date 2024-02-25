using System;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
	private void Start()
	{
		ObjectManager.Char_Pab = (Resources.Load("Prefab/Char") as GameObject);
		ObjectManager.Gold_Pab = (Resources.Load("Prefab/Gold") as GameObject);
		ObjectManager.Pot_Pab = (Resources.Load("Prefab/Pot") as GameObject);
		ObjectManager.Patic = (Resources.Load("Prefab/Particle") as GameObject);
		ObjectManager.end = (Resources.Load("Prefab/End") as GameObject);
		ObjectManager.Tutorial = (Resources.Load("Prefab/Tutorial") as GameObject);
		ObjectManager.Legend = (Resources.Load("Prefab/Legend") as GameObject);
	}

	public static GameObject Char_Pab;

	public static GameObject Gold_Pab;

	public static GameObject Pot_Pab;

	public static GameObject Patic;

	public static GameObject end;

	public static GameObject Tutorial;

	public static GameObject Legend;
}

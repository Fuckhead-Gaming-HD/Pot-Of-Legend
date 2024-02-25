using System;
using UnityEngine;

public class Title : MonoBehaviour
{
	private void Start()
	{
		for (int i = 0; i < 10; i++)
		{
			Title.Collection[i] = transform.Find("Collection_" + i).gameObject;
		}
	}

	private void Update()
	{
		for (int i = 0; i < 10; i++)
		{
			if (FileManager.Collection[i] == 1)
			{
				Title.Collection[i].SetActive(true);
			}
			else
			{
				Title.Collection[i].SetActive(false);
			}
		}
		if (Input.GetMouseButtonDown(0) && Title.ZeroOn && Title.LogoDonTouch && Physics2D.Raycast(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero, float.PositiveInfinity, 1 << LayerMask.NameToLayer("menu")))
		{
			string name = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero, float.PositiveInfinity, 1 << LayerMask.NameToLayer("menu")).collider.name;
			switch (name)
			{
			case "Continue":
				FileManager.DataOpen();
				AudioSource.PlayClipAtPoint(FileManager.Wind, Vector3.zero);
				GameObject.Find("Gage").GetComponent<PotReGen>().enabled = true;
				Ending.DontGold = false;
				this.AllCharGen();
				CameraMove.GamePlay = 2;
				break;
			case "Restart":
				if (Social.localUser.authenticated)
				{
					FileManager.SendRank();
					// NerdGPG.Instance().showAllLeaderBoards(); // Removed NerdGPG reference
				}
				else
				{
					Social.localUser.Authenticate(new Action<bool>(this.OnAuthCB));
				}
				break;
			case "Credit":
				AudioSource.PlayClipAtPoint(FileManager.Wind, Vector3.zero);
				CameraMove.GamePlay = 0;
				break;
			}
		}
	}

	private void OnAuthCB(bool result)
	{
		ScreenLog.Log("GPGUI: Got Login Response: " + result);
	}

	private void AllCharGen()
	{
		GameObject[] array = new GameObject[FileManager.HeroLimit];
		for (int i = 0; i < FileManager.HeroLimit; i++)
		{
			array[i] = (UnityEngine.Object.Instantiate(ObjectManager.Char_Pab) as GameObject);
			array[i].transform.parent = GameObject.Find("CharManager").transform;
			array[i].transform.position = GameObject.Find("Door").transform.position;
			array[i].GetComponent<Player>().Index = i;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(ObjectManager.Legend) as GameObject;
	}

	public static GameObject[] Collection = new GameObject[10];

	public static bool ZeroOn;

	public static bool LogoDonTouch = false;
}

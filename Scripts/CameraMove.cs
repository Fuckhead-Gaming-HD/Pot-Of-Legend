using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CameraMove : MonoBehaviour
{
	private void Start()
	{
		// Removed reference to GPGSocial
		if (Application.platform == RuntimePlatform.Android)
		{
			AdvertisementHandler.Instantiate("fuckme500", AdvertisementHandler.AdvSize.IAB_LEADERBOARD, AdvertisementHandler.AdvOrientation.HORIZONTAL, AdvertisementHandler.Position.BOTTOM, AdvertisementHandler.Position.LEFT, false, AdvertisementHandler.AnimationInType.SLIDE_IN_LEFT, AdvertisementHandler.AnimationOutType.FADE_OUT, AdvertisementHandler.LevelOfDebug.LOW);
		}
		if (Application.platform == RuntimePlatform.Android)
		{
			AdvertisementHandler.EnableAds();
			AdvertisementHandler.HideAds();
		}
		CameraMove.CharManager = GameObject.Find("CharManager");
		CameraMove.PotManager = GameObject.Find("PotManager");
		CameraMove.GoldManager = GameObject.Find("GoldManager");
	}

	private void Update()
	{
		if (CameraMove.GamePlay == 1)
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
		}
		else if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			this.TitleGo();
		}
		switch (CameraMove.GamePlay)
		{
		case 0:
			if (base.transform.position.y > -30.29f)
			{
				base.transform.position = new Vector3(0f, base.transform.position.y + (-30.3f - base.transform.position.y) * 0.09f, -10f);
			}
			else
			{
				base.transform.position = new Vector3(0f, -30.3f, -10f);
				if (Input.GetMouseButtonDown(0))
				{
					AudioSource.PlayClipAtPoint(FileManager.Wind, Vector3.zero);
					CameraMove.GamePlay = 1;
				}
			}
			break;
		case 1:
			if (base.transform.position.y > -15.29f || base.transform.position.y < -15.31f)
			{
				Title.ZeroOn = false;
				base.transform.position = new Vector3(0f, base.transform.position.y + (-15.3f - base.transform.position.y) * 0.09f, -10f);
			}
			else
			{
				Title.ZeroOn = true;
				base.transform.position = new Vector3(0f, -15.3f, -10f);
				for (int i = CameraMove.CharManager.transform.childCount; i > 0; i--)
				{
					UnityEngine.Object.DestroyObject(CameraMove.CharManager.transform.GetChild(i - 1).gameObject);
				}
				for (int j = CameraMove.PotManager.transform.childCount; j > 0; j--)
				{
					UnityEngine.Object.DestroyObject(CameraMove.PotManager.transform.GetChild(j - 1).gameObject);
				}
				for (int k = CameraMove.GoldManager.transform.childCount; k > 0; k--)
				{
					UnityEngine.Object.DestroyObject(CameraMove.GoldManager.transform.GetChild(k - 1).gameObject);
				}
				if (GameObject.Find("Legend(Clone)"))
				{
					UnityEngine.Object.Destroy(GameObject.Find("Legend(Clone)").gameObject);
				}
				PotReGen.Regen = 0f;
				PotReGen.GageAni = 0f;
				GameObject.Find("Gage").transform.localScale = new Vector2(0f, 10f);
			}
			break;
		case 2:
			Ranking.Rank_PlayTime += Time.deltaTime;
			if (base.transform.position.y < -0.01f)
			{
				base.transform.position = new Vector3(0f, base.transform.position.y * 0.9f, -10f);
			}
			else
			{
				base.transform.position = new Vector3(0f, 0f, -10f);
			}
			break;
		}
	}

	private void TitleGo()
	{
		CameraMove.GamePlay = 1;
		Title.ZeroOn = true;
		FileManager.MainII();
		for (int i = CameraMove.CharManager.transform.childCount; i > 0; i--)
		{
			UnityEngine.Object.DestroyObject(CameraMove.CharManager.transform.GetChild(i - 1).gameObject);
		}
		for (int j = CameraMove.PotManager.transform.childCount; j > 0; j--)
		{
			UnityEngine.Object.DestroyObject(CameraMove.PotManager.transform.GetChild(j - 1).gameObject);
		}
		for (int k = CameraMove.GoldManager.transform.childCount; k > 0; k--)
		{
			UnityEngine.Object.DestroyObject(CameraMove.GoldManager.transform.GetChild(k - 1).gameObject);
		}
		if (GameObject.Find("Legend(Clone)"))
		{
			UnityEngine.Object.Destroy(GameObject.Find("Legend(Clone)").gameObject);
		}
		PotReGen.Regen = 0f;
		PotReGen.GageAni = 0f;
		GameObject.Find("Gage").transform.localScale = new Vector2(0f, 10f);
		GameObject.Find("Gage").GetComponent<PotReGen>().enabled = false;
		Shop.OnShop = false;
		UnityEngine.Object.Destroy(GameObject.Find("Tutorial(Clone)"));
		Ending.anim = 0;
		if (Application.platform == RuntimePlatform.Android)
		{
			AdvertisementHandler.HideAds();
		}
	}

	private void OnApplicationPause(bool Pause)
	{
		if (Pause)
		{
			FileManager.SendRank();
			base.transform.position = new Vector3(0f, -15.3f, -10f);
			this.TitleGo();
		}
	}

	public static int GamePlay = 1;
	public static GameObject CharManager;
	public static GameObject PotManager;
	public static GameObject GoldManager;
}

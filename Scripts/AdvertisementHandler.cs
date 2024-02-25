using System;
using UnityEngine;

public class AdvertisementHandler : MonoBehaviour
{
	public static void Instantiate(string pubID, AdvertisementHandler.AdvSize advSize, AdvertisementHandler.AdvOrientation advOrient, AdvertisementHandler.Position position_1, AdvertisementHandler.Position position_2, bool isTesting, AdvertisementHandler.AnimationInType animIn, AdvertisementHandler.AnimationOutType animOut, AdvertisementHandler.LevelOfDebug levelOfDebug)
	{
		AdvertisementHandler.admobPluginClass = new AndroidJavaClass("com.microeyes.admob.AdmobActivity");
		AdvertisementHandler.unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AdvertisementHandler.currActivity = AdvertisementHandler.unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		AdvertisementHandler.admobPluginClass.CallStatic("AdvHandler", new object[]
		{
			0,
			AdvertisementHandler.currActivity,
			pubID,
			(int)advSize,
			(int)advOrient,
			(int)position_1,
			(int)position_2,
			isTesting,
			(int)animIn,
			(int)animOut,
			(int)levelOfDebug
		});
	}

	public static void EnableAds()
	{
		AdvertisementHandler.admobPluginClass.CallStatic("AdvHandler", new object[]
		{
			2,
			AdvertisementHandler.currActivity,
			string.Empty,
			-1,
			-1,
			-1,
			-1,
			false,
			-1,
			-1,
			-1
		});
	}

	public static void DisableAds()
	{
		AdvertisementHandler.admobPluginClass.CallStatic("AdvHandler", new object[]
		{
			1,
			AdvertisementHandler.currActivity,
			string.Empty,
			-1,
			-1,
			-1,
			-1,
			false,
			-1,
			-1,
			-1
		});
	}

	public static void HideAds()
	{
		AdvertisementHandler.admobPluginClass.CallStatic("AdvHandler", new object[]
		{
			3,
			AdvertisementHandler.currActivity,
			string.Empty,
			-1,
			-1,
			-1,
			-1,
			false,
			-1,
			-1,
			-1
		});
	}

	public static void ShowAds()
	{
		AdvertisementHandler.admobPluginClass.CallStatic("AdvHandler", new object[]
		{
			4,
			AdvertisementHandler.currActivity,
			string.Empty,
			-1,
			-1,
			-1,
			-1,
			false,
			-1,
			-1,
			-1
		});
	}

	public static void RepositionAds(AdvertisementHandler.Position position_1, AdvertisementHandler.Position position_2)
	{
		AdvertisementHandler.admobPluginClass.CallStatic("AdvHandler", new object[]
		{
			5,
			AdvertisementHandler.currActivity,
			string.Empty,
			-1,
			-1,
			(int)position_1,
			(int)position_2,
			false,
			-1,
			-1,
			-1
		});
	}

	private static AndroidJavaClass admobPluginClass;

	private static AndroidJavaClass unityPlayer;

	private static AndroidJavaObject currActivity;

	public enum AdvSize
	{
		BANNER,
		IAB_MRECT,
		IAB_BANNER,
		IAB_LEADERBOARD,
		DEVICE_WILL_DECIDE
	}

	public enum AdvOrientation
	{
		VERTICAL,
		HORIZONTAL
	}

	public enum Position
	{
		NO_GRAVITY,
		CENTER_HORIZONTAL,
		LEFT = 3,
		RIGHT = 5,
		FILL_HORIZONTAL = 7,
		CENTER_VERTICAL = 16,
		CENTER,
		TOP = 48,
		BOTTOM = 80,
		FILL_VERTICAL = 112
	}

	public enum AnimationInType
	{
		SLIDE_IN_LEFT,
		FADE_IN
	}

	public enum AnimationOutType
	{
		SLIDE_OUT_RIGHT,
		FADE_OUT
	}

	public enum Activity
	{
		INSTANTIATE,
		DISABLE,
		ENABLE,
		HIDE,
		SHOW,
		REPOSITION
	}

	public enum LevelOfDebug
	{
		NONE,
		LOW,
		HIGH
	}
}

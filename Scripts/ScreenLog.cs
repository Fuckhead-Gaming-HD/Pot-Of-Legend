using System;
using UnityEngine;

public class ScreenLog : MonoBehaviour
{
	private void Awake()
	{
		if (this.MustView)
		{
			ScreenLog.DevelopMode = true;
		}
		if (!ScreenLog.DevelopMode)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (ScreenLog.MakeScreenLog)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		ScreenLog.MakeScreenLog = true;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Menu) || UnityEngine.Input.GetKeyDown(KeyCode.L))
		{
			if (this.LogOn)
			{
				this.LogOn = false;
			}
			else
			{
				this.LogOn = true;
			}
		}
	}

	public static void Log(string str)
	{
		if (!ScreenLog.DevelopMode)
		{
			return;
		}
		string logStr = ScreenLog.LogStr;
		ScreenLog.LogStr = string.Concat(new object[]
		{
			logStr,
			"[",
			DateTime.Now,
			"] ",
			str,
			"\n"
		});
	}

	public static void Log(int str)
	{
		if (!ScreenLog.DevelopMode)
		{
			return;
		}
		string logStr = ScreenLog.LogStr;
		ScreenLog.LogStr = string.Concat(new object[]
		{
			logStr,
			"[",
			DateTime.Now,
			"] ",
			str,
			"\n"
		});
	}

	private void OnGUI()
	{
		if (this.LogOn)
		{
			this.Window_Rect = GUI.Window(1, this.Window_Rect, new GUI.WindowFunction(this.DoMyWindow), ScreenLog.LogStr);
			if (GUI.Button(new Rect(250f, 200f, 150f, 50f), "Log Reset"))
			{
				ScreenLog.LogStr = string.Empty;
			}
			if (GUI.Button(new Rect(250f, 300f, 40f, 40f), "X+"))
			{
				this.Window_Rect.width = this.Window_Rect.width + 300f;
			}
			if (GUI.Button(new Rect(300f, 300f, 40f, 40f), "X-"))
			{
				this.Window_Rect.width = this.Window_Rect.width - 300f;
			}
			if (GUI.Button(new Rect(250f, 400f, 40f, 40f), "Y+"))
			{
				this.Window_Rect.height = this.Window_Rect.height + 300f;
			}
			if (GUI.Button(new Rect(300f, 400f, 40f, 40f), "Y-"))
			{
				this.Window_Rect.height = this.Window_Rect.height - 300f;
			}
		}
	}

	private void DoMyWindow(int windowID)
	{
		GUI.DragWindow();
	}

	public bool MustView;

	public static bool DevelopMode;

	public static bool MakeScreenLog;

	public bool LogOn;

	public static string LogStr = string.Empty;

	private int StartX;

	private int StartY;

	private int ScaleX = 500;

	private int ScaleY = 400;

	private Rect Window_Rect = new Rect(50f, 50f, 400f, 400f);
}

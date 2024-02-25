using System;
using UnityEngine;

public class UICameraScreenSetting : MonoBehaviour
{
	private void Start()
	{
		float orthographicSize;
		if ((float)Screen.width / (float)Screen.height <= 1.8f)
		{
			orthographicSize = 1.77f / ((float)Screen.width / (float)Screen.height) * 7.5f;
		}
		else
		{
			orthographicSize = 7.5f;
		}
		base.gameObject.GetComponent<Camera>().orthographicSize = orthographicSize;
	}
}

using System;
using UnityEngine;

public class CointSOund : MonoBehaviour
{
	private void Update()
	{
		if (CointSOund.PlayTime > 0f)
		{
			base.GetComponent<AudioSource>().enabled = true;
			base.GetComponent<AudioSource>().volume = CointSOund.PlayTime * 10f;
			CointSOund.PlayTime -= Time.deltaTime;
		}
		else
		{
			base.GetComponent<AudioSource>().enabled = false;
		}
	}

	public static float PlayTime;
}

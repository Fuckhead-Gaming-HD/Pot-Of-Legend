using System;
using UnityEngine;

public class Logo : MonoBehaviour
{
	private void Start()
	{
		this.Fade = base.transform.FindChild("Fade").gameObject;
		FileManager.MainII();
	}

	private void Update()
	{
		switch (this.LogoSlide)
		{
		case 0:
			this.Delay += Time.deltaTime;
			if (this.Delay > 1f)
			{
				this.Delay = 0f;
				this.LogoSlide = 1;
			}
			break;
		case 1:
			this.Delay += Time.deltaTime;
			this.Fade.GetComponent<SpriteRenderer>().color = new Color(this.Delay / 2f, this.Delay / 2f, this.Delay / 2f);
			if (this.Delay > 2f)
			{
				this.Delay = 0f;
				this.LogoSlide = 2;
			}
			break;
		case 2:
			this.Delay += Time.deltaTime;
			if (this.Delay > 1f)
			{
				this.Delay = 0f;
				this.LogoSlide = 3;
			}
			break;
		case 3:
			this.Delay += Time.deltaTime;
			this.Fade.GetComponent<SpriteRenderer>().color = new Color(1f - this.Delay / 2f, 1f - this.Delay / 2f, 1f - this.Delay / 2f);
			if (this.Delay > 2f)
			{
				this.Delay = 0f;
				this.LogoSlide = 4;
			}
			break;
		case 4:
			this.Delay += Time.deltaTime;
			this.Fade.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f - this.Delay * 2f);
			if (this.Delay > 0.5f)
			{
				base.GetComponent<Logo>().enabled = false;
				UnityEngine.Object.Destroy(this.Fade);
				Title.LogoDonTouch = true;
				this.LogoSlide = 5;
			}
			break;
		}
	}

	private float Delay;

	private int LogoSlide;

	private GameObject Fade;
}

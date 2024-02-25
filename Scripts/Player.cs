using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	private void Start()
	{
		this.Weapon = base.transform.FindChild("Weapon").gameObject;
		this.F1up = GameObject.Find("Flour1Up").gameObject;
		this.F2up = GameObject.Find("Flour2Up").gameObject;
		this.F2down = GameObject.Find("Flour2Down").gameObject;
		this.F3down = GameObject.Find("Flour3Down").gameObject;
	}

	private void Update()
	{
		if (!Shop.OnShop)
		{
			if (this.Target == null)
			{
				this.Target = this.FindMinPot(GameObject.Find("PotManager").transform);
				this.Pick = false;
				this.STATE = Player.eSTATE.idle;
			}
			else if (this.Target.GetComponent<Pot>().Flour == this.Flour)
			{
				this.HorizonMove();
			}
			else
			{
				this.ChangeFlour();
			}
			if (this.Pick)
			{
				this.Attack();
			}
			this.Ani_Play();
		}
	}

	private GameObject FindMinPot(Transform Manager)
	{
		GameObject gameObject = null;
		for (int i = 0; i < Manager.childCount; i++)
		{
			if (!Manager.GetChild(i).GetComponent<Pot>().Pick)
			{
				gameObject = Manager.GetChild(i).gameObject;
				gameObject.GetComponent<Pot>().Pick = true;
				break;
			}
		}
		return gameObject;
	}

	private void Attack()
	{
		this.Delay += Time.deltaTime;
		if (this.Delay > this.DEX)
		{
			if (this.Target != null)
			{
				this.Delay = 0f;
			}
			AudioSource.PlayClipAtPoint(FileManager.Sword, Vector3.zero, 0.7f);
			this.AniFrame = 0;
			this.Deltatime = 0f;
			this.STATE = Player.eSTATE.attack;
			this.NowSTATE = Player.eSTATE.attack;
		}
	}

	private void AniFrameZero()
	{
		if (this.NowSTATE != this.STATE)
		{
			this.AniFrame = 0;
			this.Deltatime = 0f;
			this.NowSTATE = this.STATE;
		}
	}

	private void HorizonMove()
	{
		if (Mathf.Abs(base.transform.position.x - this.Target.transform.position.x) > this.REN)
		{
			if (!this.Pick)
			{
				this.Tranmove(this.Target);
			}
		}
		else
		{
			this.Pick = true;
			if (base.transform.position.x > this.Target.transform.position.x)
			{
				base.transform.position = new Vector2(base.transform.position.x + this.MoveSpeed / 4f * Time.deltaTime, base.transform.position.y);
				base.transform.localScale = new Vector3(10f, 10f, 0f);
			}
			else
			{
				base.transform.position = new Vector2(base.transform.position.x - this.MoveSpeed / 4f * Time.deltaTime, base.transform.position.y);
				base.transform.localScale = new Vector3(-10f, 10f, 0f);
			}
			this.STATE = Player.eSTATE.idle;
		}
	}

	private void Tranmove(GameObject MovePoint)
	{
		if (base.transform.position.x - MovePoint.transform.position.x > 0f)
		{
			base.transform.position = new Vector2(base.transform.position.x - this.MoveSpeed * Time.deltaTime, base.transform.position.y);
			base.transform.localScale = new Vector3(10f, 10f, 0f);
		}
		else
		{
			base.transform.position = new Vector2(base.transform.position.x + this.MoveSpeed * Time.deltaTime, base.transform.position.y);
			base.transform.localScale = new Vector3(-10f, 10f, 0f);
		}
		this.STATE = Player.eSTATE.move;
	}

	private void ChangeFlour()
	{
		float num = 0.3f;
		switch (this.Flour)
		{
		case 1:
			if (Mathf.Abs(base.transform.position.x - this.F1up.transform.position.x) > num)
			{
				this.Tranmove(this.F1up);
			}
			else
			{
				this.TranUp(this.F1up);
			}
			break;
		case 2:
			if (this.Target.GetComponent<Pot>().Flour > this.Flour)
			{
				if (Mathf.Abs(base.transform.position.x - this.F2up.transform.position.x) > num)
				{
					this.Tranmove(this.F2up);
				}
				else
				{
					this.TranUp(this.F2up);
				}
			}
			else if (Mathf.Abs(base.transform.position.x - this.F2down.transform.position.x) > num)
			{
				this.Tranmove(this.F2down);
			}
			else
			{
				this.TranDown(this.F2down);
			}
			break;
		case 3:
			if (Mathf.Abs(base.transform.position.x - this.F3down.transform.position.x) > num)
			{
				this.Tranmove(this.F3down);
			}
			else
			{
				this.TranDown(this.F3down);
			}
			break;
		}
	}

	private void TranDown(GameObject FlourPos)
	{
		if (base.transform.position.y > this.FlourZero(this.Flour - 1))
		{
			base.transform.position = new Vector2(FlourPos.transform.position.x, base.transform.position.y - this.MoveSpeed * Time.deltaTime);
			this.STATE = Player.eSTATE.action;
		}
		else
		{
			base.transform.position = new Vector2(FlourPos.transform.position.x, this.FlourZero(this.Flour - 1));
			this.STATE = Player.eSTATE.idle;
			this.Flour--;
		}
	}

	private void TranUp(GameObject FlourPos)
	{
		if (base.transform.position.y < this.FlourZero(this.Flour + 1))
		{
			base.transform.position = new Vector2(FlourPos.transform.position.x, base.transform.position.y + this.MoveSpeed * Time.deltaTime);
			this.STATE = Player.eSTATE.action;
		}
		else
		{
			base.transform.position = new Vector2(FlourPos.transform.position.x, this.FlourZero(this.Flour + 1));
			this.STATE = Player.eSTATE.idle;
			this.Flour++;
		}
	}

	private float FlourZero(int Temp)
	{
		float result = 0f;
		switch (Temp)
		{
		case 1:
			result = -6.4f;
			break;
		case 2:
			result = -2.3f;
			break;
		case 3:
			result = 1.8f;
			break;
		}
		return result;
	}

	private void Ani_Play()
	{
		this.Deltatime += Time.deltaTime;
		this.AniFrameZero();
		switch (this.NowSTATE)
		{
		case Player.eSTATE.idle:
			this.Ani_Idle();
			break;
		case Player.eSTATE.move:
			this.Ani_Move(0.07f);
			break;
		case Player.eSTATE.attack:
			this.Ani_Attack(0.07f);
			break;
		case Player.eSTATE.action:
			this.Ani_Action(0.07f);
			break;
		}
	}

	private void Ani_Action(float AniSpeed)
	{
		if (this.Deltatime > AniSpeed)
		{
			this.AniFrame++;
			this.Deltatime = 0f;
		}
		int aniFrame = this.AniFrame;
		if (aniFrame != 0)
		{
			if (aniFrame != 1)
			{
				this.AniFrame = 0;
			}
			else
			{
				base.GetComponent<SpriteRenderer>().sprite = FileManager.CharSprite[5 + this.Index * 8];
				this.Weapon.GetComponent<SpriteRenderer>().sprite = FileManager.WeaponSprite[3 + FileManager.HeroWeapon[this.Index] * 4];
			}
		}
		else
		{
			base.GetComponent<SpriteRenderer>().sprite = FileManager.CharSprite[4 + this.Index * 8];
			this.Weapon.GetComponent<SpriteRenderer>().sprite = FileManager.WeaponSprite[3 + FileManager.HeroWeapon[this.Index] * 4];
		}
	}

	private void Ani_Idle()
	{
		base.GetComponent<SpriteRenderer>().sprite = FileManager.CharSprite[0 + this.Index * 8];
		this.Weapon.GetComponent<SpriteRenderer>().sprite = FileManager.WeaponSprite[0 + FileManager.HeroWeapon[this.Index] * 4];
	}

	private void Ani_Move(float AniSpeed)
	{
		if (this.Deltatime > AniSpeed)
		{
			this.AniFrame++;
			this.Deltatime = 0f;
		}
		this.Weapon.GetComponent<SpriteRenderer>().sprite = FileManager.WeaponSprite[0 + FileManager.HeroWeapon[this.Index] * 4];
		switch (this.AniFrame)
		{
		case 0:
			base.GetComponent<SpriteRenderer>().sprite = FileManager.CharSprite[1 + this.Index * 8];
			break;
		case 1:
			base.GetComponent<SpriteRenderer>().sprite = FileManager.CharSprite[2 + this.Index * 8];
			break;
		case 2:
			base.GetComponent<SpriteRenderer>().sprite = FileManager.CharSprite[3 + this.Index * 8];
			break;
		case 3:
			base.GetComponent<SpriteRenderer>().sprite = FileManager.CharSprite[0 + this.Index * 8];
			break;
		default:
			this.AniFrame = 0;
			break;
		}
	}

	private void Ani_Attack(float AniSpeed)
	{
		if (this.Deltatime > AniSpeed)
		{
			this.AniFrame++;
			this.Deltatime = 0f;
		}
		int aniFrame = this.AniFrame;
		if (aniFrame != 0)
		{
			if (aniFrame != 1)
			{
				if (this.Target != null && this.Pick)
				{
					this.Target.GetComponent<Pot>().HP -= (int)Mathf.Pow(2.6f, (float)FileManager.HeroWeapon[this.Index]);
				}
				this.STATE = Player.eSTATE.idle;
			}
			else
			{
				base.GetComponent<SpriteRenderer>().sprite = FileManager.CharSprite[7 + this.Index * 8];
				this.Weapon.GetComponent<SpriteRenderer>().sprite = FileManager.WeaponSprite[2 + FileManager.HeroWeapon[this.Index] * 4];
			}
		}
		else
		{
			base.GetComponent<SpriteRenderer>().sprite = FileManager.CharSprite[6 + this.Index * 8];
			this.Weapon.GetComponent<SpriteRenderer>().sprite = FileManager.WeaponSprite[1 + FileManager.HeroWeapon[this.Index] * 4];
		}
	}

	public GameObject Target;

	public int Index;

	private GameObject F1up;

	private GameObject F2up;

	private GameObject F2down;

	private GameObject F3down;

	private GameObject Weapon;

	private Player.eSTATE STATE;

	private Player.eSTATE NowSTATE;

	private float MoveSpeed = 10f;

	private float Deltatime;

	private float Delay;

	private int AniFrame;

	private int Flour = 1;

	private bool Pick;

	private float REN = 1f;

	private float DEX = 0.3f;

	public enum eSTATE
	{
		idle,
		move,
		attack,
		action
	}
}

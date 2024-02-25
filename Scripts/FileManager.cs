using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class FileManager : MonoBehaviour
{
	private void Awake()
	{
		// Social.Active = new GPGSocial(); // Placeholder removed
		FileManager.file_Data = "Data.txt";
		FileManager.file_Collection = "Collection.txt";
		FileManager.file_path = Application.persistentDataPath;
	}

	private void Start()
	{
		for (int i = 0; i < 10; i++)
		{
			FileManager.HeroWeapon[i] = 0;
		}
		for (int j = 0; j < 16; j++)
		{
			FileManager.PlayerGold[j] = 0;
		}
		FileManager.CharSprite = Resources.LoadAll<Sprite>("Char/Char");
		FileManager.WeaponSprite = Resources.LoadAll<Sprite>("Char/Weapon");
		FileManager.GoldImage = Resources.LoadAll<Sprite>("Gold/Gold");
		FileManager.PotImage = Resources.LoadAll<Sprite>("Pot/Pot");
		FileManager.NumberImage = Resources.LoadAll<Sprite>("Number/Number");
		FileManager.Collec = Resources.LoadAll<Sprite>("Collection");
		FileManager.Buy = (Resources.Load("Sound/Buy") as AudioClip);
		FileManager.Sword = (Resources.Load("Sound/Sword") as AudioClip);
		FileManager.Destroy = (Resources.Load("Sound/Destroy") as AudioClip);
		FileManager.Get = (Resources.Load("Sound/Get") as AudioClip);
		FileManager.Tension = (Resources.Load("Sound/Tension") as AudioClip);
		FileManager.Collect = (Resources.Load("Sound/Collect") as AudioClip);
		FileManager.Click = (Resources.Load("Sound/Click") as AudioClip);
		FileManager.Wind = (Resources.Load("Sound/Wind") as AudioClip);
		FileManager.Shop = (Resources.Load("Sound/Shop") as AudioClip);
		FileManager.Cm = (Resources.Load("Sound/Cm") as AudioClip);
		FileManager.CollectionOpen();
	}

	public static void MainII()
	{
		if (File.Exists(FileManager.file_path + "/" + FileManager.file_Data))
		{
			GameObject.Find("Title").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Main1");
		}
		else
		{
			GameObject.Find("Title").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Main0");
		}
	}

	public static void DataSave()
	{
		stream_write = new StreamWriter(file_path + "/" + file_Data);
		stream_write.WriteLine(DesEncode(password + "L", PotLevel.ToString()));
		stream_write.WriteLine(DesEncode(password + "R", PotRegen.ToString()));
		stream_write.WriteLine(DesEncode(password + "M", PotLimit.ToString()));
		stream_write.WriteLine(DesEncode(password + "H", HeroLimit.ToString()));
		for (int i = 0; i < 10; i++)
		{
			stream_write.WriteLine(DesEncode(password + Convert.ToChar(65 + i), HeroWeapon[i].ToString()));
		}
		for (int j = 0; j < 16; j++)
		{
			stream_write.WriteLine(DesEncode(password + Convert.ToChar(65 + j), PlayerGold[j].ToString()));
		}
		stream_write.Close();
	}

	public static void DataOpen()
	{
		if (File.Exists(file_path + "/" + file_Data))
		{
			try
			{
				stream_read = new StreamReader(file_path + "/" + file_Data);
				PotLevel = int.Parse(DesDecode(password + "L", stream_read.ReadLine()));
				PotRegen = int.Parse(DesDecode(password + "R", stream_read.ReadLine()));
				PotLimit = int.Parse(DesDecode(password + "M", stream_read.ReadLine()));
				HeroLimit = int.Parse(DesDecode(password + "H", stream_read.ReadLine()));
				for (int i = 0; i < 10; i++)
				{
					HeroWeapon[i] = int.Parse(DesDecode(password + Convert.ToChar(65 + i), stream_read.ReadLine()));
				}
				for (int j = 0; j < 16; j++)
				{
					PlayerGold[j] = int.Parse(DesDecode(password + Convert.ToChar(65 + j), stream_read.ReadLine()));
				}
				stream_read.Close();
			}
			catch
			{
				stream_read.Close();
				DataZero();
				DataSave();
				GameObject gameObject = UnityEngine.Object.Instantiate(ObjectManager.Tutorial) as GameObject;
			}
		}
		else
		{
			DataSave();
			GameObject gameObject2 = UnityEngine.Object.Instantiate(ObjectManager.Tutorial) as GameObject;
		}
	}

	public static void DataZero()
	{
		File.Delete(file_path + "/" + file_Data);
		PotLevel = 1;
		PotRegen = 1;
		PotLimit = 1;
		HeroLimit = 1;
		for (int i = 0; i < 10; i++)
		{
			HeroWeapon[i] = 0;
		}
		for (int j = 0; j < 16; j++)
		{
			PlayerGold[j] = 0;
		}
	}

	public static void CollectionZero()
	{
		File.Delete(file_path + "/" + file_Collection);
		for (int i = 0; i < 10; i++)
		{
			Collection[i] = 0;
		}
	}

	public static void CollectionSave()
	{
		stream_write = new StreamWriter(file_path + "/" + file_Collection);
		for (int i = 0; i < 10; i++)
		{
			stream_write.WriteLine(DesEncode(password + Convert.ToChar(65 + i), Collection[i].ToString()));
		}
		stream_write.Close();
	}

	public static void CollectionOpen()
	{
		if (File.Exists(file_path + "/" + file_Collection))
		{
			try
			{
				stream_read = new StreamReader(file_path + "/" + file_Collection);
				for (int i = 0; i < 10; i++)
				{
					Collection[i] = int.Parse(DesDecode(password + Convert.ToChar(65 + i), stream_read.ReadLine()));
				}
				stream_read.Close();
			}
			catch
			{
				stream_read.Close();
				CollectionZero();
				CollectionSave();
			}
		}
		else
		{
			CollectionSave();
		}
	}

	private static string DesEncode(string password, string text)
	{
		try
		{
			DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
			DES des = descryptoServiceProvider;
			byte[] bytes = Encoding.ASCII.GetBytes(password);
			descryptoServiceProvider.IV = bytes;
			des.Key = bytes;
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
			byte[] bytes2 = Encoding.UTF8.GetBytes(text);
			cryptoStream.Write(bytes2, 0, bytes2.Length);
			cryptoStream.FlushFinalBlock();
			return Convert.ToBase64String(memoryStream.ToArray());
		}
		catch
		{
		}
		return string.Empty;
	}

	private static string DesDecode(string password, string text)
	{
		try
		{
			DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
			DES des = descryptoServiceProvider;
			byte[] bytes = Encoding.ASCII.GetBytes(password);
			descryptoServiceProvider.IV = bytes;
			des.Key = bytes;
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
			byte[] array = Convert.FromBase64String(text);
			cryptoStream.Write(array, 0, array.Length);
			cryptoStream.FlushFinalBlock();
			return Encoding.UTF8.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
		}
		catch
		{
		}
		return string.Empty;
	}

	public static void SendRank()
	{
		// Your SendRank implementation here
	}

	public static void OnSubmitScore(bool result)
	{
		// Your OnSubmitScore implementation here
	}

	public static int PotLevel = 1;
	public static int PotRegen = 1;
	public static int PotLimit = 1;
	public static int HeroLimit = 1;
	public static int[] HeroWeapon = new int[10];
	public static int[] PlayerGold = new int[16];
	public static Sprite[] CharSprite;
	public static Sprite[] WeaponSprite;
	public static Sprite[] GoldImage;
	public static Sprite[] PotImage;
	public static Sprite[] NumberImage;
	public static Sprite[] Collec;
	public static string file_Data;
	public static string file_Collection;
	public static string file_path;
	public static StreamWriter stream_write;
	public static StreamReader stream_read;
	public static AudioClip Buy;
	public static AudioClip Sword;
	public new static AudioClip Destroy;
	public static AudioClip Get;
	public static AudioClip Tension;
	public static AudioClip Click;
	public static AudioClip Collect;
	public static AudioClip Wind;
	public static AudioClip Shop;
	public static AudioClip Cm;
	public static int[] Collection = new int[10];
	public static string password = "Texture";
}

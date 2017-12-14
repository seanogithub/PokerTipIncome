using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text;

public class UserBlobManager : MonoBehaviour {

	public List<TipData> DailyTipData = new List<TipData>();
	public List<ExpenseData> MonthlyExpenseData = new List<ExpenseData>();
	public AppData UserAppData = new AppData();	
	public UserData UserAccountData = new UserData();
	public GameObject UIManager;
	public GameObject UICalendar;
	public GameObject UIStatistics;
	
	public GUISkin CurrentSkin;
	public string CurrentSkinName = "iOS7";

	public Color FontColor = new Color( 0.0f, 0.0f, 0.0f, 1.0f);  // Black
	public Color ButtonColor = new Color( 0.37f, 0.63f, 1.0f, 1.0f);  // Light Blue
	public Color BackGroundColor = new Color( 0.50f, 0.50f, 0.50f, 1.0f);  // Light Gray
	
	public Texture2D CalendarBackground;
	public Texture2D EnterAlphaNumericValueBackground;
	public Texture2D EnterDataBackground;
	public Texture2D CalendarRightArrowButton;
	public Texture2D CalendarLeftArrowButton;
	
	public Font myFont;
	public int myFontSize = 30;
	
	SaveData myData = new SaveData();
	
	Rect _Save, _Load, _SaveMSG, _LoadMSG; 
	bool _ShouldSave, _ShouldLoad,_SwitchSave,_SwitchLoad; 
	string _FileLocation,_FileName; 	 	
	string _data; 	
	
	
	// Use this for initialization
	void Awake () 
	{
		UIManager = GameObject.Find("UI_Manager_Prefab");
		UICalendar = GameObject.Find("UI_Calendar_Prefab");
		UIStatistics = GameObject.Find("UI_Statistics_Prefab");
		
		UserAccountData = new UserData();
		UserAccountData.UserDeviceID = SystemInfo.deviceUniqueIdentifier.ToString();
		DailyTipData.Clear();
		MonthlyExpenseData.Clear();
		
		_FileLocation=Application.persistentDataPath; 
		_FileName="SaveData.xml";  
		
		// we need soemthing to store the information into 
		myData=new SaveData(); 		
		
		if(File.Exists(_FileLocation+"/"+ _FileName))
		{
			LoadPlayerData();
//			print (UserAccountData.UserAccountLogIn.ToString());
		}		
	}
	
	void Start ()
	{
		if(!File.Exists(_FileLocation+"/"+ _FileName))
		{
			CreateSaveDataFile();
		}
		ChangeCurrentSkin();
	}
	
	public void ChangeCurrentSkin()
	{
		switch(CurrentSkinName)
		{
		case "iOS7":
			CurrentSkin = (GUISkin)(Resources.Load("UI/Skins/iOS7_Skin"));
			CalendarBackground = (Texture2D)(Resources.Load("UI/Background/Calendar_Background"));
			EnterAlphaNumericValueBackground = (Texture2D)(Resources.Load("UI/Background/EnterAlphaNumericValue_Background"));
			EnterDataBackground = (Texture2D) (Resources.Load("UI/Background/EnterData_Background"));
			CalendarRightArrowButton  = (Texture2D) (Resources.Load("UI/Styles/StyleCalendarRightArrowButton"));
			CalendarLeftArrowButton  = (Texture2D) (Resources.Load("UI/Styles/StyleCalendarLeftArrowButton"));
//			FontColor = new Color( 0.0f, 0.0f, 0.0f, 1.0f);  // Black
//			ButtonColor = new Color( 0.37f, 0.63f, 1.0f, 1.0f);  // Light Blue
//			BackGroundColor = new Color( 0.50f, 0.50f, 0.50f, 1.0f);  // Light Gray		
			CurrentSkin.customStyles[0].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.25f);
			CurrentSkin.customStyles[1].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.25f);
			CurrentSkin.customStyles[2].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.25f);
			CurrentSkin.customStyles[3].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.25f);
			CurrentSkin.customStyles[4].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 2.0f);
			CurrentSkin.customStyles[5].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 2.0f);
			CurrentSkin.customStyles[6].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 2.0f);
			CurrentSkin.customStyles[7].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.5f);
			CurrentSkin.customStyles[8].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.5f);
			CurrentSkin.customStyles[9].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.0f);
			CurrentSkin.customStyles[10].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 2.0f);
			CurrentSkin.customStyles[11].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.5f);
			CurrentSkin.customStyles[5].normal.textColor = ButtonColor;
			CurrentSkin.customStyles[6].normal.textColor = ButtonColor;
			CurrentSkin.customStyles[7].normal.textColor = FontColor;
			CurrentSkin.customStyles[8].normal.textColor = FontColor;
			CurrentSkin.customStyles[9].normal.textColor = ButtonColor;
			CurrentSkin.customStyles[10].normal.textColor = ButtonColor;
			CurrentSkin.customStyles[11].normal.textColor = FontColor;
			CurrentSkin.horizontalSlider.overflow.top = (int)((float)Screen.width * 0.02f  + (float)Screen.width * 0.02f) * -1;			
			CurrentSkin.horizontalSlider.overflow.bottom = (int)((float)Screen.width * 0.02f + (float)Screen.width * 0.02f) * -1;			
			CurrentSkin.horizontalSlider.fixedHeight = 0;			
			CurrentSkin.horizontalSliderThumb.fixedWidth = (int)((float)Screen.width * 0.04f);			
			CurrentSkin.horizontalSliderThumb.fixedHeight = (int)((float)Screen.width * 0.06f);			
			CurrentSkin.horizontalSliderThumb.overflow.bottom = (int)((float)Screen.width * 0.06f);			
			break;
		case "Vintage":
			CurrentSkin = (GUISkin)(Resources.Load("UI/Skins/Vintage_Skin"));
			CalendarBackground = (Texture2D)(Resources.Load("UI/Background/Calendar_Vintage_Background"));
			EnterAlphaNumericValueBackground = (Texture2D)(Resources.Load("UI/Background/EnterAlphaNumericValue_Vintage_Background"));
			EnterDataBackground = (Texture2D) (Resources.Load("UI/Background/EnterData_Vintage_Background"));
			CalendarRightArrowButton  = (Texture2D) (Resources.Load("UI/Styles/StyleCalendarRightArrowButton_Vintage"));
			CalendarLeftArrowButton  = (Texture2D) (Resources.Load("UI/Styles/StyleCalendarLeftArrowButton_Vintage"));			
//			FontColor = new Color( 0.0f, 0.0f, 0.0f, 1.0f);  // Black
//			ButtonColor = new Color( 0.37f, 0.63f, 1.0f, 1.0f);  // Light Blue
//			BackGroundColor = new Color( 0.50f, 0.50f, 0.50f, 1.0f);  // Light Gray		
			CurrentSkin.customStyles[0].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.25f);
			CurrentSkin.customStyles[1].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.25f);
			CurrentSkin.customStyles[2].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.25f);
			CurrentSkin.customStyles[3].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.25f);
			CurrentSkin.customStyles[4].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 2.0f);
			CurrentSkin.customStyles[5].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 2.0f);
			CurrentSkin.customStyles[6].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 2.0f);
			CurrentSkin.customStyles[7].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.5f);
			CurrentSkin.customStyles[8].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.75f);// 1.5f);
			CurrentSkin.customStyles[9].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.0f);
			CurrentSkin.customStyles[10].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 2.0f);
			CurrentSkin.customStyles[11].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.5f);
			CurrentSkin.customStyles[5].normal.textColor = ButtonColor;
			CurrentSkin.customStyles[6].normal.textColor = ButtonColor;
			CurrentSkin.customStyles[7].normal.textColor = FontColor;
			CurrentSkin.customStyles[8].normal.textColor = FontColor;
			CurrentSkin.customStyles[9].normal.textColor = ButtonColor;
			CurrentSkin.customStyles[10].normal.textColor = ButtonColor;
			CurrentSkin.customStyles[11].normal.textColor = FontColor;
			CurrentSkin.horizontalSlider.overflow.top = (int)((float)Screen.width * 0.02f  + (float)Screen.width * 0.02f) * -1;			
			CurrentSkin.horizontalSlider.overflow.bottom = (int)((float)Screen.width * 0.02f + (float)Screen.width * 0.02f) * -1;			
			CurrentSkin.horizontalSlider.fixedHeight = 0;			
			CurrentSkin.horizontalSliderThumb.fixedWidth = (int)((float)Screen.width * 0.04f);			
			CurrentSkin.horizontalSliderThumb.fixedHeight = (int)((float)Screen.width * 0.06f);			
			CurrentSkin.horizontalSliderThumb.overflow.bottom = (int)((float)Screen.width * 0.06f);				
			break;		
		case "Money":
			CurrentSkin = (GUISkin)(Resources.Load("UI/Skins/Money_Skin"));
			CalendarBackground = (Texture2D)(Resources.Load("UI/Background/Calendar_Money_Background"));
			EnterAlphaNumericValueBackground = (Texture2D)(Resources.Load("UI/Background/EnterAlphaNumericValue_Money_Background"));
			EnterDataBackground = (Texture2D) (Resources.Load("UI/Background/EnterData_Money_Background"));
			CalendarRightArrowButton  = (Texture2D) (Resources.Load("UI/Styles/StyleCalendarRightArrowButton_Money"));
			CalendarLeftArrowButton  = (Texture2D) (Resources.Load("UI/Styles/StyleCalendarLeftArrowButton_Money"));			
//			FontColor = new Color( 0.0f, 0.0f, 0.0f, 1.0f);  // Black
//			ButtonColor = new Color( 0.37f, 0.63f, 1.0f, 1.0f);  // Light Blue
//			BackGroundColor = new Color( 0.50f, 0.50f, 0.50f, 1.0f);  // Light Gray		
			CurrentSkin.customStyles[0].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.0f); //
			CurrentSkin.customStyles[1].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.0f); //
			CurrentSkin.customStyles[2].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.25f);
			CurrentSkin.customStyles[3].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.25f);
			CurrentSkin.customStyles[4].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 2.0f);
			CurrentSkin.customStyles[5].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 2.0f);
			CurrentSkin.customStyles[6].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.35f); // 
			CurrentSkin.customStyles[7].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.5f); 
			CurrentSkin.customStyles[8].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.5f);
			CurrentSkin.customStyles[9].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.0f);
			CurrentSkin.customStyles[10].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.5f);
			CurrentSkin.customStyles[11].fontSize = Mathf.RoundToInt((float)myFontSize * (float)Screen.width / 1200 * 1.5f);
			CurrentSkin.customStyles[5].normal.textColor = ButtonColor;
			CurrentSkin.customStyles[6].normal.textColor = ButtonColor;
			CurrentSkin.customStyles[7].normal.textColor = FontColor;
			CurrentSkin.customStyles[8].normal.textColor = FontColor;
			CurrentSkin.customStyles[9].normal.textColor = ButtonColor;
			CurrentSkin.customStyles[10].normal.textColor = ButtonColor;
			CurrentSkin.customStyles[11].normal.textColor = FontColor;
			CurrentSkin.horizontalSlider.overflow.top = (int)((float)Screen.width * 0.02f  + (float)Screen.width * 0.02f) * -1;			
			CurrentSkin.horizontalSlider.overflow.bottom = (int)((float)Screen.width * 0.02f + (float)Screen.width * 0.02f) * -1;			
			CurrentSkin.horizontalSlider.fixedHeight = 0;			
			CurrentSkin.horizontalSliderThumb.fixedWidth = (int)((float)Screen.width * 0.04f);			
			CurrentSkin.horizontalSliderThumb.fixedHeight = (int)((float)Screen.width * 0.06f);			
			CurrentSkin.horizontalSliderThumb.overflow.bottom = (int)((float)Screen.width * 0.06f);				
			break;				
		}
	}
	
	void CreateSaveDataFile()
	{
		UserAppData = new AppData();
		UserAppData.JobNameList = new List<string> {"Golden Nugget", "Aria", "Bellagio", "MGM", "Venetian", "Mandalay Bay", "Luxor", "Excalibur", "Binions", "Red Rock", "GVR"};
		UserAppData.TournamentNameList = new List<string> {"Daily", "Deep Stack", "WSOP", "WPT"};
		UserAppData.ExpenseNameList = new List<string> {"Mortgage", "Rent", "Car", "Car Insurance", "Gas", "Food", "Phone", "Cable", "Water", "Power", "Utilites", "Credit Card 1", "Credit Card 2", "Credit Card 3", "Other", "Misc"};
		SaveAccountData();
	}	
	
	public void SaveAccountData()
	{
		UserAppData.FontColor = FontColor;
		UserAppData.ButtonColor = ButtonColor;
		UserAppData.BackGroundColor = BackGroundColor;
		
		myData.DailyTipData = DailyTipData;
		myData.UserAppData = UserAppData;
		myData.UserAccountData = UserAccountData;
		myData.MonthlyExpenseData = MonthlyExpenseData;
		
		SavePlayerData();
	}	

	public void SavePlayerData()
	{
		// Time to creat our XML! 
		_data = SerializeObject(myData); 
		
		// This is the final resulting XML from the serialization process 
		CreateXML(); 
	}

	public void LoadPlayerData()
	{
		LoadXML(); 
		if(_data.ToString() != "") 
		{ 
			myData = (SaveData)DeserializeObject(_data); 
/*
			// set Inventory
			Inventory.InventoryList.Clear();
			for(var i = 0; i < myData.Inventory.InventoryList.Count; i++)
			{
				Inventory.InventoryList.Add(myData.Inventory.InventoryList[i]);
			}
*/
			// set DailyTipData
			DailyTipData.Clear();
			for(var i = 0; i < myData.DailyTipData.Count; i++)
			{
				DailyTipData.Add(myData.DailyTipData[i]);
			}
			
			MonthlyExpenseData.Clear();
			for(var i = 0; i < myData.MonthlyExpenseData.Count; i++)
			{
				MonthlyExpenseData.Add(myData.MonthlyExpenseData[i]);
			}
		
			// set Appdata
			UserAppData = myData.UserAppData;
			
			FontColor = UserAppData.FontColor;
			ButtonColor = UserAppData.ButtonColor;
			BackGroundColor = UserAppData.BackGroundColor;
			CurrentSkinName = UserAppData.SkinName;
			
			// set Account Data
			UserAccountData = myData.UserAccountData;
		}		
	}	
	
	public void DeletePlayerData()
	{
		if(File.Exists(_FileLocation+"/"+ _FileName))
		{
			print (_FileLocation+"/"+ _FileName);
			File.Delete(_FileLocation+"/"+ _FileName);	
			DailyTipData.Clear();
			MonthlyExpenseData.Clear();
			SaveAccountData();
			UIManager.GetComponent<UI_Manager>().ResetManagerData();
			UICalendar.GetComponent<UI_Calendar>().ResetCalendarData();
			UICalendar.GetComponent<UI_Calendar>().UpdateCalendar();
			UIStatistics.GetComponent<UI_Statistics>().ResetStatisticsData();
			UIStatistics.GetComponent<UI_Statistics>().UpdateStatistics();

			var temp = Resources.Load("UI/UI_PopUpDialogBox_Prefab") as GameObject;
			var myPopUp = GameObject.Instantiate(temp, (new Vector3(0.0f, 0.0f , 1.0f)), Quaternion.identity ) as GameObject;
			myPopUp.GetComponent<UI_PopUpDialogBox>().Message = "All data has been deleted!"; 
			
		}		
	}

	/* The following metods came from the referenced URL */ 
	string UTF8ByteArrayToString(byte[] characters) 
	{      
	  UTF8Encoding encoding = new UTF8Encoding(); 
	  string constructedString = encoding.GetString(characters); 
	  return (constructedString); 
	} 
	
	byte[] StringToUTF8ByteArray(string pXmlString) 
	{ 
	  UTF8Encoding encoding = new UTF8Encoding(); 
	  byte[] byteArray = encoding.GetBytes(pXmlString); 
	  return byteArray; 
	} 

	// Here we deserialize it back into its original form 
	object DeserializeObject(string pXmlizedString) 
	{ 
		// type of need to be the type of data we are saving
//		XmlSerializer xs = new XmlSerializer(typeof(MapBuildable[])); 
		XmlSerializer xs = new XmlSerializer(typeof(SaveData)); 
		MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		return xs.Deserialize(memoryStream); 
	} 
	
	string SerializeObject(object pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		// type of need to be the type of data we are saving
//		XmlSerializer xs = new XmlSerializer(typeof(MapBuildable[])); 
		XmlSerializer xs = new XmlSerializer(typeof(SaveData)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xmlTextWriter.Settings.Indent = true;
		xmlTextWriter.Formatting = Formatting.Indented ;
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString; 
	} 
	
	void CreateXML() 
	{ 
		StreamWriter writer; 
		FileInfo t = new FileInfo(_FileLocation+"/"+ _FileName); 
		if(!t.Exists) 
		{ 
			writer = t.CreateText(); 
		} 
		else 
		{ 
			t.Delete(); 
			writer = t.CreateText(); 
		} 
		writer.Write(_data); 
		writer.Close(); 
		Debug.Log("File written."); 
	} 

	void LoadXML() 
	{ 
		StreamReader r = File.OpenText(_FileLocation+"/"+ _FileName); 
		string _info = r.ReadToEnd(); 
		r.Close(); 
		_data=_info; 
		Debug.Log("File Read"); 
	} 	
	
}

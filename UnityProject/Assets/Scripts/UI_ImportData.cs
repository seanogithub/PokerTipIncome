using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;

[RequireComponent(typeof(GUITexture))]
public class UI_ImportData : MonoBehaviour 
{

	public bool Enabled = true;
	
	public GameObject UIManager;
	public GameObject UIEnterData;
	public GameObject UserBlobManager;
	public GameObject UICalendar; 
	
	public List<TipData> myDailyTipData = new List<TipData>(); 
		
	public Texture2D BackgroundBitmap;	
	public Texture2D styleCalendarButtonsBitmap;	
	public Texture2D styleCalendarButtonsActiveBitmap;	
	public Texture2D style2XButtonBlueBitmap;

	private GUIStyle style;
	private GUIStyle styleWhite;
	private GUIStyle styleRed;
	private GUIStyle styleGreen;
	private GUIStyle style2X;
	private GUIStyle style2XButton;
	private GUIStyle style2XButtonBlue;
	private GUIStyle styleBigger;
	private GUIStyle styleDailyTotals;
	private GUIStyle styleCalendarButtons;
	private GUIStyle styleEnterDataButtons;
	private GUIStyle style2XBlack;

	public Color FontColor;
	public Color ButtonColor;
	public Color BackgroundColor;
	
	private float AspectRatioMultiplier = 0.57f;	
	
	public TouchScreenKeyboard DefaultKeyBoard;
	
//	public string myAlphaNumericValue;
	public string myAlphaNumericValueString;
	private string myAlphaNumericValueLabel;
	
	private float CheckNumberValue;
	private string PreviousmyAlphaNumericValueString;

	public int myTipDataIndex = 0;
	
	public string DataOutState = "";
	
	void Awake()
	{
		GetComponent<GUITexture>().pixelInset = new Rect(Screen.width/2, Screen.height/2, 0, 0);		
	}
	
	// Use this for initialization
	void Start () 
	{
		UIManager = GameObject.Find("UI_Manager_Prefab");
		UIEnterData = GameObject.Find("UI_EnterData_Prefab");
		UserBlobManager = GameObject.Find("UserBlobManager_Prefab");
		UICalendar = GameObject.Find("UI_Calendar_Prefab");
		
//		DefaultKeyBoard = new TouchScreenKeyboard.Open(myAlphaNumericValueString, TouchScreenKeyboardType.NumberPad);
		
		AspectRatioMultiplier = ((float)Screen.width/1200 );
	}

	public void GUIEnabled(bool myValue)
	{
		Enabled = myValue;
		this.GetComponent<GUITexture>().enabled = myValue;
	}
	
	void OnGUI()
	{
		if (Enabled == true)
		{
			FontColor = UserBlobManager.GetComponent<UserBlobManager>().FontColor;
			ButtonColor = UserBlobManager.GetComponent<UserBlobManager>().ButtonColor;
			BackgroundColor =  UserBlobManager.GetComponent<UserBlobManager>().BackGroundColor;
			
			style = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[0];
			styleWhite = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[1];
			styleRed = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[2];
			styleGreen = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[3];
			style2X = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[4];
			style2XButton = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[5];
			style2XButtonBlue = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[6];
			styleBigger = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[7];
			styleDailyTotals = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[8];
			styleCalendarButtons = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[9];
			styleEnterDataButtons = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[10];
			style2XBlack = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin.customStyles[11];
			
			AspectRatioMultiplier = ((float)Screen.width/1200 );

			// background image
			GetComponent<GUITexture>().color = BackgroundColor;
			GetComponent<GUITexture>().texture = UserBlobManager.GetComponent<UserBlobManager>().EnterAlphaNumericValueBackground;
			
			// time and month and year		
			GUI.Label(new Rect((Screen.width * 0.5f), (Screen.height* 0.005f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (System.DateTime.Now.ToShortTimeString()), styleBigger);
			
			GUI.Label(new Rect((Screen.width * 0.5f), (Screen.height * 0.10f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), myAlphaNumericValueLabel, styleBigger);

#if UNITY_ANDROID || UNITY_IPHONE	
			if(GUI.Button (new Rect((Screen.width * 0.10f), (Screen.height * 0.17f), (Screen.width * 0.80f), Screen.height * 0.15f), myAlphaNumericValueString, styleEnterDataButtons))
			{
				if (TouchScreenKeyboard.visible == false )
				{
					DefaultKeyBoard = null;
					DefaultKeyBoard = new TouchScreenKeyboard(myAlphaNumericValueString, TouchScreenKeyboardType.Default, true, false, false, false, "0");
				}			
			}
			myAlphaNumericValueString = DefaultKeyBoard.text;
//			myAlphaNumericValueString = Regex.Replace(myAlphaNumericValueString, @"[^0-9.]", "");
/*			
			if (myAlphaNumericValueString != "")
			{
				CheckNumberValue = 0.0f;
				if(float.TryParse(myAlphaNumericValueString, System.Globalization.NumberStyles.Currency ,new CultureInfo("en-US"), out CheckNumberValue))
				{
					myAlphaNumericValueString = CheckNumberValue.ToString();
					PreviousmyAlphaNumericValueString = myAlphaNumericValueString;
				}
				else
				{
					myAlphaNumericValueString = PreviousmyAlphaNumericValueString;
					DefaultKeyBoard.text = PreviousmyAlphaNumericValueString;
				}
				
			}
			PreviousmyAlphaNumericValueString = myAlphaNumericValueString;			
*/
			
#elif UNITY_EDITOR || UNITY_STANDALONE
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.17f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), myAlphaNumericValueLabel, styleBigger);
			myAlphaNumericValueString = DefaultKeyBoard.text;

//			myAlphaNumericValueString = GUI.TextField (new Rect((Screen.width * 0.4f), (Screen.height * 0.17f), (Screen.width * 0.5f), (Screen.height * 0.05f)), myAlphaNumericValueString, 10, style2XField);
//			myAlphaNumericValueString = Regex.Replace(myAlphaNumericValueString, @"[^0-9.]", "");
/*			
			if (myAlphaNumericValueString != "")
			{
				CheckNumberValue = 0.0f;
				if(float.TryParse(myAlphaNumericValueString, System.Globalization.NumberStyles.Currency ,new CultureInfo("en-US"), out CheckNumberValue))
				{
					myAlphaNumericValueString = CheckNumberValue.ToString();
					PreviousmyAlphaNumericValueString = myAlphaNumericValueString;
				}
				else
				{
					myAlphaNumericValueString = PreviousmyAlphaNumericValueString;
//					DefaultKeyBoard.text = PreviousmyAlphaNumericValueString;
				}
			}			
			PreviousmyAlphaNumericValueString = myAlphaNumericValueString;
*/			
#endif
	
			GUI.SetNextControlName ("OKButton");
			if (GUI.Button( new Rect((Screen.width * 0.2f), (Screen.height * 0.35f), (Screen.width * 0.2f), (Screen.height * 0.1f)) ,"OK", style2XButtonBlue))
			{
				ParseDataString();
				PassDataOut();
			}
			GUI.SetNextControlName ("CancelButton");
			if (GUI.Button( new Rect((Screen.width * 0.6f), (Screen.height * 0.35f), (Screen.width * 0.2f), (Screen.height * 0.1f)) ,"Cancel", style2XButtonBlue))
			{
				DontPassDataOut();
			}
		}
	}
	
	
	void Update()
	{
		if(Enabled == true)
		{
			if (DefaultKeyBoard == null )
			{
				DefaultKeyBoard = new TouchScreenKeyboard(myAlphaNumericValueString, TouchScreenKeyboardType.Default, true, false, false, false, myAlphaNumericValueString );
			}
		}
	}
	
	void ParseDataString()
	{
		var Char = new char[] {',', '\n'};
//		var shit = "GVR,8/1/2013 6:47:26 AM,100,10,10,False,Deep Stack,0,0,0,,True,1/1/2013 2:00:00 PM,0\nGolden Nugget,8/2/2013 6:48:24 AM,0,0,5,False,Daily,0,10,0,,True,1/1/2013 1:00:00 PM,0";
//		var temp = shit.Split(Char);
		var temp = myAlphaNumericValueString.Split(Char);
		
		myDailyTipData = new List<TipData>(); 
		
		for(int i = 0; i < temp.Length; i+=14)
		{
			print (temp[i+9]);
			
			var tempTipData = new TipData();
			tempTipData.JobName = temp[i]; //"Golden Nugget";
			tempTipData.Date =  System.Convert.ToDateTime(temp[i+1]); //SearchDate;
			tempTipData.TipAmount =  (float)System.Convert.ToDouble(temp[i+2]); //0.0f;
			tempTipData.NumberOfDowns = (int)System.Convert.ToDouble(temp[i+3]); // 0;
			tempTipData.HoursWorked = (float)System.Convert.ToDouble(temp[i+4]); //0.0f;
			tempTipData.TournamentDowns = System.Convert.ToBoolean(temp[i+5]); //false;
			tempTipData.TournamentName = temp[i+6]; // "";
			tempTipData.NumberOfTournamentDowns = (int)System.Convert.ToDouble(temp[i+7]); // 0;
			tempTipData.TipOut = (float)System.Convert.ToDouble(temp[i+9]); //0.0f; 
			tempTipData.Notes = temp[i+10]; // "";	
			tempTipData.WorkDay = System.Convert.ToBoolean(temp[i+11]); // false;
			tempTipData.WorkDayStartTime =  System.Convert.ToDateTime(temp[i+12]); //new System.DateTime(SearchDate.Year,SearchDate.Month,SearchDate.Day,0,0,0);
			tempTipData.PayCheckAmount = (float)System.Convert.ToDouble(temp[i+13]); //0.0f;
			
			myDailyTipData.Add(tempTipData);
			
		}
		
		UserBlobManager.GetComponent<UserBlobManager>().DailyTipData.Clear();
		UIManager.GetComponent<UI_Manager>().myDailyTipData.Clear();
		UICalendar.GetComponent<UI_Calendar>().DailyTipData.Clear();
		
		UserBlobManager.GetComponent<UserBlobManager>().DailyTipData = myDailyTipData;
		UIManager.GetComponent<UI_Manager>().myDailyTipData = myDailyTipData;
		UICalendar.GetComponent<UI_Calendar>().DailyTipData = myDailyTipData;
		
		UICalendar.GetComponent<UI_Calendar>().UpdateCalendar();
	}
	
	public void PassDataIn( string DataName )
	{
//		myTipDataIndex = UIEnterData.GetComponent<UI_EnterData>().myTipDataIndex;
		myTipDataIndex = 0;
		
		switch(DataName)
		{
		case "ImportData":
//			myAlphaNumericValueString = UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].JobName;
			myAlphaNumericValueLabel	 = 	"Paste data from email into here";
			DataOutState = DataName;
			break;				
		}
	}
	
	public void PassDataOut()
	{
		switch(DataOutState)
		{
		case "ImportData":
//			UserBlobManager.GetComponent<UserBlobManager>().UserAppData.JobNameList.Add(myAlphaNumericValueString);
//			UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].JobName = myAlphaNumericValueString;
			DefaultKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("SettingsState");			
			break;			
		}
	}
	
	public void DontPassDataOut()
	{
		switch(DataOutState)
		{
		case "ImportData":
			DefaultKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("SettingsState");
			break;		
		}		
	}

}

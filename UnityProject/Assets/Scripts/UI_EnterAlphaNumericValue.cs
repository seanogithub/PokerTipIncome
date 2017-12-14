using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;

[RequireComponent(typeof(GUITexture))]
public class UI_EnterAlphaNumericValue : MonoBehaviour {

	public bool Enabled = true;
	
	public GameObject UIManager;
	public GameObject UIEnterData;
	public GameObject UserBlobManager;
	public GameObject UIMonthlyExpenses;
		
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
	private float TextButtonVerticalSize;
	
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
		UIMonthlyExpenses = GameObject.Find("UI_MonthlyExpenses_Prefab");
		
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
//			GUI.DrawTexture((new Rect(0,0,Screen.width,Screen.height)), BackgroundBitmap,ScaleMode.StretchToFill,true);
			
			// time and month and year		
			GUI.Label(new Rect((Screen.width * 0.5f), (Screen.height* 0.005f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (System.DateTime.Now.ToShortTimeString()), styleBigger);
			
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.17f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), myAlphaNumericValueLabel, styleBigger);

// need to fix the word wraping ???			
			if (DataOutState == "Notes")
			{
				TextButtonVerticalSize = (Screen.height * 0.15f);
			}
			else
			{
				TextButtonVerticalSize = (Screen.height * 0.05f);
			}
			
#if UNITY_ANDROID || UNITY_IPHONE	
			if(GUI.Button (new Rect((Screen.width * 0.4f), (Screen.height * 0.17f), (Screen.width * 0.5f), TextButtonVerticalSize), myAlphaNumericValueString, styleCalendarButtons))
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
	
	public void PassDataIn( string DataName )
	{
		myTipDataIndex = UIEnterData.GetComponent<UI_EnterData>().myTipDataIndex;
		
		switch(DataName)
		{
		case "JobName":
			myAlphaNumericValueString = UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].JobName;
			myAlphaNumericValueLabel	 = 	"Job Name";
			DataOutState = DataName;
			break;
		case "TournamentName":
			myAlphaNumericValueString = UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].TournamentName;
			myAlphaNumericValueLabel	 = 	"Tournament Name";
			DataOutState = DataName;
			break;		
		case "Notes":
			myAlphaNumericValueString = UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].Notes;
			myAlphaNumericValueLabel	 = 	"Notes";
			DataOutState = DataName;
			break;	
		case "ExpenseName":
			var myExpenseNameIndex = UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().myExpenseNameIndex;
			var CurrentMonthlyExpenseIndex = UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().CurrentMonthlyExpenseIndex;
			myAlphaNumericValueString = UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().MonthlyExpenseData[CurrentMonthlyExpenseIndex].ExpenseName[myExpenseNameIndex];
			myAlphaNumericValueLabel	 = 	"Expense Name";
			DataOutState = DataName;
			break;				
		}
	}
	
	public void PassDataOut()
	{
		switch(DataOutState)
		{
		case "JobName":
			UserBlobManager.GetComponent<UserBlobManager>().UserAppData.JobNameList.Add(myAlphaNumericValueString);
			UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].JobName = myAlphaNumericValueString;
			DefaultKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");			
			break;
		case "TournamentName":
			UserBlobManager.GetComponent<UserBlobManager>().UserAppData.TournamentNameList.Add(myAlphaNumericValueString);
			UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].TournamentName = myAlphaNumericValueString;
			DefaultKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");			
			break;
		case "Notes":
			UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].Notes = myAlphaNumericValueString;
			DefaultKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");			
			break;	
		case "ExpenseName":
			var myExpenseNameIndex = UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().myExpenseNameIndex;
			var CurrentMonthlyExpenseIndex = UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().CurrentMonthlyExpenseIndex;
			UserBlobManager.GetComponent<UserBlobManager>().UserAppData.ExpenseNameList.Add(myAlphaNumericValueString);
			UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().MonthlyExpenseData[CurrentMonthlyExpenseIndex].ExpenseName[myExpenseNameIndex] = myAlphaNumericValueString;
//			UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].TournamentName = myAlphaNumericValueString;
			DefaultKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("MonthlyExpensesState");
			break;			
		}
	}
	
	public void DontPassDataOut()
	{
		switch(DataOutState)
		{
		case "JobName":
			DefaultKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;
		case "TournamentName":
			DefaultKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;
		case "Notes":
			DefaultKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;	
		case "ExpenseName":
			DefaultKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("MonthlyExpensesState");
			break;			
		}		
	}
}

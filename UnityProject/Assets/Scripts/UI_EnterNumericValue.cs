using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;

[RequireComponent(typeof(GUITexture))]
public class UI_EnterNumericValue : MonoBehaviour {
	
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

	public TouchScreenKeyboard NumPadKeyBoard;
	
	public float myNumericValue;
	public string myNumericValueString;
	private string myNumericValueLabel;
	
	private float CheckNumberValue;
	private string PreviousmyNumericValueString;

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
		
//		NumPadKeyBoard = new TouchScreenKeyboard.Open(myNumericValueString, TouchScreenKeyboardType.NumbersAndPunctuation);

		AspectRatioMultiplier = ((float)Screen.width/1200 );

		
// need to pass this value in		
//		myNumericValue = 0.0f;
//		myNumericValueString = "";
//		myNumericValueLabel = "Tips $";
		
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
			
			GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.17f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), myNumericValueLabel, style2XBlack);
			
#if UNITY_ANDROID || UNITY_IPHONE	
			if(GUI.Button (new Rect((Screen.width * 0.5f), (Screen.height * 0.17f), (Screen.width * 0.25f), (Screen.height * 0.05f)), myNumericValueString, styleEnterDataButtons))
			{
				if (TouchScreenKeyboard.visible == false )
				{
					NumPadKeyBoard = null;
					NumPadKeyBoard = new TouchScreenKeyboard(myNumericValueString, TouchScreenKeyboardType.NumbersAndPunctuation, true, false, false, false, "0");
				}			
			}
			myNumericValueString = NumPadKeyBoard.text;
			myNumericValueString = Regex.Replace(myNumericValueString, @"[^0-9.]", "");
			
			if (myNumericValueString != "")
			{
				CheckNumberValue = 0.0f;
				if(float.TryParse(myNumericValueString, System.Globalization.NumberStyles.Currency ,new CultureInfo("en-US"), out CheckNumberValue))
				{
					myNumericValueString = CheckNumberValue.ToString();
					PreviousmyNumericValueString = myNumericValueString;
				}
				else
				{
					myNumericValueString = PreviousmyNumericValueString;
					NumPadKeyBoard.text = PreviousmyNumericValueString;
				}
				
			}
			PreviousmyNumericValueString = myNumericValueString;			
	
#elif UNITY_EDITOR || UNITY_STANDALONE
			myNumericValueString = GUI.TextField (new Rect((Screen.width * 0.4f), (Screen.height * 0.17f), (Screen.width * 0.5f), (Screen.height * 0.05f)), myNumericValueString, 10, styleEnterDataButtons);
			myNumericValueString = Regex.Replace(myNumericValueString, @"[^0-9.]", "");
			
			if (myNumericValueString != "")
			{
				CheckNumberValue = 0.0f;
				if(float.TryParse(myNumericValueString, System.Globalization.NumberStyles.Currency ,new CultureInfo("en-US"), out CheckNumberValue))
				{
					myNumericValueString = CheckNumberValue.ToString();
					PreviousmyNumericValueString = myNumericValueString;
				}
				else
				{
					myNumericValueString = PreviousmyNumericValueString;
//					NumPadKeyBoard.text = PreviousmyNumericValueString;
				}
			}			
			PreviousmyNumericValueString = myNumericValueString;
#endif
	
			// ok button
			GUI.SetNextControlName ("OKButton");
			if (GUI.Button( new Rect((Screen.width * 0.2f), (Screen.height * 0.35f), (Screen.width * 0.2f), (Screen.height * 0.1f)) ,"OK", style2XButtonBlue))
			{
				PassDataOut();
			}
			
			// cancel button
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
			if (NumPadKeyBoard == null )
			{
				NumPadKeyBoard = new TouchScreenKeyboard(myNumericValueString, TouchScreenKeyboardType.NumbersAndPunctuation, true, false, false, false, myNumericValueString );
			}
		}
	}
	
	public void PassDataIn( string DataName )
	{
		myTipDataIndex = UIEnterData.GetComponent<UI_EnterData>().myTipDataIndex;
		
		switch(DataName)
		{
		case "TipAmount":
			myNumericValue = UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].TipAmount;
			myNumericValueString = myNumericValue.ToString();
			myNumericValueLabel	 = 	"Tips $";
			DataOutState = DataName;
			break;
		case "NumberOfDowns":
			myNumericValue = UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].NumberOfDowns;
			myNumericValueString = myNumericValue.ToString();
			myNumericValueLabel	 = 	"Number of Downs";
			DataOutState = DataName;
			break;
		case "HoursWorked":
			myNumericValue = UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].HoursWorked;
			myNumericValueString = myNumericValue.ToString();
			myNumericValueLabel	 = 	"HoursWorked";
			DataOutState = DataName;
			break;
		case "TournamentDownAmount":
			myNumericValue = UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].TournamentDownAmount;
			myNumericValueString = myNumericValue.ToString();
			myNumericValueLabel	 = 	"Tourney $ Per Down";
			DataOutState = DataName;
			break;	
		case "NumberOfTournamentDowns":
			myNumericValue = UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].NumberOfTournamentDowns;
			myNumericValueString = myNumericValue.ToString();
			myNumericValueLabel	 = 	"Tournament Downs";
			DataOutState = DataName;
			break;
		case "TipOut":
			myNumericValue = UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].TipOut;
			myNumericValueString = myNumericValue.ToString();
			myNumericValueLabel	 = 	"Tip Out $";
			DataOutState = DataName;
			break;	
		case "ExpenseAmount":
			var myExpenseAmountIndex = UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().myExpenseAmountIndex;
			var CurrentMonthlyExpenseIndex = UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().CurrentMonthlyExpenseIndex;
			myNumericValue = UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().MonthlyExpenseData[CurrentMonthlyExpenseIndex].ExpenseAmount[myExpenseAmountIndex];
			myNumericValueString = myNumericValue.ToString();
			myNumericValueLabel = UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().MonthlyExpenseData[CurrentMonthlyExpenseIndex].ExpenseName[myExpenseAmountIndex];
//			myNumericValueLabel	= "Expense Amount $";
			DataOutState = DataName;
			break;	
		case "PayCheckAmount":
			myNumericValue = UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].PayCheckAmount;
			myNumericValueString = myNumericValue.ToString();
			myNumericValueLabel	 = 	"Pay Check $";
			DataOutState = DataName;
			break;	
		case "DaysWorkedPerMonth":
			myNumericValue = UserBlobManager.GetComponent<UserBlobManager>().UserAppData.NumberOfWorkingDaysPerMonth;
			myNumericValueString = myNumericValue.ToString();
			myNumericValueLabel	 = 	"Days Worked Per Month";
			DataOutState = DataName;
			break;		
		case "TaxPercentage":
			myNumericValue = UserBlobManager.GetComponent<UserBlobManager>().UserAppData.TaxPercentage;
			myNumericValueString = myNumericValue.ToString();
			myNumericValueLabel	 = 	"Tax Percent";
			DataOutState = DataName;
			break;		
		case "HourlyWageAmount":
			myNumericValue = UserBlobManager.GetComponent<UserBlobManager>().UserAppData.HourlyWageAmount;
			myNumericValueString = myNumericValue.ToString();
			myNumericValueLabel	 = 	"Hourly Wage";
			DataOutState = DataName;
			break;				
		}
	}
	
	public void DontPassDataOut()
	{
		switch(DataOutState)
		{
		case "TipAmount":
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;
		case "NumberOfDowns":
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;
		case "HoursWorked":
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;	
		case "TournamentDownAmount":
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;	
		case "NumberOfTournamentDowns":
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;	
		case "TipOut":
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;				
		case "ExpenseAmount":
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("MonthlyExpensesState");
			break;		
		case "PayCheckAmount":
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;	
		case "DaysWorkedPerMonth":
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("ExpensesState");
			break;		
		case "TaxPercentage":
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("ExpensesState");
			break;		
		case "HourlyWageAmount":
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("ExpensesState");
			break;				
		}		
	}
	
	public void PassDataOut()
	{
		switch(DataOutState)
		{
		case "TipAmount":
			UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].TipAmount = CheckNumberValue;
			print (UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].TipAmount.ToString());
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;
		case "NumberOfDowns":
			UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].NumberOfDowns = (int)CheckNumberValue;
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;
		case "HoursWorked":
			UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].HoursWorked = CheckNumberValue;
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;	
		case "TournamentDownAmount":
			UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].TournamentDownAmount = CheckNumberValue;
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;	
		case "NumberOfTournamentDowns":
			UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].NumberOfTournamentDowns = (int)CheckNumberValue;
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;				
		case "TipOut":
			UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].TipOut = CheckNumberValue;
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;	
		case "ExpenseAmount":
//			UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].TipOut = CheckNumberValue;
			var myExpenseAmountIndex = UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().myExpenseAmountIndex;
			var CurrentMonthlyExpenseIndex = UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().CurrentMonthlyExpenseIndex;
			UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().MonthlyExpenseData[CurrentMonthlyExpenseIndex].ExpenseAmount[myExpenseAmountIndex] = CheckNumberValue;
			UserBlobManager.GetComponent<UserBlobManager>().MonthlyExpenseData[CurrentMonthlyExpenseIndex].ExpenseAmount[myExpenseAmountIndex] = CheckNumberValue;
			
			print ("myNumericValue " + myNumericValue.ToString());
			print ("myExpenseAmountIndex " + myExpenseAmountIndex.ToString());
			print ("CurrentMonthlyExpenseIndex " + CurrentMonthlyExpenseIndex.ToString());
			
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("MonthlyExpensesState");
			break;		
		case "PayCheckAmount":
			UIEnterData.GetComponent<UI_EnterData>().myDailyTipData[myTipDataIndex].PayCheckAmount = CheckNumberValue;
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("EnterDataState");
			break;		
		case "DaysWorkedPerMonth":
			UserBlobManager.GetComponent<UserBlobManager>().UserAppData.NumberOfWorkingDaysPerMonth = (int)CheckNumberValue;
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("ExpensesState");
			break;	
		case "TaxPercentage":
			UserBlobManager.GetComponent<UserBlobManager>().UserAppData.TaxPercentage = CheckNumberValue;
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("ExpensesState");
			break;		
		case "HourlyWageAmount":
			UserBlobManager.GetComponent<UserBlobManager>().UserAppData.HourlyWageAmount = CheckNumberValue;
			NumPadKeyBoard = null;
			UIManager.GetComponent<UI_Manager>().SwitchStates("ExpensesState");
			break;				
		}
	}
	
}

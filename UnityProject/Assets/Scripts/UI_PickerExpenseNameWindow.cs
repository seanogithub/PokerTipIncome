using UnityEngine;
using System.Collections;

public class UI_PickerExpenseNameWindow : MonoBehaviour {

	public bool Enabled = true;
	
	public GameObject UIManager;
	public GameObject UIMonthlyExpenses;
	public GameObject  UserBlobManager;

	public string PopUpYesNoDialogState = "";
	private bool PopUpActive = false;
	
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
	
	
//	public string myAlphaNumericValue;
	public string myAlphaNumericValueString;
	private string myAlphaNumericValueLabel;
	
	private float CheckNumberValue;
	private string PreviousmyAlphaNumericValueString;

	public int myExpenseNameIndex = 0;
	private int CurrentMonthlyExpenseIndex = 0;
	
	public string DataOutState = "";
	
	void Awake()
	{
		GetComponent<GUITexture>().pixelInset = new Rect(Screen.width/2, Screen.height/2, 0, 0);		
	}
	
	// Use this for initialization
	void Start () 
	{
		UIManager = GameObject.Find("UI_Manager_Prefab");
		UIMonthlyExpenses = GameObject.Find("UI_MonthlyExpenses_Prefab");
		UserBlobManager = GameObject.Find("UserBlobManager_Prefab");
		
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
			
			// del button	
			if (GUI.Button( new Rect((Screen.width * 0.17f), (Screen.height * 0.65f), (Screen.width * 0.15f), (Screen.height * 0.05f)) ,"Del", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					PopUpYesNoDialog("Are you sure you want to delete this expense name?", this.gameObject.GetComponent<UI_PickerExpenseNameWindow>(), "DeleteExpenseName");
//					RemoveData();				
				}
			}
			
			// new button
			if (GUI.Button( new Rect((Screen.width * 0.68f), (Screen.height * 0.65f), (Screen.width * 0.15f), (Screen.height * 0.05f)) ,"New", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					UIManager.GetComponent<UI_Manager>().SwitchStates("EnterExpenseNameState");
				}
			}
			
			// ok button
			if (GUI.Button( new Rect((Screen.width * 0.2f), (Screen.height * 0.85f), (Screen.width * 0.2f), (Screen.height * 0.1f)) ,"OK", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					myAlphaNumericValueString = this.GetComponent<UI_PickerExpenseName>().pickerParams[0].column[this.GetComponent<UI_PickerExpenseName>().pickerParams[0].selectedIndexReadOnly].ToString();
					PassDataOut();
					UIManager.GetComponent<UI_Manager>().SwitchStates("MonthlyExpensesState");
				}
			}
			
			// cancel button
			if (GUI.Button( new Rect((Screen.width * 0.6f), (Screen.height * 0.85f), (Screen.width * 0.2f), (Screen.height * 0.1f)) ,"Cancel", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					UIManager.GetComponent<UI_Manager>().SwitchStates("MonthlyExpensesState");
				}
			}
		}
	}
	
	void RemoveData()
	{
//		print (this.GetComponent<UI_PickerExpenseName>().pickerParams[0].column.Length.ToString());
//		print (this.GetComponent<UI_PickerExpenseName>().pickerParams[0].selectedIndexReadOnly.ToString());
		if(this.GetComponent<UI_PickerExpenseName>().pickerParams[0].selectedIndexReadOnly != this.GetComponent<UI_PickerExpenseName>().pickerParams[0].column.Length - 1)
		{
			UserBlobManager.GetComponent<UserBlobManager>().UserAppData.ExpenseNameList.RemoveAt(this.GetComponent<UI_PickerExpenseName>().pickerParams[0].selectedIndexReadOnly);
			UserBlobManager.GetComponent<UserBlobManager>().UserAppData.ExpenseNameList.TrimExcess();
			UserBlobManager.GetComponent<UserBlobManager>().SaveAccountData();
		}
		else
		{
			PopUpMessage("Can NOT delete the last entry in the list");
		}
	}
	
	public void PassDataIn( string DataName )
	{
		myExpenseNameIndex = UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().myExpenseNameIndex;
		CurrentMonthlyExpenseIndex = UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().CurrentMonthlyExpenseIndex;
		switch(DataName)
		{
		case "ExpenseName":
			myAlphaNumericValueString = UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().MonthlyExpenseData[CurrentMonthlyExpenseIndex].ExpenseName[myExpenseNameIndex];
			myAlphaNumericValueLabel = "Expense Name";
			DataOutState = DataName;
			break;
		}
	}
	
	public void PassDataOut()
	{
		switch(DataOutState)
		{
		case "ExpenseName":
			UIMonthlyExpenses.GetComponent<UI_MonthlyExpenses>().MonthlyExpenseData[CurrentMonthlyExpenseIndex].ExpenseName[myExpenseNameIndex] = myAlphaNumericValueString;
			break;
		}
	}
	
	void PopUpMessage(string myMessage)
	{
		var temp = Resources.Load("UI/UI_PopUpDialogBox_Prefab") as GameObject;
		var myPopUp = GameObject.Instantiate(temp, (new Vector3(0.0f, 0.0f , 1.0f)), Quaternion.identity ) as GameObject;
		myPopUp.GetComponent<UI_PopUpDialogBox>().Message = myMessage; 
	}

	void PopUpYesNoDialog(string myMessage, Component mySender, string myState)
	{
		var temp = Resources.Load("UI/UI_PopUpYesNoDialogBox_Prefab") as GameObject;
		var myPopUpYesNo = GameObject.Instantiate(temp, (new Vector3(0.0f, 0.0f , 1.0f)), Quaternion.identity ) as GameObject;
		myPopUpYesNo.GetComponent<UI_PopUpYesNoDialogBox>().Message = myMessage; 
		myPopUpYesNo.GetComponent<UI_PopUpYesNoDialogBox>().Sender = mySender; 
		PopUpYesNoDialogState = myState;
		PopUpActive = true;
	}
		
	public void YesNoDialog(string DialogState)
	{
		switch(PopUpYesNoDialogState)
		{
		case "DeleteExpenseName":
			if(DialogState == "ok")
			{
				RemoveData();
			}
			if(DialogState == "cancel")
			{
				print ("DeleteExpenseName cancel");
			}
			break;			
		}
		PopUpYesNoDialogState = "";	
		PopUpActive = false;
	}	
}

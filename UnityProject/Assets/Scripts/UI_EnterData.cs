using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;

[RequireComponent(typeof(GUITexture))]
public class UI_EnterData : MonoBehaviour {
	
	public bool Enabled = true;
	public GameObject UIManager;
	public GameObject  UserBlobManager;

	public string PopUpYesNoDialogState = "";
	private bool PopUpActive = false;
	
	public List<TipData> myDailyTipData = new List<TipData>(); 
	public TipData myTipData = new TipData();
	public int myTipDataIndex = 0;
	
	public string JobName = "";
//	public System.DateTime CurrentDate = new System.DateTime();
	public string TipAmountString = "";
	public float TipAmount = 0.0f;
	public string NumberOfDownsString = "";
	public int NumberOfDowns = 0;
	public System.DateTime TimeStarted = new System.DateTime();
	public string HoursWorkedString = "";
	public float HoursWorked = 0.0f;
	public string TournamentDownsString = "No";
	public bool TournamentDowns = false;
	public string TournamentName = "";
	public string TournamentDownAmountString = "";
	public float TournamentDownAmount = 0.0f;
	public string NumberOfTournamentDownsString = "";
	public int NumberOfTournamentDowns = 0;
	public string TipOutString = "";
	public float TipOut = 0.0f;
	public string Notes = "";
	public string WorkDayString = "";
	public string WorkDayStartTimeString = "";
	public float PayCheckAmount = 0.0f;
	public string PayCheckAmountString = "";
	
	public Texture2D BackgroundBitmap;	
	public Texture2D styleCalendarButtonsBitmap;	
	public Texture2D styleCalendarButtonsActiveBitmap;	
	public Texture2D styleCalendarButtonsTodayBitmap;	
	public Texture2D style2XButtonBlueBitmap;
	public Texture2D styleCalendarLeftArrowButtonBitmap;
	public Texture2D styleCalendarRightArrowButtonBitmap;

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
	
	public Font myFont;
	public int myFontSize = 30;
	
	private float AspectRatioMultiplier = 0.57f;

	TouchScreenKeyboard NumPadKeyBoard;
	TouchScreenKeyboard DefaultKeyBoard;
	
	public string PreviousTipAmountString;
	
	void Awake()
	{
		GetComponent<GUITexture>().pixelInset = new Rect(Screen.width/2, Screen.height/2, 0, 0);		
	}
	
	void Start () 
	{
		UIManager = GameObject.Find("UI_Manager_Prefab");
		UserBlobManager = GameObject.Find("UserBlobManager_Prefab");

		AspectRatioMultiplier = ((float)Screen.width/1200 );	
		
	}

	public void GUIEnabled(bool myValue)
	{
		Enabled = myValue;
		this.GetComponent<GUITexture>().enabled = myValue;
	}
	
// THIS IS THE DUAL LIVE/TOURNAMENT LAYOUT	
	void OnGUI()
	{
		if (Enabled && myTipDataIndex < myDailyTipData.Count)
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
			GetComponent<GUITexture>().texture = UserBlobManager.GetComponent<UserBlobManager>().EnterDataBackground;
			
			// number of entries label
			GUI.Label(new Rect((Screen.width * 0.50f), (Screen.height * 0.01f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), ((myTipDataIndex + 1).ToString() + " of " +  myDailyTipData.Count.ToString()) , styleBigger);
			
			// date label
			GUI.Label(new Rect((Screen.width * 0.50f), (Screen.height * 0.04f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (myDailyTipData[myTipDataIndex].Date.Month.ToString() +"-"+ myDailyTipData[myTipDataIndex].Date.Day.ToString() +"-"+ myDailyTipData[myTipDataIndex].Date.Year.ToString()), styleBigger);
			
			// button delete
			if (GUI.Button( new Rect((Screen.width * 0.22f), (Screen.height * 0.02f), (Screen.width * 0.15f), (Screen.height * 0.05f)) ,"Del", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					PopUpYesNoDialog("Are you sure you want to delete this data?", this.gameObject.GetComponent<UI_EnterData>(), "DeleteDailyData");
//					RemoveData();
				}
			}
			
			// new
			if (GUI.Button( new Rect((Screen.width * 0.63f), (Screen.height * 0.02f), (Screen.width * 0.15f), (Screen.height * 0.05f)) ,"New", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					AddNewData();
				}
			}
			
			// button previous
			if (GUI.Button( new Rect((Screen.width * 0.06f), (Screen.height * 0.02f), (Screen.width * 0.15f), (Screen.height * 0.05f)) ,"Prev", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{
					myTipDataIndex -=1;
					if (myTipDataIndex <= 0)
					{
						myTipDataIndex = 0;
					}
				}
			}
		
			// button next
			if (GUI.Button( new Rect((Screen.width * 0.79f), (Screen.height * 0.02f), (Screen.width * 0.15f), (Screen.height * 0.05f)) ,"Next", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{
					myTipDataIndex +=1;
					if (myTipDataIndex >= myDailyTipData.Count )
					{
						myTipDataIndex = myDailyTipData.Count - 1;
					}	
				}
			}
			
			// data fields
			if (myTipDataIndex < myDailyTipData.Count )
			{
				// job name
				GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.10f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Job Name", style2XBlack);
				if(GUI.Button(new Rect((Screen.width * 0.4f), (Screen.height * 0.10f), (Screen.width * 0.5f), (Screen.height * 0.05f)), myDailyTipData[myTipDataIndex].JobName, styleEnterDataButtons))
				{
					if(PopUpActive == false)
					{					
						UIManager.GetComponent<UI_Manager>().SwitchStates("PickerJobNameState");
					}
				}	
				
/*				
//				GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.17f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Live or Tournament", style2XBlack);
				if (GUI.Toggle(new Rect((Screen.width * 0.4f), (Screen.height * 0.17f), (Screen.width * 0.35f), (Screen.height * 0.05f)), myDailyTipData[myTipDataIndex].TournamentDowns, TournamentDownsString, styleEnterDataButtons) != myDailyTipData[myTipDataIndex].TournamentDowns)
				{
					if(PopUpActive == false)
					{
						myDailyTipData[myTipDataIndex].TournamentDowns = !myDailyTipData[myTipDataIndex].TournamentDowns;
						if(myDailyTipData[myTipDataIndex].TournamentDowns)
						{
							myDailyTipData[myTipDataIndex].TipAmount = 0;
							myDailyTipData[myTipDataIndex].TipOut = 0;
							myDailyTipData[myTipDataIndex].NumberOfDowns = 0;
						}
						else
						{
							myDailyTipData[myTipDataIndex].TournamentDownAmount = 0;
							myDailyTipData[myTipDataIndex].NumberOfTournamentDowns = 0;
						}
					}
				}
*/				

				// workday 
				GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.16f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Work Schedule", style2XBlack);
				if(GUI.Button(new Rect((Screen.width * 0.4f), (Screen.height * 0.16f), (Screen.width * 0.25f), (Screen.height * 0.05f)), WorkDayString, styleEnterDataButtons))
				{
					if(PopUpActive == false)
					{
						myDailyTipData[myTipDataIndex].WorkDay = !myDailyTipData[myTipDataIndex].WorkDay;
						if(myDailyTipData[myTipDataIndex].WorkDay ==  false)
						{
							myDailyTipData[myTipDataIndex].WorkDayStartTime = new System.DateTime(myDailyTipData[myTipDataIndex].Date.Year,myDailyTipData[myTipDataIndex].Date.Month,myDailyTipData[myTipDataIndex].Date.Day,0,0,0);
						}
					}
				}	
				
				// workday start time
				if(myDailyTipData[myTipDataIndex].WorkDay)
				{
					GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.22f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Start Time", style2XBlack);
					if(GUI.Button(new Rect((Screen.width * 0.4f), (Screen.height * 0.22f), (Screen.width * 0.25f), (Screen.height * 0.05f)), WorkDayStartTimeString, styleEnterDataButtons))
					{
						if(PopUpActive == false)
						{
							UIManager.GetComponent<UI_Manager>().SwitchStates("PickerWorkDayStartTimeState");
						}
					}				
				}	
				
				// hours worked 
				GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.28f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Hours Worked", style2XBlack);
				if(GUI.Button(new Rect((Screen.width * 0.4f), (Screen.height * 0.28f), (Screen.width * 0.25f), (Screen.height * 0.05f)), HoursWorkedString, styleEnterDataButtons))
				{
					if(PopUpActive == false)
					{
						UIManager.GetComponent<UI_Manager>().SwitchStates("EnterHoursWorked");
					}
				}	

				GUI.Label(new Rect((Screen.width * 0.34f), (Screen.height * 0.34f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Live Game Data", style2XBlack);
				
				// tip amount button
				GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.38f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Tips $", style2XBlack);
				if(GUI.Button(new Rect((Screen.width * 0.4f), (Screen.height * 0.38f), (Screen.width * 0.25f), (Screen.height * 0.05f)), TipAmountString, styleEnterDataButtons))
				{
					if(PopUpActive == false)
					{
						UIManager.GetComponent<UI_Manager>().SwitchStates("EnterTipAmount");
					}
				}

				// number of downs
				GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.44f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Downs", style2XBlack);
				if(GUI.Button(new Rect((Screen.width * 0.4f), (Screen.height * 0.44f), (Screen.width * 0.25f), (Screen.height * 0.05f)), NumberOfDownsString, styleEnterDataButtons))
				{
					if(PopUpActive == false)
					{
						UIManager.GetComponent<UI_Manager>().SwitchStates("EnterNumberOfDowns");
					}
				}	
				
				// tip out
				GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.50f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Tip Out $", style2XBlack);
				if(GUI.Button(new Rect((Screen.width * 0.4f), (Screen.height * 0.50f), (Screen.width * 0.25f), (Screen.height * 0.05f)), TipOutString, styleEnterDataButtons))
				{
					if(PopUpActive == false)
					{
						UIManager.GetComponent<UI_Manager>().SwitchStates("EnterTipOut");
					}
				}					

				GUI.Label(new Rect((Screen.width * 0.35f), (Screen.height * 0.56f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Tournament Data", style2XBlack);
				
				// tournament name
				GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.61f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Tournament Name", style2XBlack);
				if(GUI.Button(new Rect((Screen.width * 0.4f), (Screen.height * 0.61f), (Screen.width * 0.5f), (Screen.height * 0.05f)), myDailyTipData[myTipDataIndex].TournamentName, styleEnterDataButtons))
				{
					if(PopUpActive == false)
					{
						UIManager.GetComponent<UI_Manager>().SwitchStates("PickerTournamentNameState");
					}
				}					
				
		
				// number of downs
				GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.67f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Tournament Downs", style2XBlack);
				if(GUI.Button(new Rect((Screen.width * 0.4f), (Screen.height * 0.67f), (Screen.width * 0.25f), (Screen.height * 0.05f)), NumberOfTournamentDownsString, styleEnterDataButtons))
				{
					if(PopUpActive == false)
					{
						UIManager.GetComponent<UI_Manager>().SwitchStates("EnterNumberOfTournamentDowns");
					}
				}

				// tournament $ per down
				GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.73f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Tourney $ Per Down", style2XBlack);
				if(GUI.Button(new Rect((Screen.width * 0.4f), (Screen.height * 0.73f), (Screen.width * 0.25f), (Screen.height * 0.05f)), TournamentDownAmountString, styleEnterDataButtons))
				{
					if(PopUpActive == false)
					{
						UIManager.GetComponent<UI_Manager>().SwitchStates("EnterTournamentDownAmount");
					}
				}					
				
				GUI.Label(new Rect((Screen.width * 0.35f), (Screen.height * 0.79f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Pay Check Data", style2XBlack);
				GUI.Label(new Rect((Screen.width * 0.05f), (Screen.height * 0.82f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Pay Check $", style2XBlack);
			
				// pay check amount
				if(GUI.Button(new Rect((Screen.width * 0.4f), (Screen.height * 0.82f), (Screen.width * 0.25f), (Screen.height * 0.05f)), PayCheckAmountString, styleEnterDataButtons))
				{
					if(PopUpActive == false)
					{
						UIManager.GetComponent<UI_Manager>().SwitchStates("EnterPayCheckAmountState");
					}
				}	
				
				// ok button
				if (GUI.Button( new Rect((Screen.width * 0.2f), (Screen.height * 0.90f), (Screen.width * 0.2f), (Screen.height * 0.1f)) ,"OK", style2XButtonBlue))
				{
					if(PopUpActive == false)
					{
						OKPressed();
					}
				}
				
				// cancel button
				if (GUI.Button( new Rect((Screen.width * 0.6f), (Screen.height * 0.90f), (Screen.width * 0.2f), (Screen.height * 0.1f)) ,"Cancel", style2XButtonBlue))
				{
					if(PopUpActive == false)
					{
						CancelPressed();
					}
				}
			}
		}
	}	
	
	void OKPressed()
	{
		SendNewData();
		// convert strings to correct data types, add to user data list
		UIManager.GetComponent<UI_Manager>().SwitchStates("CalendarState");
	}
	
	void CancelPressed()
	{
		myDailyTipData = new List<TipData>();
		UIManager.GetComponent<UI_Manager>().UpdateEnteredData = false;
		UIManager.GetComponent<UI_Manager>().myDailyTipData = new List<TipData>();
		UIManager.GetComponent<UI_Manager>().SwitchStates("CalendarState");
	}
	
	public void GetNewData()
	{
		myDailyTipData = new List<TipData>();
		myDailyTipData = UIManager.GetComponent<UI_Manager>().myDailyTipData;
	}
	
	public void SendNewData()
	{
		UIManager.GetComponent<UI_Manager>().UpdateEnteredData = true;
		UIManager.GetComponent<UI_Manager>().myDailyTipData = new List<TipData>();
		UIManager.GetComponent<UI_Manager>().myDailyTipData = myDailyTipData;
	}
	
	void AddNewData()
	{
		var tempTipData = new TipData();
		tempTipData.JobName = "Golden Nugget";
		tempTipData.Date = myDailyTipData[myTipDataIndex].Date;
		tempTipData.TipAmount = 0.0f;
		tempTipData.NumberOfDowns = 0;
		tempTipData.HoursWorked = 0.0f;
		tempTipData.TournamentDowns = false;
		tempTipData.TournamentName = "";
		tempTipData.NumberOfTournamentDowns = 0;
		tempTipData.TipOut = 0.0f;
		tempTipData.Notes = "";	
		tempTipData.WorkDay = false;
		tempTipData.WorkDayStartTime = new System.DateTime(myDailyTipData[myTipDataIndex].Date.Year,myDailyTipData[myTipDataIndex].Date.Month,myDailyTipData[myTipDataIndex].Date.Day,0,0,0); //myDailyTipData[myTipDataIndex].Date;
		tempTipData.PayCheckAmount = 0.0f;
		
		myDailyTipData.Add(tempTipData);
		myDailyTipData.TrimExcess();
		myTipDataIndex = myDailyTipData.Count - 1;
//		print ("AddNewData");
	}

	void RemoveData()
	{
		myDailyTipData.TrimExcess();
		if(myDailyTipData.Count == 1)
		{
			myDailyTipData.RemoveAt(myTipDataIndex);
			myDailyTipData = new List<TipData>();
//			Enabled = false;
			SendNewData();
			UIManager.GetComponent<UI_Manager>().SwitchStates("CalendarState");
		}
		else
		{
			myDailyTipData.RemoveAt(myTipDataIndex);
			myDailyTipData.Capacity = myDailyTipData.Count;
			myDailyTipData.TrimExcess();
			myTipDataIndex -= 1;
			if (myTipDataIndex <= 0)
			{
				myTipDataIndex = 0;
			}			
//			SendNewData();
		}
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
		case "DeleteDailyData":
			if(DialogState == "ok")
			{
				RemoveData();
			}
			if(DialogState == "cancel")
			{
				print ("DeleteDailyData cancel");
			}
			break;			
		}
		PopUpYesNoDialogState = "";	
		PopUpActive = false;
	}
	
	string DateTimeToTimeString(System.DateTime myDateTime)
	{
		var newHour = myDateTime.Hour;
		var newMinute = myDateTime.Minute.ToString();
		var newAMPM = "am";
		
		if (newHour > 12)
		{
			newHour = newHour - 12;
			newAMPM = "pm";
		}
		
		if (newMinute.ToString() ==  "0")
		{
			newMinute = "00";
		}

		var newDateTimeString = newHour.ToString() + ":" + newMinute + newAMPM;
		return newDateTimeString;
	}
	
	void Update()
	{
		if (Enabled)
		{	
//			print ("index " + myTipDataIndex.ToString());
			TipAmountString = myDailyTipData[myTipDataIndex].TipAmount.ToString();
			NumberOfDownsString = myDailyTipData[myTipDataIndex].NumberOfDowns.ToString();
			NumberOfTournamentDownsString = myDailyTipData[myTipDataIndex].NumberOfTournamentDowns.ToString();
			HoursWorkedString = myDailyTipData[myTipDataIndex].HoursWorked.ToString();
			TournamentDownAmountString = myDailyTipData[myTipDataIndex].TournamentDownAmount.ToString();
			TipOutString = myDailyTipData[myTipDataIndex].TipOut.ToString();
			PayCheckAmountString = myDailyTipData[myTipDataIndex].PayCheckAmount.ToString();
			
			if(myDailyTipData[myTipDataIndex].TournamentDowns)
			{
				TournamentDownsString = "Tournament";
			}
			else
			{
				TournamentDownsString = "Live Game";
			}	

			if(myDailyTipData[myTipDataIndex].WorkDay)
			{
				WorkDayString = "Yes";
			}
			else
			{
				WorkDayString = "No";
			}		
			
			WorkDayStartTimeString = DateTimeToTimeString(myDailyTipData[myTipDataIndex].WorkDayStartTime);
		}
//		print ("Enter Data count " + myDailyTipData.Count.ToString());	
		
	}
}

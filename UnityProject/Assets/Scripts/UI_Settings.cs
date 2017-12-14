using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GUITexture))]
public class UI_Settings : MonoBehaviour {
	
	public bool Enabled = true;

	public GameObject UIManager;
	public GameObject UserBlobManager;
		
	public string PopUpYesNoDialogState = "";
	private bool PopUpActive = false;
	
	public Texture2D BackgroundBitmap;
	public Texture2D style2XButtonBlueBitmap;
	public Texture2D style2XButtonBlueActiveBitmap;

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

	private Color FontColor;
	private Color ButtonColor;
	private Color BackgroundColor;
	
	private float AspectRatioMultiplier = 0.57f;
	
	public float PassCodeTimerSliderValue = 1.0f;
	public string PassCodeTimerSliderValueString = "";
	
	void Awake()
	{
		GetComponent<GUITexture>().pixelInset = new Rect(Screen.width/2, Screen.height/2, 0, 0);		
	}
	
	void Start () 
	{
		UIManager = GameObject.Find("UI_Manager_Prefab");
		UserBlobManager = GameObject.Find("UserBlobManager_Prefab");
		
		PassCodeTimerSliderValue = UserBlobManager.GetComponent<UserBlobManager>().UserAppData.PassCodeTimer / 60;

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
			GUI.skin = UserBlobManager.GetComponent<UserBlobManager>().CurrentSkin;
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
			
			AspectRatioMultiplier = ((float)Screen.width/1200 );

			// background image
			GetComponent<GUITexture>().color = BackgroundColor;
			GetComponent<GUITexture>().texture = UserBlobManager.GetComponent<UserBlobManager>().EnterDataBackground;			
//			GUI.DrawTexture((new Rect(0,0,Screen.width,Screen.height)), BackgroundBitmap,ScaleMode.StretchToFill,true);

			// time and month and year		
			GUI.Label(new Rect((Screen.width * 0.5f), (Screen.height* 0.005f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (System.DateTime.Now.ToShortTimeString()), styleBigger);

			// rate app button
			if (GUI.Button( new Rect((Screen.width * 0.1f), (Screen.height * 0.10f), (Screen.width * 0.8f), (Screen.height * 0.075f)) ,"Rate This App", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{
//					PopUpMessage("Sorry, Feature Not Implemented Yet");

					// need to use this the app is published
//					Application.OpenURL ("market://details?id=com.trollugames.caverun3d");
					
#if UNITY_ANDROID || UNITY_EDITOR || UNITY_STANDALONE					
					Application.OpenURL ("https://play.google.com/store");	
#elif UNITY_IPHONE
					Application.OpenURL ("https://itunes.apple.com/us/genre/ios/id36?mt=8");	
#endif
				}
			}	

			// change skin button
			if (GUI.Button( new Rect((Screen.width * 0.1f), (Screen.height * 0.18f), (Screen.width * 0.8f), (Screen.height * 0.075f)) ,"Change Skin", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{
					UIManager.GetComponent<UI_Manager>().SwitchStates("PickerSkinState");
//					PopUpMessage("Sorry, Feature Not Implemented Yet");
				}
			}	
			
			// color settings
			if (GUI.Button( new Rect((Screen.width * 0.1f), (Screen.height * 0.26f), (Screen.width * 0.8f), (Screen.height * 0.075f)) ,"Color Settings", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					UIManager.GetComponent<UI_Manager>().SwitchStates("ColorSettingsState");
				}
			}	

			// enter new pass code
			if (GUI.Button( new Rect((Screen.width * 0.1f), (Screen.height * 0.34f), (Screen.width * 0.8f), (Screen.height * 0.075f)) ,"Enter New Pass Code", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					UIManager.GetComponent<UI_Manager>().SwitchStates("EnterPassCodeState");
				}
			}
/*			
			// use pass code
			if(UserBlobManager.GetComponent<UserBlobManager>().UserAppData.PassCode != "")
			{
				GUI.Label(new Rect((Screen.width * 0.25f), (Screen.height* 0.445f), 100, 20), "Use Pass Code:", styleBigger);
	
				var PassCodeString = "NO"; //UserBlobManager.GetComponent<UserBlobManager>().UserAppData.UsePassCode.ToString();
				if (UserBlobManager.GetComponent<UserBlobManager>().UserAppData.UsePassCode)
				{
					PassCodeString = "YES";
				}
				if (GUI.Button( new Rect((Screen.width * 0.55f), (Screen.height * 0.42f), (Screen.width * 0.2f), (Screen.height * 0.075f)) , PassCodeString, style2XButtonBlue))
				{
					if(PopUpActive == false)
					{				
						if(PassCodeString == "NO")
						{
							PopUpYesNoDialog("WARNING:\nIf you forget your pass code then you will need to delete the application from your device and reinstall it. Then your data will be lost\n\n\n\n", this.gameObject.GetComponent<UI_Settings>(), "UsePassCode");
//							PopUpMessage("WARNING:\n\nIf you forget your pass code then you will need to delete the application from your device and reinstall it.");
						}
						else
						{
							UserBlobManager.GetComponent<UserBlobManager>().UserAppData.UsePassCode = !UserBlobManager.GetComponent<UserBlobManager>().UserAppData.UsePassCode;
							UserBlobManager.GetComponent<UserBlobManager>().SaveAccountData();
						}
					}
				}	
			}	
*/
			
			// pass code timer slider
			if(UserBlobManager.GetComponent<UserBlobManager>().UserAppData.PassCode != "")
			{
				GUI.Label(new Rect((Screen.width * 0.20f), (Screen.height* 0.42f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), "Pass Code Timer", styleBigger);
				
				
				if(Mathf.Floor(PassCodeTimerSliderValue) == 0)
				{
					PassCodeTimerSliderValueString = "OFF";
				}
				else
				{
					PassCodeTimerSliderValueString = Mathf.Floor(PassCodeTimerSliderValue).ToString() + " minutes";
				}
				GUI.Label(new Rect((Screen.width * 0.20f), (Screen.height* 0.46f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (PassCodeTimerSliderValueString), styleBigger);
	
				PassCodeTimerSliderValue = GUI.HorizontalSlider(new Rect((Screen.width * 0.45f), (Screen.height* 0.42f), (Screen.width * 0.5f), (Screen.height * 0.075f)), PassCodeTimerSliderValue, 0.0f, 5.0f);
				
				if(UserBlobManager.GetComponent<UserBlobManager>().UserAppData.PassCodeTimer /60 != Mathf.Floor(PassCodeTimerSliderValue))
				{
					if(Mathf.Floor(PassCodeTimerSliderValue) == 0)
					{
						UserBlobManager.GetComponent<UserBlobManager>().UserAppData.UsePassCode = false;
						UserBlobManager.GetComponent<UserBlobManager>().UserAppData.PassCodeTimer = 0;
					}
					else
					{
						UserBlobManager.GetComponent<UserBlobManager>().UserAppData.UsePassCode = true;
						UserBlobManager.GetComponent<UserBlobManager>().UserAppData.PassCodeTimer = Mathf.Floor(PassCodeTimerSliderValue) * 60;
					}
					UserBlobManager.GetComponent<UserBlobManager>().SaveAccountData();
				}
				
			}

			// email support button
			if (GUI.Button( new Rect((Screen.width * 0.1f), (Screen.height * 0.50f), (Screen.width * 0.8f), (Screen.height * 0.075f)) ,"Email Support", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					var ExportDataString = CreateSupportEmailString();
					Application.OpenURL(ExportDataString);
//					Application.OpenURL("mailto:yesyoucan@hotmail.com?subject=Email&body=from Unity");
				}
			}	
			
			// export data button
			if (GUI.Button( new Rect((Screen.width * 0.1f), (Screen.height * 0.58f), (Screen.width * 0.8f), (Screen.height * 0.075f)) ,"Export Data", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					var ExportDataString = CreateExportDataString();
					Application.OpenURL(ExportDataString);
//					Application.OpenURL("mailto:yesyoucan@hotmail.com?subject=Email&body=from Unity");
				}
			}			

			// import data button
			if (GUI.Button( new Rect((Screen.width * 0.1f), (Screen.height * 0.66f), (Screen.width * 0.8f), (Screen.height * 0.075f)) ,"Import Data", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					UIManager.GetComponent<UI_Manager>().SwitchStates("ImportDataState");
				}
			}			
			
			// delete all data button
			if (GUI.Button( new Rect((Screen.width * 0.1f), (Screen.height * 0.74f), (Screen.width * 0.8f), (Screen.height * 0.075f)) ,"Delete All Data", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					PopUpYesNoDialog("Are you sure you want to delete all data?", this.gameObject.GetComponent<UI_Settings>(), "DeleteAllData");
//					UserBlobManager.GetComponent<UserBlobManager>().DeletePlayerData();
//					PopUpMessage("All Data has been deleted!");
				}
			}	

			// back button
			if (GUI.Button( new Rect((Screen.width * 0.25f), (Screen.height * 0.90f), (Screen.width * 0.5f), (Screen.height * 0.075f)) ,"Back", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					UIManager.GetComponent<UI_Manager>().SwitchStates("CalendarState");
				}
			}			
		}
	}
	
	public string CreateExportDataString()
	{
		var ExportDataString = "mailto:?subject=Export Poker Tip Income Data&body=";
		var DailyTipData = new List<TipData>(); 
		DailyTipData = UserBlobManager.GetComponent<UserBlobManager>().DailyTipData;
		DailyTipData.Sort( delegate(TipData DTD1, TipData DTD2) { return DTD1.Date.CompareTo(DTD2.Date); });

		for(int i = 0; i < DailyTipData.Count ; i++)
		{
			ExportDataString += 
				DailyTipData[i].JobName.ToString() + "," +
				DailyTipData[i].Date.ToString() + "," +
				DailyTipData[i].TipAmount.ToString() + "," +
				DailyTipData[i].NumberOfDowns.ToString() + "," +
				DailyTipData[i].HoursWorked.ToString() + "," +
				DailyTipData[i].TournamentDowns.ToString() + "," +
				DailyTipData[i].TournamentName.ToString() + "," +
				DailyTipData[i].TournamentDownAmount.ToString() + "," +
				DailyTipData[i].NumberOfTournamentDowns.ToString() + "," +
				DailyTipData[i].TipOut.ToString() + "," +
				DailyTipData[i].Notes.ToString() + "," +
				DailyTipData[i].WorkDay.ToString() + "," +
				DailyTipData[i].WorkDayStartTime.ToString() + "," +
				DailyTipData[i].PayCheckAmount.ToString();
			if (i == DailyTipData.Count - 1)
			{
				ExportDataString += "\n";
			}
			else
			{
				ExportDataString += ",\n";
			}
		}
		ExportDataString = ExportDataString.Replace(" ", "%20");
		ExportDataString = ExportDataString.Replace("\n", "%0D");
		
		return ExportDataString;
	}
	
	public string CreateSupportEmailString()
	{
		var ExportDataString = "mailto:PokerDealerIncomeTracker@gmail.com?subject=Help With Poker Dealer Income Tracker&body=";
		var DailyTipData = new List<TipData>(); 
		DailyTipData = UserBlobManager.GetComponent<UserBlobManager>().DailyTipData;
		DailyTipData.Sort( delegate(TipData DTD1, TipData DTD2) { return DTD1.Date.CompareTo(DTD2.Date); });
		ExportDataString += "\n\n";
		for(int i = 0; i < DailyTipData.Count ; i++)
		{
			ExportDataString += 
				DailyTipData[i].JobName.ToString() + "," +
				DailyTipData[i].Date.ToString() + "," +
				DailyTipData[i].TipAmount.ToString() + "," +
				DailyTipData[i].NumberOfDowns.ToString() + "," +
				DailyTipData[i].HoursWorked.ToString() + "," +
				DailyTipData[i].TournamentDowns.ToString() + "," +
				DailyTipData[i].TournamentName.ToString() + "," +
				DailyTipData[i].TournamentDownAmount.ToString() + "," +
				DailyTipData[i].NumberOfTournamentDowns.ToString() + "," +
				DailyTipData[i].TipOut.ToString() + "," +
				DailyTipData[i].Notes.ToString() + "," +
				DailyTipData[i].WorkDay.ToString() + "," +
				DailyTipData[i].WorkDayStartTime.ToString() + "," +
				DailyTipData[i].PayCheckAmount.ToString();
			if (i == DailyTipData.Count - 1)
			{
				ExportDataString += "\n";
			}
			else
			{
				ExportDataString += ",\n";
			}
		}
		ExportDataString = ExportDataString.Replace(" ", "%20");
		ExportDataString = ExportDataString.Replace("\n", "%0D");
		
		return ExportDataString;
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
		case "ResetAllColors":
			if(DialogState == "ok")
			{
				UserBlobManager.GetComponent<UserBlobManager>().FontColor = new Color( 0.0f, 0.0f, 0.0f, 1.0f);  // Black
				UserBlobManager.GetComponent<UserBlobManager>().ButtonColor = new Color( 0.37f, 0.63f, 1.0f, 1.0f);  // Light Blue
				UserBlobManager.GetComponent<UserBlobManager>().BackGroundColor = new Color( 0.50f, 0.50f, 0.50f, 1.0f);  // Light Gray	
				UserBlobManager.GetComponent<UserBlobManager>().SaveAccountData();
				PopUpMessage("All colors have been reset.");
//				UIManager.GetComponent<UI_Manager>().SwitchStates("BackgroundColorPickerState");
			}
			if(DialogState == "cancel")
			{
				print ("ResetAllColors cancel");
			}
			break;
		case "DeleteAllData":
			if(DialogState == "ok")
			{
				UserBlobManager.GetComponent<UserBlobManager>().DeletePlayerData();
			}
			if(DialogState == "cancel")
			{
				print ("DeleteAllData cancel");
			}
			break;		
		case "UsePassCode":
			if(DialogState == "ok")
			{
				UserBlobManager.GetComponent<UserBlobManager>().UserAppData.UsePassCode = !UserBlobManager.GetComponent<UserBlobManager>().UserAppData.UsePassCode;
				UserBlobManager.GetComponent<UserBlobManager>().SaveAccountData();
			}
			if(DialogState == "cancel")
			{
				print ("UsePassCode cancel");
			}
			break;				
		}
		PopUpYesNoDialogState = "";	
		PopUpActive = false;
	}
}

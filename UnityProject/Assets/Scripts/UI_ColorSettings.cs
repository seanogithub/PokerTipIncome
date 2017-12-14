using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GUITexture))]
public class UI_ColorSettings : MonoBehaviour {

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
	private GUIStyle styleEnterDataButtons;
	private GUIStyle style2XBlack;
	
	public Color FontColor;
	public Color ButtonColor;
	public Color BackgroundColor;
	
	private float AspectRatioMultiplier = 0.57f;
	
	void Awake()
	{
		GetComponent<GUITexture>().pixelInset = new Rect(Screen.width/2, Screen.height/2, 0, 0);		
	}
	
	void Start () 
	{
		UIManager = GameObject.Find("UI_Manager_Prefab");
		UserBlobManager = GameObject.Find("UserBlobManager_Prefab");
		
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
			GetComponent<GUITexture>().texture = UserBlobManager.GetComponent<UserBlobManager>().EnterDataBackground;
			
			// time and month and year		
			GUI.Label(new Rect((Screen.width * 0.5f), (Screen.height* 0.005f), (float)style.fontSize * AspectRatioMultiplier, (float)style.fontSize * AspectRatioMultiplier), (System.DateTime.Now.ToShortTimeString()), styleBigger);
			
			// change font color button
			if (GUI.Button( new Rect((Screen.width * 0.1f), (Screen.height * 0.10f), (Screen.width * 0.8f), (Screen.height * 0.075f)) ,"Change Font Color", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{
					UIManager.GetComponent<UI_Manager>().SwitchStates("FontColorPickerState");
				}
			}	

			// change button color button
			if (GUI.Button( new Rect((Screen.width * 0.1f), (Screen.height * 0.18f), (Screen.width * 0.8f), (Screen.height * 0.075f)) ,"Change Button Color", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					UIManager.GetComponent<UI_Manager>().SwitchStates("ButtonColorPickerState");
				}
			}	

			// change background color button
			if (GUI.Button( new Rect((Screen.width * 0.1f), (Screen.height * 0.26f), (Screen.width * 0.8f), (Screen.height * 0.075f)) ,"Change Background Color", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					UIManager.GetComponent<UI_Manager>().SwitchStates("BackgroundColorPickerState");
				}
			}	

			// reset all color button
			if (GUI.Button( new Rect((Screen.width * 0.1f), (Screen.height * 0.34f), (Screen.width * 0.8f), (Screen.height * 0.075f)) ,"Reset All Colors", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{
					PopUpYesNoDialog("Are you sure you want to reset all the color settings?", this.gameObject.GetComponent<UI_ColorSettings>(), "ResetAllColors");
				}
			}	
			
			// back button
			if (GUI.Button( new Rect((Screen.width * 0.25f), (Screen.height * 0.90f), (Screen.width * 0.5f), (Screen.height * 0.075f)) ,"Back", style2XButtonBlue))
			{
				if(PopUpActive == false)
				{				
					UIManager.GetComponent<UI_Manager>().SwitchStates("SettingsState");
				}
			}			
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
		case "ResetAllColors":
			if(DialogState == "ok")
			{
				UserBlobManager.GetComponent<UserBlobManager>().FontColor = new Color( 0.0f, 0.0f, 0.0f, 1.0f);  // Black
				UserBlobManager.GetComponent<UserBlobManager>().ButtonColor = new Color( 0.37f, 0.63f, 1.0f, 1.0f);  // Light Blue
				UserBlobManager.GetComponent<UserBlobManager>().BackGroundColor = new Color( 0.50f, 0.50f, 0.50f, 1.0f);  // Light Gray	
				UserBlobManager.GetComponent<UserBlobManager>().ChangeCurrentSkin();
				UserBlobManager.GetComponent<UserBlobManager>().SaveAccountData();
				PopUpMessage("All colors have been reset.");
//				UIManager.GetComponent<UI_Manager>().SwitchStates("BackgroundColorPickerState");
			}
			if(DialogState == "cancel")
			{
				print ("ResetAllColors cancel");
			}
			break;
		}
		PopUpYesNoDialogState = "";	
		PopUpActive = false;
	}
}

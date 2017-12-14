using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class SplashScreen : MonoBehaviour {

	public bool Enabled = true;

	public GameObject UIManager;
	public GameObject UserBlobManager;
	public GameObject UIPasswordScreen;
	
//    public float fadeSpeed = 1f;          // Speed that the screen fades to and from black.
	public float SplashScreenTime = 3.0f;
	public Texture2D SplashScreenBitmap;
	public float myTimer = 0;
	
	
    void Awake ()
    {
		myTimer = SplashScreenTime;
		
		// Set the texture so that it is the the size of the screen and covers it.
//		guiTexture.pixelInset = new Rect(Screen.width/2, Screen.height/2, 0, 0);		
    }
	
	void Start()
	{
		UIManager = GameObject.Find("UI_Manager_Prefab");
		UserBlobManager = GameObject.Find("UserBlobManager_Prefab");
		UIPasswordScreen = GameObject.Find("UI_PasswordScreen_Prefab");
	}

	public void GUIEnabled(bool myValue)
	{
		Enabled = myValue;
		this.GetComponent<GUITexture>().enabled = myValue;
	}
/*	
	void OnGUI()
	{
		if(Enabled == true)
		{
			GUI.DrawTexture((new Rect(0,0,Screen.width, Screen.height)), SplashScreenBitmap,ScaleMode.StretchToFill,true);
		}
	}
*/	
    void Update ()
    {
		if(Enabled == true)
		{
			this.GetComponent<GUITexture>().pixelInset = new Rect(Screen.width/2, Screen.height/2, 0, 0);		
			myTimer -= Time.deltaTime;
			if (myTimer < 0)
			{
				if(UserBlobManager.GetComponent<UserBlobManager>().UserAppData.UsePassCode == true)
				{
					UIPasswordScreen.GetComponent<UI_PasswordScreen>().ClearPassCode();					
					UIManager.GetComponent<UI_Manager>().SwitchStates("PasswordScreenState");
					myTimer = SplashScreenTime;
				}
				else
				{
					UIManager.GetComponent<UI_Manager>().SwitchStates("CalendarState");
					myTimer = SplashScreenTime;
				}
			}
		}
	}
}

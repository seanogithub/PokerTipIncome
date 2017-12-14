using UnityEngine;
using System.Collections;

public class ExpenseData  {
	
	public System.DateTime Date = new System.DateTime();
	public string[] ExpenseName = new string[15];
	public float[] ExpenseAmount = new float[15];
	
	public void Initialize()
	{
		ExpenseName[0] = "Rent";
		ExpenseName[1] = "Car";
		ExpenseName[2] = "Car Insurance";
		ExpenseName[3] = "Gas";
		ExpenseName[4] = "Food";
		ExpenseName[5] = "Phone";
		ExpenseName[6] = "Cable";
		ExpenseName[7] = "Water";
		ExpenseName[8] = "Power";
		ExpenseName[9] = "Utilites";
		ExpenseName[10] = "Credit Card 1";
		ExpenseName[11] = "Credit Card 2";
		ExpenseName[12] = "Credit Card 3";
		ExpenseName[13] = "Other";
		ExpenseName[14] = "Misc";
	}
}

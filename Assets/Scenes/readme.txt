TOUCHSCRIPT INSTRUCTIONS

- Create scene
1) Create an empty GameObject and add MouseInput and MobileInput
2) Add TouchDebugger prefab from touchscript folder
3) Add Camera Layer 2D to the MainCamera

- Create a button
1) Add TapGesture to the button
2) Add Collider to the button
3) Add following lines to the script


	using System;
	using TouchScript.Gestures;
	
	private void OnEnable()
	{
		"gameObject".GetComponent<TapGesture>().Tapped += methodToCall;
		
	}
	
	private void OnDisable()
	{
		// don't forget to unsubscribe
		try{
		"gameObject".GetComponent<TapGesture>().Tapped -= methodToCall;
		}catch{}
	}

	private void methodToCall(object sender, EventArgs e)
	{
		//your method
	}
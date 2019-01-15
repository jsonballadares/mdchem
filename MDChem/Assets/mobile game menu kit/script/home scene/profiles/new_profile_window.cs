using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;


public class new_profile_window : MonoBehaviour {
    //public AuthManager am;
	public TMP_InputField my_input;
    public TMP_InputField password;
    public manage_menu_uGUI my_manage_menu_uGUI;
	game_master my_game_master;
	public profile_manager my_profile_manager;

	public GameObject only_ok_button;
	public GameObject ok_and_cancel_button;
	int profile_slot;

    // Use this for initialization
    public void My_start (int profile_target_slot, bool show_cancel_button) {
		if (game_master.game_master_obj)
			my_game_master = (game_master)game_master.game_master_obj.GetComponent("game_master");

		if (show_cancel_button)
			{
			only_ok_button.SetActive(false);
			ok_and_cancel_button.SetActive(true);
			}
		else
			{
			only_ok_button.SetActive(true);
			ok_and_cancel_button.SetActive(false);
			}

		profile_slot = profile_target_slot;
		this.gameObject.SetActive(true);

		Focus();

	}


	public void Focus()
		{
		my_input.ActivateInputField();
		my_input.Select();
		}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
			Ok_button();
	}

    string uuid = "";
    public async void CreateAccount()
    {
        await CreateUserEmailAsync();
        
        Debug.Log("uuid " + uuid);
        PlayerPrefs.SetString("ui", uuid);
    }

    public Task CreateUserEmailAsync()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        return auth.CreateUserWithEmailAndPasswordAsync(my_input.text, password.text)
            .ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    var user = task.Result;
                    uuid = user.UserId;
                }
            });
    }

    
    public void Ok_button()
	{
        Debug.Log("TESTINGFUN");
        if (my_input.text != "")
			{
             CreateAccount();
            int old_profile = my_game_master.current_profile_selected;
			my_game_master.current_profile_selected = profile_slot;
            
            if (my_manage_menu_uGUI.current_screen == my_manage_menu_uGUI.profile_screen)//update profile screen
				{
				my_profile_manager.Select_this_profile(old_profile);
				my_profile_manager.Select_this_profile(profile_slot);
				}

			my_game_master.Gui_sfx(my_game_master.tap_sfx);
			my_game_master.Create_new_profile(my_input.text);
			my_input.text = "";
			my_manage_menu_uGUI.Update_profile_name(true);
			this.gameObject.SetActive(false);
			my_profile_manager.Update_this_slot(my_game_master.current_profile_selected);
			}
		else
			my_game_master.Gui_sfx(my_game_master.tap_error_sfx);

	}

	public void Cancel()
	{
		my_game_master.Gui_sfx(my_game_master.tap_sfx);
		this.gameObject.SetActive(false);
	}
}

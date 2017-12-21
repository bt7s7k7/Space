using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreator : MonoBehaviour {
	
	public string namePref = "playerName";
	public string bgColorPref = "playerBGColor";
	public string iconColorPref = "playerIconColor";
	public string iconTypePref = "playerIconType";
	[Space]
	public string defName = "Player McPlayerface";
	public int defBGColor = 14;
	public int defIconColor = 0;
	public int defIconType = 0;
	[Space]
	public Slider bgSlider;
	public Slider iconColorSlider;
	public Slider iconTypeSlider;
	public InputField nameField;
	[Space]
	public Image background;
	public Image icon;
	
	void Reset() {
		
	}
	
	void Awake() {
		
	}
	
	void Start() {
		iconColorSlider.maxValue = bgSlider.maxValue = GameManager.instance.colorPallete.Length - 1;
		bgSlider.maxValue = bgSlider.maxValue = GameManager.instance.colorPallete.Length - 2;
		iconTypeSlider.maxValue = GameManager.instance.playerIcons.Length - 1;
		bgSlider.value = PlayerPrefs.GetInt(bgColorPref,defBGColor) - 1;
		iconColorSlider.value = PlayerPrefs.GetInt(iconColorPref,defIconColor);
		iconTypeSlider.value = PlayerPrefs.GetInt(iconTypePref,defIconType);
		nameField.text = PlayerPrefs.GetString(namePref,defName);
	}
	
	void Update() {
		PlayerPrefs.SetInt(bgColorPref,(int)bgSlider.value + 1);
		PlayerPrefs.SetInt(iconColorPref,(int)iconColorSlider.value);
		PlayerPrefs.SetInt(iconTypePref,(int)iconTypeSlider.value);
		PlayerPrefs.SetString(namePref,nameField.text);
		background.color = GameManager.instance.colorPallete[PlayerPrefs.GetInt(bgColorPref,defBGColor)];
		icon.color = GameManager.instance.colorPallete[PlayerPrefs.GetInt(iconColorPref,defIconColor)];
		icon.sprite = GameManager.instance.playerIcons[PlayerPrefs.GetInt(iconTypePref,defIconType)];
	}
	
	void FixedUpdate() {
		
	}
	
}

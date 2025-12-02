using System;
using UnityEngine;

[Serializable]
public class SettingsData
{
	public bool soundEnabled = true;
	public bool musicEnabled = true;
	public Language language = Language.Russian;
	public bool thumbModeEnabled = false;
	public bool dontSleepDuringGame = true;
	public bool showReligiousIcons = true;

	public enum Language
	{
		Russian,
		English
	}
}
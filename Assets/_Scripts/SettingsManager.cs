using UnityEngine;
using System.IO;

public class SettingsManager : MonoBehaviour
{
	public static SettingsManager Instance { get; private set; }

	public SettingsData CurrentSettings { get; private set; }

	private string savePath;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);

		savePath = Path.Combine(Application.persistentDataPath, "settings.json");
		LoadSettings();
	}

	public void LoadSettings()
	{
		if (File.Exists(savePath))
		{
			string json = File.ReadAllText(savePath);
			CurrentSettings = JsonUtility.FromJson<SettingsData>(json);
		}
		else
		{
			CurrentSettings = new SettingsData();
			SaveSettings();
		}
	}

	public void SaveSettings()
	{
		string json = JsonUtility.ToJson(CurrentSettings, true);
		File.WriteAllText(savePath, json);
	}

	public void UpdateSettings(SettingsData newSettings)
	{
		CurrentSettings = newSettings;
		SaveSettings();

		// Применяем настройки
		ApplySettings();
	}

	private void ApplySettings()
	{
		// Применяем настройки звука
		AudioListener.volume = CurrentSettings.soundEnabled ? 1 : 0;

		// Применяем настройку сна устройства
		Screen.sleepTimeout = CurrentSettings.dontSleepDuringGame ?
				SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;

		// Здесь можно добавить логику для смены языка и других настроек
		// Для смены языка потребуется система локализации
	}
}
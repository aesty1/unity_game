using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsMenuUI : MonoBehaviour
{
	[Header("UI Elements")]
	[SerializeField] private GameObject settingsPanel;

	[Header("Toggle Buttons")]
	[SerializeField] private Toggle soundToggle;
	[SerializeField] private Toggle musicToggle;
	[SerializeField] private Toggle thumbModeToggle;
	[SerializeField] private Toggle dontSleepToggle;
	[SerializeField] private Toggle religiousIconsToggle;

	[Header("Language")]
	[SerializeField] private TMP_Dropdown languageDropdown;
	[SerializeField] private List<string> languageOptions = new List<string> { "Русский", "English" };

	[Header("About Section")]
	[SerializeField] private GameObject aboutPanel;
	[SerializeField] private TextMeshProUGUI aboutText;

	[Header("Navigation")]
	[SerializeField] private Button backButton;
	[SerializeField] private Button applyButton;
	[SerializeField] private Button aboutButton;
	[SerializeField] private Button closeAboutButton;

	private SettingsData tempSettings;

	private void Start()
	{
		InitializeUI();
		SetupEventListeners();
		LoadCurrentSettings();
	}

	private void InitializeUI()
	{
		settingsPanel.SetActive(false);
		aboutPanel.SetActive(false);

		// Настраиваем dropdown для языка
		languageDropdown.ClearOptions();
		languageDropdown.AddOptions(languageOptions);
	}

	private void SetupEventListeners()
	{
		// Кнопки
		backButton.onClick.AddListener(OnBackClicked);
		applyButton.onClick.AddListener(OnApplyClicked);
		aboutButton.onClick.AddListener(OnAboutClicked);
		closeAboutButton.onClick.AddListener(() => aboutPanel.SetActive(false));

		// Toggles
		soundToggle.onValueChanged.AddListener((value) => tempSettings.soundEnabled = value);
		musicToggle.onValueChanged.AddListener((value) => tempSettings.musicEnabled = value);
		thumbModeToggle.onValueChanged.AddListener((value) => tempSettings.thumbModeEnabled = value);
		dontSleepToggle.onValueChanged.AddListener((value) => tempSettings.dontSleepDuringGame = value);
		religiousIconsToggle.onValueChanged.AddListener((value) => tempSettings.showReligiousIcons = value);

		// Dropdown
		languageDropdown.onValueChanged.AddListener((index) =>
		{
			tempSettings.language = (SettingsData.Language)index;
		});
	}

	public void ShowSettings()
	{
		settingsPanel.SetActive(true);
		LoadCurrentSettings();
	}

	public void HideSettings()
	{
		settingsPanel.SetActive(false);
	}

	private void LoadCurrentSettings()
	{
		if (SettingsManager.Instance == null)
		{
			Debug.LogError("SettingsManager not found!");
			return;
		}

		tempSettings = new SettingsData
		{
			soundEnabled = SettingsManager.Instance.CurrentSettings.soundEnabled,
			musicEnabled = SettingsManager.Instance.CurrentSettings.musicEnabled,
			language = SettingsManager.Instance.CurrentSettings.language,
			thumbModeEnabled = SettingsManager.Instance.CurrentSettings.thumbModeEnabled,
			dontSleepDuringGame = SettingsManager.Instance.CurrentSettings.dontSleepDuringGame,
			showReligiousIcons = SettingsManager.Instance.CurrentSettings.showReligiousIcons
		};

		// Обновляем UI
		soundToggle.isOn = tempSettings.soundEnabled;
		musicToggle.isOn = tempSettings.musicEnabled;
		thumbModeToggle.isOn = tempSettings.thumbModeEnabled;
		dontSleepToggle.isOn = tempSettings.dontSleepDuringGame;
		religiousIconsToggle.isOn = tempSettings.showReligiousIcons;
		languageDropdown.value = (int)tempSettings.language;
	}

	private void OnBackClicked()
	{
		HideSettings();
		// Возвращаемся в главное меню
		// Здесь можно добавить вызов метода возврата в главное меню
	}

	private void OnApplyClicked()
	{
		SettingsManager.Instance.UpdateSettings(tempSettings);
		// Можно добавить визуальную обратную связь
		ShowNotification("Настройки сохранены!");
	}

	private void OnAboutClicked()
	{
		UpdateAboutText();
		aboutPanel.SetActive(true);
	}

	private void UpdateAboutText()
	{
		string text = tempSettings.language == SettingsData.Language.Russian ?
				"<size=24><b>Об игре</b></size>\n\n" +
				"Игра с гексагональной картой, случайно генерируемой каждый раз.\n\n" +
				"<b>Версия:</b> 1.0.0\n" +
				"<b>Разработчик:</b> [TR14WR]\n\n" +
				"Спасибо за игру!" :

				"<size=24><b>About Game</b></size>\n\n" +
				"Game with hexagonal map, randomly generated each time.\n\n" +
				"<b>Version:</b> 1.0.0\n" +
				"<b>Developer:</b> [TR14WR]\n\n" +
				"Thank you for playing!";

		aboutText.text = text;
	}

	private void ShowNotification(string message)
	{
		// Простая реализация уведомления
		// Можно заменить на более продвинутую систему
		Debug.Log(message);
		// Или создать префаб для уведомлений
	}
}
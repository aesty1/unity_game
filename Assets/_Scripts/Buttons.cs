using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class Buttons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
  private Text _text;
  private Color _defaultColor;
  public Color pressedColor = Color.white;
  [ColorUsage(true, true)]
  public Color defaultColor = new Color(0.631f, 0.047f, 0.047f);

  [Header("Scene Indices (from Build Settings)")]
  public int gameSceneIndex = 0;
  public int startGameSceneIndex = 1;
  public int settingsSceneIndex = 2;
  public int achievementsSceneIndex = 3;
  public int laborotoryLevelSceneIndex = 4;

  [Header("Sound Settings")]
  public AudioClip clickSound; // Звук при нажатии
  public AudioClip hoverSound; // Звук при наведении (опционально)
  [Range(0, 1)]
  public float volume = 0.5f;

  private AudioSource audioSource;

  void Start()
  {
    _text = GetComponent<Text>();
    if (_text == null)
    {
      Debug.LogError("Этот скрипт должен быть прикреплен к Text элементу!");
      enabled = false;
      return;
    }
    _defaultColor = defaultColor;
    _text.color = _defaultColor;

    // Добавляем AudioSource, если его нет
    audioSource = gameObject.GetComponent<AudioSource>();
    if (audioSource == null)
    {
      audioSource = gameObject.AddComponent<AudioSource>();
    }
    audioSource.playOnAwake = false;
    audioSource.volume = volume;
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    _text.color = pressedColor;
    PlaySound(clickSound);
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    _text.color = _defaultColor;
  }

  // Опционально: звук при наведении
  public void OnPointerEnter(PointerEventData eventData)
  {
    if (hoverSound != null)
    {
      PlaySound(hoverSound);
    }
  }

  private void PlaySound(AudioClip clip)
  {
    if (clip != null && audioSource != null)
    {
      audioSource.clip = clip;
      audioSource.Play();
    }
  }

  public void SetDefaultColor(Color color)
  {
    _defaultColor = color;
    _text.color = _defaultColor;
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (string.Equals(gameObject.name, "StartGame", StringComparison.OrdinalIgnoreCase))
    {
      LoadSceneAsync(startGameSceneIndex);
    }
    else if (string.Equals(gameObject.name, "Settings", StringComparison.OrdinalIgnoreCase))
    {
      LoadSceneAsync(settingsSceneIndex);
    }
    else if (string.Equals(gameObject.name, "Achievements", StringComparison.OrdinalIgnoreCase))
    {
      Application.OpenURL("https://google.com");
    }
    else if (string.Equals(gameObject.name, "Go", StringComparison.OrdinalIgnoreCase))
    {
      LoadSceneAsync(laborotoryLevelSceneIndex);
    }
    else if (string.Equals(gameObject.name, "Training", StringComparison.OrdinalIgnoreCase))
    {
      LoadSceneAsync(laborotoryLevelSceneIndex);
    }
    else if (string.Equals(gameObject.name, "Back", StringComparison.OrdinalIgnoreCase))
    {
      LoadSceneAsync(gameSceneIndex);
    }
    else if (string.Equals(gameObject.name, "Quit", StringComparison.OrdinalIgnoreCase))
    {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
  }

  private void LoadSceneAsync(int sceneIndex)
  {
    if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
    {
      SceneManager.LoadSceneAsync(sceneIndex);
    }
    else
    {
      Debug.LogError("Неверный индекс сцены: " + sceneIndex);
    }
  }
}
using System;
using UnityEngine;
using UnityEngine.UI;

namespace OOP.UI
{
   public class MainMenuScreen : MonoBehaviour
   {
      [SerializeField] private Button _startGameButton;
      
      public event Action OnPlayButtonClicked;

      public void Awake()
      {
         _startGameButton.onClick.AddListener(PlayButtonClick);
      }

      private void PlayButtonClick()
      {
         OnPlayButtonClicked?.Invoke();
      }
   }
}

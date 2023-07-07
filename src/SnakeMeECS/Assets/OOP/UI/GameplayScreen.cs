using TMPro;
using UnityEngine;

namespace OOP.UI
{
    public class GameplayScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _applesTextField;
        [SerializeField] private TextMeshProUGUI _lenghtTextField;

        public void UpdateApplesText(long applesCount)
        {
            _applesTextField.text = applesCount.ToString();
        }
        
        public void UpdateLenghtText(long lenght)
        {
            _lenghtTextField.text = lenght.ToString();
        }
    }
}

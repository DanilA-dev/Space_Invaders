using UnityEngine;

namespace UI
{
    public class UIHealthItem : MonoBehaviour
    {
        public bool IsHidden { get; private set; }
        
        public void Hide()
        {
            IsHidden = true;
            gameObject.SetActive(false);
        }

        public void Show()
        {
            IsHidden = false;
            gameObject.SetActive(true);
        }
    }
}
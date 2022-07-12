using UnityEngine;
using UnityEngine.UI;

namespace AvatarSDKExample
{
    public class UIProgressBar : MonoBehaviour
    {
        public Image progressImage;
        public Text progressText;

        void Start()
        {
            try
            {
                progressText.text = "0%"; // Throws a weird null ref exception despite actually working
                progressImage.fillAmount = 0;
            }
            catch { }
        }

        public void SetPercent(float percent)
        {
            progressImage.fillAmount = percent;
            progressText.text = percent * 100 + "%";
        }

        public void ResetValue()
        {
            SetPercent(0);
        }
    }
}
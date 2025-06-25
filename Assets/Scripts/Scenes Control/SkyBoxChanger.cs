using DG.Tweening;
using UnityEngine;

namespace ScenesControl
{
    public class SkyBoxChanger : MonoBehaviour
    {
        [SerializeField] private float _fogHueOffset = 0.33f;   
        [SerializeField] private float _fogSaturationMul = 0.8f; 
        [SerializeField] private float _fogValueMul = 0.8f;

        [SerializeField] private float _duration = 60f;
        [SerializeField] private float _saturation = 0.4f;  
        [SerializeField] private float _value = 0.5f;       

        private float _currentHue = 0f;
        private Material _skyboxMat;

        void Start()
        {
            _skyboxMat = RenderSettings.skybox;

            if (_skyboxMat == null || !_skyboxMat.HasProperty("_Tint"))
            {
                Debug.LogWarning("Skybox material missing or _Tint property not found.");
                return;
            }

            Color startColor = Color.gray;
            _skyboxMat.SetColor("_Tint", startColor);
            RenderSettings.fogColor = startColor;   

            if (!RenderSettings.fog) RenderSettings.fog = true; 

            StartCycle();
        }

        private void StartCycle()
        {
            DOTween.To(() => _currentHue, x =>
            {
                _currentHue = x;

                Color skyColor = Color.HSVToRGB(_currentHue % 1f, _saturation, _value);
                _skyboxMat.SetColor("_Tint", skyColor);

                float fogHue = (_currentHue + _fogHueOffset) % 1f;
                Color fogColor = Color.HSVToRGB(fogHue,
                                                _saturation * _fogSaturationMul,
                                                _value * _fogValueMul);

                RenderSettings.fogColor = fogColor;

            }, 1f, _duration)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
        }
    }
}

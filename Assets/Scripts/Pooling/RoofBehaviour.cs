#region

using UnityEngine;

#endregion

namespace Assets.Scripts.Pooling
{
    public class RoofBehaviour : BlockBehaviour
    {
        public Light PointLight;

        public void Awake()
        {
            //PointLight.gameObject.SetActive(Random.value > 0.25);
            PointLight.color = EnvironmentManager.Ctrl.LightColor;
            PointLight.intensity = Random.value*5;
        }

        public void Update()
        {
            PointLight.color = EnvironmentManager.Ctrl.LightColor;
            PointLight.intensity = EnvironmentManager.Ctrl.LightIntensity;
            PointLight.range = EnvironmentManager.Ctrl.LightRange;
        }
    }
}
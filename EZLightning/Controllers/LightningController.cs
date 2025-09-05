using DigitalRuby.ThunderAndLightning;
using UnityEngine;

namespace EZLightning.Controllers
{
    // Credit to Zagster, I updated the code to work with the latest version of C# and made some optimizations.
    public class LightningController
    {
        public static StormyWeather? stormyWeather = null;
        public static void SpawnLightningBolt(Vector3 strikeOrigin, Vector3 strikeDestination, float strikeVolume, float glowWidthMultiplier, float glowIntensity, int maxLights, float intensity, float minTrunkWidth, float maxTrunkWidth, int minCount, int maxCount)
        {
            // Set up AudioSource
            AudioSource thunderOrigin = GameObject.Find("LightningGrenade_ThunderAudioSource")?.GetComponent<AudioSource>()!;
            if (thunderOrigin == null)
            {
                GameObject audioObject = new GameObject("LightningGrenade_ThunderAudioSource");
                audioObject.SetActive(true);
                UnityEngine.Object.DontDestroyOnLoad(audioObject); // persist across scenes
                thunderOrigin = audioObject.AddComponent<AudioSource>();
                thunderOrigin.spatialBlend = 0f; // 2D sound
                thunderOrigin.minDistance = 5f;
                thunderOrigin.maxDistance = 200f;
                thunderOrigin.rolloffMode = AudioRolloffMode.Logarithmic;
                thunderOrigin.playOnAwake = false;
                thunderOrigin.loop = false;
                thunderOrigin.enabled = true;
            }

            if (stormyWeather == null)
            {
                stormyWeather = UnityEngine.Object.FindObjectOfType<StormyWeather>(true);
            }

            // Plugin.ExtendedLogging($"{vector} -> {strikePosition}");

            LightningBoltPrefabScript localLightningBoltPrefabScript = UnityEngine.Object.Instantiate(stormyWeather.targetedThunder);
            localLightningBoltPrefabScript.enabled = true;

            localLightningBoltPrefabScript.GlowWidthMultiplier = glowWidthMultiplier;
            localLightningBoltPrefabScript.DurationRange = new RangeOfFloats { Minimum = 0.6f, Maximum = 1.2f };
            localLightningBoltPrefabScript.TrunkWidthRange = new RangeOfFloats { Minimum = minTrunkWidth, Maximum = maxTrunkWidth };
            localLightningBoltPrefabScript.Camera = GameNetworkManager.Instance.localPlayerController.gameplayCamera;
            localLightningBoltPrefabScript.Source.transform.position = strikeOrigin;
            localLightningBoltPrefabScript.Destination.transform.position = strikeDestination;
            localLightningBoltPrefabScript.AutomaticModeSeconds = 0.2f;
            localLightningBoltPrefabScript.Generations = 8;
            if ((minCount >= 0) && (maxCount >= 0))
            {
                localLightningBoltPrefabScript.CountRange = new RangeOfIntegers { Minimum = minCount, Maximum = maxCount };
            }
            if (intensity >= 0)
            {
                localLightningBoltPrefabScript.Intensity = intensity;
            }
            if (glowIntensity >= 0)
            {
                localLightningBoltPrefabScript.GlowIntensity = glowIntensity;
            }
            if (maxLights >= 0)
            {
                localLightningBoltPrefabScript.MaximumLightsPerBatch = maxLights;
            }
            localLightningBoltPrefabScript.CreateLightningBoltsNow();

            thunderOrigin.transform.position = strikeDestination;
            thunderOrigin.volume = strikeVolume;
            stormyWeather.PlayThunderEffects(strikeDestination, thunderOrigin);
        }
    }
}
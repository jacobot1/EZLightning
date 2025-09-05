using UnityEngine;
using EZLightning.Data;

namespace EZLightning
{
    /// <summary>
    /// Public API for other mods to call to create networked lightning strikes.
    /// </summary>
    public static class API
    {
        /// <summary>
        /// Creates a networked lightning bolt between a specified origin and destination.
        /// </summary>
        /// <param name="origin">The starting point of the lightning bolt.</param>
        /// <param name="destination">The end point of the lightning bolt.</param>
        /// <param name="volume">The volume of the thunder sound effect (0.0 to 1.0).</param>
        /// <param name="glowWidthMultiplier">The glowiness of the bolt (default 2.5f)</param>
        /// <param name="glowIntensity">The intensity of the bolt glow</param>
        /// <param name="maxLights">The allowed number of lights in each bolt batch</param>
        /// <param name="intensity">The bolt intensity</param>
        /// <param name="minTrunkWidth">The minimum width of the bolt (default 0.6f)</param>
        /// <param name="maxTrunkWidth">The maximum width of the bolt (default 1.2f)</param>
        /// <param name="minCount">The minimum number of bolts per batch</param>
        /// <param name="maxCount">The maximum number of bolts per batch</param>
        public static void Strike(Vector3 origin, Vector3 destination, float volume = 1f, float glowWidthMultiplier = 2.5f, float glowIntensity = -1f, int maxLights = -1, float intensity = -1f, float minTrunkWidth = 0.6f, float maxTrunkWidth = 1.2f, int minCount = -1, int maxCount = -1)
        {
            var data = new StrikeData
            {
                origin = new SVector3(origin),
                destination = new SVector3(destination),
                volume = Mathf.Clamp01(volume),
                glowWidthMultiplier = glowWidthMultiplier,
                glowIntensity = glowIntensity,
                maxLights = maxLights,
                intensity = intensity,
                minTrunkWidth = minTrunkWidth,
                maxTrunkWidth = maxTrunkWidth,
                minCount = minCount,
                maxCount = maxCount
            };
            EZLightning.StrikeMessage.SendServer(data);
        }
    }
}

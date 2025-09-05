using System;
using UnityEngine;

namespace EZLightning.Data
{
    /// <summary>
    /// A serializable representation of Unity's Vector3 for network transmission.
    /// </summary>
    [Serializable]
    public struct SVector3
    {
        public float x, y, z;

        public SVector3(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }

    /// <summary>
    /// Data payload for a custom lightning strike between two points.
    /// </summary>
    [Serializable]
    public struct StrikeData
    {
        public SVector3 origin;
        public SVector3 destination;
        public float volume;
        public float glowWidthMultiplier;
        public float glowIntensity;
        public int maxLights;
        public float intensity;
        public float minTrunkWidth;
        public float maxTrunkWidth;
        public int minCount;
        public int maxCount;
    }
}

using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalNetworkAPI;
using EZLightning.Data;
using EZLightning.Controllers;

namespace EZLightning
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency("LethalNetworkAPI")]
    public class EZLightning : BaseUnityPlugin
    {
        public const string GUID = "com.jacobot5.EZLightning";
        public const string NAME = "EZLightning";
        public const string VERSION = "1.0.0";

        private readonly Harmony harmony = new Harmony(GUID);
        internal static ManualLogSource Log;

        // Network messages for syncing lightning effects
        internal static LNetworkMessage<StrikeData> StrikeMessage { get; private set; }

        private void Awake()
        {
            Log = BepInEx.Logging.Logger.CreateLogSource(GUID);
            Log.LogInfo($"{NAME} has awoken.");


            // --- Networking Setup ---
            // Message for custom origin-to-destination lightning
            StrikeMessage = LNetworkMessage<StrikeData>.Connect("EZLightning_Strike");
            StrikeMessage.OnServerReceived += OnStrikeServer;
            StrikeMessage.OnClientReceived += OnStrikeClient;
        }

        // --- Network Handlers ---

        // When the server receives a request for a custom strike, it broadcasts it to all clients
        private void OnStrikeServer(StrikeData data, ulong sender)
        {
            StrikeMessage.SendClients(data);
        }

        // When a client receives the broadcast, it spawns the custom lightning effect
        private void OnStrikeClient(StrikeData data)
        {
            LightningController.SpawnLightningBolt(
                data.origin.ToVector3(),
                data.destination.ToVector3(),
                data.volume,
                data.glowWidthMultiplier,
                data.glowIntensity,
                data.maxLights,
                data.intensity,
                data.minTrunkWidth,
                data.maxTrunkWidth,
                data.minCount,
                data.maxCount
            );
        }
    }
}


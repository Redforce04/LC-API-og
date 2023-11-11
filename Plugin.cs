// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Lethal Company Modding Community">
// Copyright (c) Lethal Company Modding Community. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LC_API;

// ReSharper disable RedundantNameQualifier
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using LC_API.Comp;
using LC_API.ServerAPI;
using UnityEngine;

/// <inheritdoc />
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    /// <summary>
    /// Gets the <see cref="ManualLogSource"/> file for the plugin.
    /// </summary>
#pragma warning disable SA1401
    public static ManualLogSource Log;
#pragma warning restore SA1401

    // ReSharper disable once InconsistentNaming
    private ConfigEntry<bool> configOverrideModServer;

    /// <summary>
    /// Called when the plugin awakes.
    /// </summary>
    private void Awake()
    {
        this.configOverrideModServer = this.Config.Bind(
            "General",
            "Force modded server browser",
            false,
            "Should the API force you into the modded server browser?");

        Log = this.Logger;

        // Plugin startup logic
        this.Logger.LogInfo($"LC-API Starting up..");
        if (this.configOverrideModServer.Value)
        {
            ModdedServer.SetServerModdedOnly();
        }

        // ReSharper disable StringLiteralTypo
        Harmony harmony = new Harmony("ModAPI");
        harmony.PatchAll();
    }

    private void OnDestroy()
    {
        BundleAPI.BundleLoader.Load();
        GameObject apiGameObject = new GameObject("API");
        DontDestroyOnLoad(apiGameObject);
        apiGameObject.AddComponent<SVAPI>();
    }
}
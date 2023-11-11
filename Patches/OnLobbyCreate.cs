// -----------------------------------------------------------------------
// <copyright file="OnLobbyCreate.cs" company="Lethal Company Modding Community">
// Copyright (c) Lethal Company Modding Community. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LC_API.Patches;

using HarmonyLib;
using Steamworks;
using Steamworks.Data;
using UnityEngine;

/// <summary>
/// Contains the patch for <see cref="GameNetworkManager.SteamMatchmaking_OnLobbyCreated"/>.
/// </summary>
// ReSharper disable InconsistentNaming
[HarmonyPatch(typeof(GameNetworkManager), "SteamMatchmaking_OnLobbyCreated")]
internal static class OnLobbyCreate
{
    [HarmonyPrefix]
    private static bool Prefix(GameNetworkManager __instance, Result result, Lobby lobby)
    {
        if (result != Result.OK)
        {
            Debug.LogError($"Lobby could not be created! {result}", __instance);
        }

        __instance.lobbyHostSettings.lobbyName = "[MODDED]" + __instance.lobbyHostSettings.lobbyName;

        // lobby.SetData("vers", "This server requires mods. " + GameNetworkManager.Instance.gameVersionNum.ToString());
        Plugin.Log.LogMessage("server pre-setup success");
        return true;
    }
}
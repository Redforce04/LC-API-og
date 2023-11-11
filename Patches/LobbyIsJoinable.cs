// -----------------------------------------------------------------------
// <copyright file="LobbyIsJoinable.cs" company="Lethal Company Modding Community">
// Copyright (c) Lethal Company Modding Community. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LC_API.Patches;

using HarmonyLib;

/// <summary>
/// Contains the patch for <see cref="GameNetworkManager.LobbyDataIsJoinable"/>.
/// </summary>
[HarmonyPatch(typeof(GameNetworkManager), "LobbyDataIsJoinable")]
internal static class LobbyIsJoinable
{
    [HarmonyPrefix]
    private static bool Prefix()
    {
        return true;
    }
}
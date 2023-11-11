// -----------------------------------------------------------------------
// <copyright file="Vers.cs" company="Lethal Company Modding Community">
// Copyright (c) Lethal Company Modding Community. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LC_API.Patches;

using Comp;
using HarmonyLib;

/// <summary>
/// Contains the patch for <see cref="MenuManager.Awake"/>.
/// </summary>
// ReSharper disable InconsistentNaming
[HarmonyPatch(typeof(MenuManager), "Awake")]
internal static class Vers
{
    [HarmonyPrefix]
    private static bool Prefix(MenuManager __instance)
    {
        SVAPI.MenuManager = __instance;
        return true;
    }
}
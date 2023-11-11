// -----------------------------------------------------------------------
// <copyright file="ChatInterpreter.cs" company="Lethal Company Modding Community">
// Copyright (c) Lethal Company Modding Community. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LC_API.Patches;

using System;

using Data;
using HarmonyLib;
using ServerAPI;
using UnityEngine;

/// <summary>
/// Contains the patch for <see cref="HUDManager.AddChatMessage"/>.
/// </summary>
[HarmonyPatch(typeof(HUDManager), "AddChatMessage")]
internal static class ChatInterpreter
{
    [HarmonyPrefix]

    // ReSharper disable once InconsistentNaming
    private static bool Prefix(HUDManager __instance, string chatMessage, string nameOfUserWhoTyped)
    {
        if (!(chatMessage.Contains("NWE") & chatMessage.Contains("<size=0>")))
        {
            return true;
        }

        string[] dataFragments = chatMessage.Split('/');

        // Make sure there are at least two '/' characters
        if (dataFragments.Length < 3)
        {
            Plugin.Log.LogError(
                "Generic Network receive fail. This is a failure of the API, and it should be reported as a bug.");
        }
        else
        {
            Enum.TryParse(dataFragments[2], out NetworkBroadcastDataType type);

            switch (type)
            {
                case NetworkBroadcastDataType.BDstring:
                    Networking.GetString(dataFragments[0], dataFragments[1]);
                    break;

                case NetworkBroadcastDataType.BDint:
                    Networking.GetInt(int.Parse(dataFragments[0]), dataFragments[1]);
                    break;

                case NetworkBroadcastDataType.BDfloat:
                    Networking.GetFloat(float.Parse(dataFragments[0]), dataFragments[1]);
                    break;

                case NetworkBroadcastDataType.BDvector3:
                    string[] components = dataFragments[0].Replace("(", string.Empty).Replace(")", string.Empty)
                        .Split(',');
                    Vector3 convertedString = default(Vector3);
                    if (components.Length == 3)
                    {
                        if (float.TryParse(components[0], out float x) && float.TryParse(components[1], out float y) &&
                            float.TryParse(components[2], out float z))
                        {
                            convertedString.x = x;
                            convertedString.y = y;
                            convertedString.z = z;
                        }
                        else
                        {
                            Plugin.Log.LogError(
                                "Vector3 Network receive fail. This is a failure of the API, and it should be reported as a bug.");
                        }
                    }
                    else
                    {
                        Plugin.Log.LogError(
                            "Vector3 Network receive fail. This is a failure of the API, and it should be reported as a bug.");
                    }

                    Networking.GetVector3(convertedString, dataFragments[1]);
                    break;
            }
        }

        return true;
    }
}
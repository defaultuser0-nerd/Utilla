﻿using GorillaGameModes;
using System;
using System.Linq;

namespace Utilla.Models
{
    /// <summary>
    /// The base gamemode for a gamemode to inherit.
    /// </summary>
    /// <remarks>
    /// None should not be used from an external program.
    /// </remarks>
    public enum BaseGamemode
    {
        /// <summary>
        /// There is no gamemode manager to rely on, this should only be used by the Utilla mod when preparing modded gamemodes or gamemodes using a unique gamemode manager.
        /// </summary>
        None,
        /// <summary>
        /// Infection gamemode, requires at least four participating players for infection and under for tag.
        /// </summary>
        Infection,
        /// <summary>
        /// Casual gamemode, no players are affected by the gamemode, such as tagging or infecting.
        /// </summary>
        Casual,
        /// <summary>
        /// Hunt gamemode, requires at least four participating players.
        /// </summary>
        Hunt,
        /// <summary>
        /// Paintbrawl gamemode, requires at least two participating players.
        /// </summary>
        Paintbrawl
    }

    public class Gamemode 
	{
		/// <summary>
		/// The title of the Gamemode visible through the gamemode selector
		/// </summary>
		public string DisplayName { get; }

		/// <summary>
		/// The internal ID of the Gamemode
		/// </summary>
		public string ID { get; }

		/// <summary>
		/// The GamemodeString used in the CustomProperties of the Room
		/// </summary>
		public string GamemodeString { get; }

		/// <summary>
		/// The BaseGamemode being inherited
		/// </summary>
		public string BaseGamemode { get; }
		public Type GameManager { get; }

        public Gamemode(string id, string displayName, string baseGameMode)
		{
            ID = id;
            DisplayName = displayName;
            BaseGamemode = baseGameMode;
			GamemodeString = Constants.GamemodePrefix + BaseGamemode.ToUpper();
        }

		public Gamemode(string id, string displayName, Type gameManager)
		{
			ID = id;
			DisplayName = displayName;
			GameManager = gameManager;
			GamemodeString = Constants.GamemodePrefix + ID;
		}

        /// <remarks>This should only be used interally to create base game gamemodes</remarks>
        internal Gamemode(string id, string displayName)
        {
            ID = id;
            DisplayName = displayName;
			BaseGamemode = displayName.ToUpper();

            GamemodeString = ID;
        }

        public static implicit operator ModeSelectButtonInfoData(Gamemode gamemode)
		{
			return new ModeSelectButtonInfoData()
			{
				Mode = gamemode.ID,
				ModeTitle = gamemode.DisplayName,
				NewMode = false,
				CountdownTo = null
			};
		}

		public static implicit operator Gamemode(ModeSelectButtonInfoData modeSelectButtonInfo)
		{
			return new Gamemode(modeSelectButtonInfo.Mode, modeSelectButtonInfo.ModeTitle, Enum.GetNames(typeof(GameModeType)).First(gamemode => gamemode.ToUpper() == modeSelectButtonInfo.Mode).ToString());
		}
    }
}

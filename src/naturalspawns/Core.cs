using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;

namespace naturalspawns
{
	public class NaturalSpawnsCore : ModSystem
	{

		public override void Start(ICoreAPI api)
		{
			base.Start(api);

			api.Logger.Notification("Loading NaturalSpawns... ");

			var harmony = new Harmony("zach2039.naturalspawns");
			harmony.PatchAll(Assembly.GetExecutingAssembly());

			api.Logger.Notification("... Done!");
		}
	}

	[HarmonyPatch(typeof(Block), "CanCreatureSpawnOn")]
	class BlockCanCreatureSpawnOnPatch
	{
		static void Postfix(Block __instance, ref bool __result, IBlockAccessor blockAccessor, BlockPos pos, EntityProperties type, BaseSpawnConditions sc)
		{
			List<String> allowedBlocks = new List<String>
			{
				// Sand
				"sand",

				// Gravel
				"gravel",
				"dirtygravel",
				"sludgygravel",
				"muddygravel",

				// Dirt
				"soil",
				"forestfloor",
				"peat",

				// Rock
				"rock",
				"crackedrock",
				"regolith",
				"ore-gem",
				"ore-graded",
				"ore-ungraded"
			};

			if (!allowedBlocks.Contains(blockAccessor.GetBlock(pos).Code.FirstCodePart()))
			{
				__result = false;
			}
		}
	}
}

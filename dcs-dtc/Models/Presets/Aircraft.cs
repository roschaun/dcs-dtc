﻿using DTC.Models.Base;
using DTC.Models.F16;
using System;
using System.Collections.Generic;

namespace DTC.Models.Presets
{
	public class Aircraft
	{
		public string Name {
			get
			{
				if (Model == AircraftModel.F16C)
				{
					return "F-16C";
				}
				throw new Exception();
			}
		}

		public List<Preset> Presets { get; set; }
		public AircraftModel Model { get; set; }

		public Aircraft(AircraftModel model)
		{
			Presets = new List<Preset>();
			Model = model;
		}

		public string GetAircraftModelName()
		{
			return Enum.GetName(typeof(AircraftModel), Model);
		}

		public Type GetAircraftConfigurationType()
		{
			if (Model == AircraftModel.F16C)
			{
				return typeof(F16Configuration);
			}
			throw new Exception();
		}

		public Preset CreatePreset(string name, IConfiguration cfg = null)
		{
			if (Model == AircraftModel.F16C)
			{
				if (cfg == null)
				{
					cfg = new F16Configuration();
				}
				var p = new Preset(name, cfg);
				Presets.Add(p);
				return p;
			}
			else
			{
				throw new Exception();
			}
		}

		internal Preset ClonePreset(Preset preset)
		{
			var p = preset.Clone();
			Presets.Add(p);
			PresetsStore.PresetChanged(this, p);
			return p;
		}

		internal void DeletePreset(Preset preset)
		{
			Presets.Remove(preset);
			FileStorage.DeletePreset(this, preset);
		}
	}
}
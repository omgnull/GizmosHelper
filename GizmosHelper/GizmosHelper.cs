using System;
using System.Collections.Generic;
using UnityEngine;

namespace Omgnull
{
	public class GizmosHelper
	{
		#region public members
		// Name for label
		public string name;
		// Lines Material
		public Material material;
		// Default map
		public static Dictionary<string, string> map;
		#endregion

		#region lines defaults
		// Width at the first line position
		public float lineStartWidth = 0.02f;
		// Width at the last line position
		public float lineEndWidth = 0.005f;
		// Distance from start position
		public float lineDistance = 3;
		#endregion

		#region protected members
		// Helper unique id
		protected string id;
		// Helper is being drawn
		protected bool drawn;
		// Helper game object
		protected Transform parent;
		// Lines
		protected Dictionary<string, GizmosHelperLine> lines;
		#endregion

		public GizmosHelper(Transform parent, string name = "Untittled helper")
		{
			this.name = name;
			this.parent = parent;
			this.material = new Material(Shader.Find("Particles/Additive"));
			this.lines = new Dictionary<string,GizmosHelperLine>();
		}

		public bool InitilalizeLines(Dictionary<string, string> map)
		{
			foreach (var definition in map)
			{
				string className = definition.Key;
				Type classType = Type.GetType(definition.Value);

				if (classType != null && classType.IsSubclassOf(typeof(GizmosHelperLine)))
				{
					GizmosHelperLine line = (GizmosHelperLine)Activator.CreateInstance(classType, this.name, this.parent);

					line.startWidth = this.lineStartWidth;
					line.endWidth = this.lineEndWidth;
					line.distance = this.lineDistance;
					line.material = this.material;

					this.lines.Add(className, line);
				}
			}

			return lines.Count > 0;
		}

		public bool Draw()
		{
			if (lines.Count == 0)
			{
				return false;
			}

			this.DrawLines();

			return true;
		}

		public GizmosHelperLine GetLine(string key)
		{
			return this.lines.ContainsKey(key) ? 
				this.lines[key] : null;
		}

		public static Dictionary<string, string> GetDefaultLineClasses()
		{
			if (GizmosHelper.map == null)
			{
				GizmosHelper.map = new Dictionary<string, string>();

				GizmosHelper.map.Add("X", "Omgnull.GizmoXLine");
				GizmosHelper.map.Add("Y", "Omgnull.GizmoYLine");
				GizmosHelper.map.Add("Z", "Omgnull.GizmoZLine");
			}

			return GizmosHelper.map;
		}

		public bool Toggle()
		{
			
			
			return true;
		}

		public override string ToString()
		{
			return this.name;
		}

		protected void DrawLines()
		{
			foreach (var line in this.lines)
			{
				line.Value.Draw();
			}
		}
    }
}

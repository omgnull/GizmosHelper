using System;
using UnityEngine;

namespace Omgnull
{
	abstract public class GizmosHelperLine
	{
		// Line color
		public Color color = Color.black;
		// Line direction
		public float distance = 2;
		// Line direction
		public Vector3 direction = Vector3.right;
		// Width at the first line position
		public float startWidth = 0.02f;
		// Width at the last line position
		public float endWidth = 0.005f;
		// Line material
		public Material material;
		// Game object transform
		protected GameObject gameObject;
		// Line renderer
		private LineRenderer renderer;

		public GizmosHelperLine(string name, Transform parent)
		{
			this.gameObject = new GameObject();

			this.gameObject.name = name + " " +this.GetType().Name + " " + parent.gameObject.name;
			this.gameObject.transform.parent = parent;
			this.gameObject.transform.position = parent.position;
			this.gameObject.transform.rotation = parent.rotation;
		}

		public virtual void Draw()
		{
			if (this.gameObject.transform.parent == null)
			{
				return;
			}
			
			LineRenderer rdr = this.GetRenderer();

			rdr.SetColors(this.color, this.color);
			rdr.material = this.material;
			rdr.SetPosition(0, this.gameObject.transform.localPosition);
			rdr.SetPosition(1, this.gameObject.transform.localPosition + this.direction * this.distance);

			rdr.SetWidth(startWidth, endWidth);
		}

		protected LineRenderer GetRenderer()
		{
			if (this.renderer == null)
			{
				this.renderer = this.gameObject.AddComponent<LineRenderer>();
				this.renderer.castShadows = false;
				this.renderer.receiveShadows = false;
				this.renderer.useWorldSpace = false;
			}
			
			return this.renderer;
		}
	}

	public class GizmoXLine : GizmosHelperLine
	{
		public GizmoXLine(string name, Transform parent)
			: base(name, parent)
		{
			this.direction = Vector3.right;
			this.color = Color.red;
		}
	}

	public class GizmoYLine : GizmosHelperLine
	{
		public GizmoYLine(string name, Transform parent)
			: base(name, parent)
		{
			this.direction = Vector3.up;
			this.color = Color.green;
		}
	}

	public class GizmoZLine : GizmosHelperLine
	{
		public GizmoZLine(string name, Transform parent)
			: base(name, parent)
		{
			this.direction = Vector3.forward;
			this.color = Color.blue;
		}
	}
}

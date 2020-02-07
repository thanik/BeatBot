using System.Collections.Generic;
using UnityEngine;

namespace JPBotelho
{
	[ExecuteInEditMode]
	public class SplineTester : MonoBehaviour
	{
		public CatmullRom spline;

		public Vector3[] controlPoints;

		[Range(2, 25)]
		public int resolution;
		public bool closedLoop;

		public Vector3 newPos;
		[Range(0, 1)]
		public float time;
		void Start()
		{
			if (spline == null)
			{
				spline = new CatmullRom(controlPoints, resolution, closedLoop);
			}
		}

		void Update()
		{
			if (spline != null)
			{
				spline.Update(controlPoints);
				spline.Update(resolution, closedLoop);
				spline.DrawSpline(Color.white);
				transform.position = LerpOverNumber(spline.GetPoints(), time);
			}
			else
			{
				spline = new CatmullRom(controlPoints, resolution, closedLoop);
			}
		}

		public Vector3 LerpOverNumber(Vector3[] vectors, float time)
		{
			time = Mathf.Clamp01(time);
			if (vectors == null || vectors.Length == 0)
			{
				throw (new System.Exception("Vectors input must have at least one value"));
			}
			if (vectors.Length == 1)
			{
				return vectors[0];
			}

			if (time == 0)
			{
				return vectors[0];
			}

			if (time == 1)
			{
				return vectors[vectors.Length - 1];
			}

			float t = time * (vectors.Length - 1);
			int p = (int)Mathf.Floor(t);
			t -= p;
			return Vector3.Lerp(vectors[p], vectors[p + 1], t);
		}
	}
}
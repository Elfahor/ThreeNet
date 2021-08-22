using System;
using System.Collections.Generic;
using System.Numerics;

namespace ThreeNet
{
	public class Transform : IEquatable<Transform>
	{
		public Vector3 Position { get => position; set => position = value; }
		public Vector3 Scale { get => scale; set => scale = value; }
		public Vector3 Rotation
		{
			get => rotation; set
			{
				value.X %= MathF.PI * 2;
				value.Y %= MathF.PI * 2;
				value.Z %= MathF.PI * 2;
				rotation = value;
			}
		}

		public Transform()
		{
			Position = Vector3.Zero;
			Rotation = Vector3.Zero;
			Scale = Vector3.One;
		}

		private Vector3 position;
		private Vector3 scale;
		private Vector3 rotation;

		public Matrix4x4 GetTransformMatrix()
		{
			Matrix4x4 position = Matrix4x4.CreateTranslation(Position);
			Matrix4x4 scale = Matrix4x4.CreateScale(Scale);
			Matrix4x4 rotation = Matrix4x4.CreateRotationX(Rotation.X) *
				Matrix4x4.CreateRotationY(Rotation.Y) *
				Matrix4x4.CreateRotationZ(Rotation.Z);
			return scale * rotation * position;

		}

		public bool Equals(Transform? other) => EqualityComparer<Transform>.Default.Equals(this, other);

		public override bool Equals(object? obj) => obj is Transform transform && Equals(transform);

		public override int GetHashCode() => HashCode.Combine(Position, Scale, Rotation);

		public override string ToString() => $"Pos: {Position}; Scale: {Scale}; Rot: {Rotation}";

		public static bool operator ==(Transform left, Transform right) => left.Equals(right);

		public static bool operator !=(Transform left, Transform right) => !(left == right);


	}
}
using System;
using System.Collections.Generic;
using System.Numerics;

namespace ThreeNet
{
	public struct Transform : IEquatable<Transform>
	{
		public Vector3 Position { get; set; }
		public Vector3 Scale { get; set; }
		public Vector3 Rotation { get; set; }

		public bool Equals(Transform other) => EqualityComparer<Transform>.Default.Equals(this, other);

		public Matrix4x4 GetTransformMatrix()
		{
			Matrix4x4 position = Matrix4x4.CreateTranslation(Position);
			Matrix4x4 scale = Matrix4x4.CreateScale(Scale);
			Matrix4x4 rotation = Matrix4x4.CreateRotationX(Rotation.X) *
				Matrix4x4.CreateRotationY(Rotation.Y) *
				Matrix4x4.CreateRotationZ(Rotation.Z);
			return scale * rotation * position;

		}

		public override bool Equals(object? obj) => obj is Transform transform && Equals(transform);

		public override int GetHashCode() => HashCode.Combine(Position, Scale, Rotation);

		public override string ToString() => $"Pos: {Position}; Scale: {Scale}; Rot: {Rotation}";

		public static bool operator ==(Transform left, Transform right) => left.Equals(right);

		public static bool operator !=(Transform left, Transform right) => !(left == right);


	}
}
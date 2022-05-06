﻿using System;
using System.Linq;

using BulletSharp;

using Evergine.Mathematics;

using RhuEngine.Physics;

using RNumerics;

namespace RBullet
{
	public class BulletColliderShape : ILinkedColliderShape
	{
		public object GetBox2D(double boxHalfExtentX, double boxHalfExtentY, double boxHalfExtentZ) {
			return new Box2DShape((float)boxHalfExtentX, (float)boxHalfExtentY, (float)boxHalfExtentZ);
		}

		public object GetCapsuleShape(double radius, double height) {
			return new CapsuleShape((float)radius, (float)height);
		}

		public object GetCapsuleShapeX(double radius, double height) {
			return new CapsuleShapeX((float)radius, (float)height);
		}

		public object GetCapsuleShapeZ(double radius, double height) {
			return new CapsuleShapeZ((float)radius, (float)height);
		}
		public object GetBox3D(double boxHalfExtentX, double boxHalfExtentY, double boxHalfExtentZ) {
			return new BoxShape((float)boxHalfExtentX, (float)boxHalfExtentY, (float)boxHalfExtentZ);
		}
		public object GetCone(double radius, double height) {
			return new ConeShape((float)radius, (float)height);
		}
		public object GetConeX(double radius, double height) {
			return new ConeShapeX((float)radius, (float)height);
		}
		public object GetConeZ(double radius, double height) {
			return new ConeShapeZ((float)radius, (float)height);
		}
		public object GetCylinderShape(double boxHalfExtentX, double boxHalfExtentY, double boxHalfExtentZ) {
			return new CylinderShape((float)boxHalfExtentX, (float)boxHalfExtentY, (float)boxHalfExtentZ);
		}
		public object GetCylinderShapeX(double boxHalfExtentX, double boxHalfExtentY, double boxHalfExtentZ) {
			return new CylinderShapeX((float)boxHalfExtentX, (float)boxHalfExtentY, (float)boxHalfExtentZ);
		}

		public object GetCylinderShapeZ(double boxHalfExtentX, double boxHalfExtentY, double boxHalfExtentZ) {
			return new CylinderShapeZ((float)boxHalfExtentX, (float)boxHalfExtentY, (float)boxHalfExtentZ);
		}
		public object GetSphereShape(double radus) {
			return new SphereShape((float)radus);
		}

		public RigidBodyCollider GetCollider(ColliderShape obj, PhysicsSim physicsSim) {
			var collider = new RigidBodyCollider {
				CollisionShape = obj,
				PhysicsSim = physicsSim
			};
			new BRigidBodyCollider(collider);
			return collider;
		}

		public object GetConvexMeshShape(IMesh mesh) {
			if(mesh == null) {
				return new EmptyShape();
			}
			var indexVertexArray2 = new TriangleIndexVertexArray(mesh.TriangleIndices().ToArray(), mesh.VertexPos().Select((val)=>new Vector3(val.x, val.y, val.z)).ToArray());
			var trys = new ConvexTriangleMeshShape(indexVertexArray2, true);
			return trys;
		}
	}
}

﻿using RhuEngine.Components;
using RhuEngine.WorldObjects.ECS;
using RhuEngine.Linker;
using RNumerics;

namespace RhuEngine.WorldObjects
{
	public static class EntityHelper
	{
		public static (T, DynamicMaterial) AttachMesh<T, S>(this Entity entity) where T : AssetProvider<RMesh>, new() where S : AssetProvider<RShader>, new() {
			var meshRender = entity.AttachComponent<MeshRender>();
			var shader = entity.World.RootEntity.GetFirstComponentOrAttach<S>();
			var material = entity.AttachComponent<DynamicMaterial>();
			material.shader.Target = shader;
			meshRender.materials.Add().Target = material;
			var mesh = entity.AttachComponent<T>();
			meshRender.mesh.Target = mesh;
			return (mesh, material);
		}

		public static (T, DynamicMaterial, MeshRender) AttachMeshWithMeshRender<T, S>(this Entity entity) where T : AssetProvider<RMesh>, new() where S : AssetProvider<RShader>, new() {
			var meshRender = entity.AttachComponent<MeshRender>();
			var shader = entity.World.RootEntity.GetFirstComponentOrAttach<S>();
			var material = entity.AttachComponent<DynamicMaterial>();
			material.shader.Target = shader;
			meshRender.materials.Add().Target = material;
			var mesh = entity.AttachComponent<T>();
			meshRender.mesh.Target = mesh;
			return (mesh, material, meshRender);
		}
	}
}

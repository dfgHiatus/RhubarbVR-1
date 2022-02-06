﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using RhuEngine.Managers;
using RhuEngine.WorldObjects;
namespace RhuEngine.AssetSystem
{
	public class AssetSession: IDisposable
	{
		public readonly AssetManager Manager;

		public World World { get; private set; }

		public void AssetLoadingTask(Action<byte[]> action,Uri asset,bool useCache) {
			var task = new AssetTask(action,this,asset,useCache);
			Manager.tasks.Add(task);
			task.Start();
		}

		public byte[] GetAsset(Uri uri,bool useCache) {
			return uri.Scheme.ToLower()=="local" ? GetLocalAsset(uri, useCache) : Manager.GetAsset(uri, useCache);
		}

		public async Task<byte[]> GetAssetAsync(Uri uri, bool useCache) {
			return await Task.Run(()=>GetAsset(uri,useCache));
		}

		private byte[] GetLocalAsset(Uri uri, bool useCache) {
			byte[] asset = null;
			if (useCache) {
				asset = Manager.GetCacheAsset(uri);
			}
			return asset is null ? GetLocalAsset(uri) : asset;
		}

		private byte[] GetLocalAsset(Uri uri) {
			return World.RequestAssets(uri);
		}

		public AssetSession(AssetManager manager,World world) {
			World = world;
			Manager = manager;
			Manager.assetSessions.Add(this);
		}

		public void Dispose() {
			Manager.assetSessions.Remove(this);
		}
	}
}

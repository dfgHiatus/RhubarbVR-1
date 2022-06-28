﻿using System;
using System.Runtime.InteropServices;

using RhuEngine.Physics;
using BulletSharp;
using System.IO;
using System.Linq;
using RhuEngine.Linker;

namespace RBullet
{  
	public class BulletPhsyicsLink: PhysicsHelper.Physics<BulletRigidBodyCollider, BulletPhysicsSim, BulletColliderShape>
    {

		public BulletPhsyicsLink() {
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
				if (!RhuEngine.Engine.MainEngine.EngineLink.ForceLibLoad) {
					//This works becases i have no idea where dlopen is in macos 
					File.Copy("./runtimes/MacOS/native/liblibbulletc.dylib", "./liblibbulletc.dylib");
				}
			}
			else if (!Native.Load()) {
				if (Environment.OSVersion.Platform == PlatformID.Unix) {
					RLog.Info("Did not load lib at first");
					var files = Directory.GetFiles("./../../", "*/*/linux-x64/native/libbulletc.so");
					var filesarray = files.ToArray();
					if (filesarray.Length == 0) {
						throw new Exception("Failed to Find lib");
					}
					else {
						File.Copy(filesarray[0], "./libbulletc.so");
						RLog.Info("Did copy to try and get arround lib no loading");
					}
				}
				else {
						throw new Exception("Failed to load lib");
				}
			}
		}

	}
}

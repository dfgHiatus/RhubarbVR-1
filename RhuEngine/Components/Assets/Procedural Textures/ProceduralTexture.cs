﻿using System;
using System.Collections.Generic;
using System.Text;

using RhuEngine.Linker;
using RhuEngine.WorldObjects;
using RhuEngine.WorldObjects.ECS;

using RNumerics;

namespace RhuEngine.Components
{
	public abstract class ProceduralTexture : AssetProvider<RTexture2D>
	{
		[Default(TexSample.Anisotropic)]
		[OnChanged(nameof(TextValueChanged))]
		public readonly Sync<TexSample> sampleMode;

		[Default(TexAddress.Wrap)]
		[OnChanged(nameof(TextValueChanged))]
		public readonly Sync<TexAddress> addressMode;

		[Default(3)]
		[OnChanged(nameof(TextValueChanged))]
		public readonly Sync<int> anisoptropy;

		public void TextValueChanged() {
			if (Value is null) {
				return;
			}
			Value.Anisoptropy = anisoptropy;
			Value.AddressMode = addressMode;
			Value.SampleMode = sampleMode;
		}


		public void UpdateTexture(Colorb[] colors, int width,int hight) {
			if(Value is null) {
				Load(RTexture2D.FromColors(colors, width, hight,true));
				TextValueChanged();
				return;
			}
			Value.SetColors(width, hight, colors);
		}

		[OnChanged(nameof(ComputeTexture))]
		public readonly Sync<Vector2i> Size;

		public override void OnAttach() {
			base.OnAttach();
			Size.Value = new Vector2i(128);
		}

		public abstract void Generate();

		public override void OnLoaded() 
		{
			ComputeTexture();
		}

		public void ComputeTexture() 
		{
			if (!Engine.EngineLink.CanRender) 
			{
				return;
			}

			RWorld.ExecuteOnEndOfFrame(this, () => {
				try {
					Generate();
				}
				catch (Exception e) {
#if DEBUG
					RLog.Err(e.ToString());
#endif
				}
			});
		}
	}
}
